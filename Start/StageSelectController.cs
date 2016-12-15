using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/*
Set to happen before default time in script execution order
because the start button will still be down from selecting an option.
*/
public class StageSelectController : MonoBehaviour {

    public static bool stageSelect = false;

    private static int currentStageIndex = 1;
    public static int CurrentStageIndex { get { return currentStageIndex; } }
    bool moved = false;

    [System.Serializable]
    private class Stage {
        [SerializeField]
        private GameObject gameObject;
        [SerializeField]
        private string coOpSceneToLoad = null, versusSceneToLoad = null;
        private Vector3 originalScale;

        public GameObject GameObject { get { return gameObject; } }
        public string CoOpSceneToLoad { get { return coOpSceneToLoad; } }
        public string VersusSceneToLoad { get { return versusSceneToLoad; } }
        public Vector3 OriginalScale { get; set; }
    }

    [SerializeField]
    private Stage[] stages;

    [SerializeField]
    private Transform selectedPosition;
    [SerializeField]
    private float lerpRate = 3, offset = 5, rotateSpeed = 1, selectedScaleMultiplier = 2;

    private void Awake() {
        foreach (Stage stage in stages)
            stage.OriginalScale = stage.GameObject.transform.localScale;
    }

    private void Start() {
        Move(false);
    }

    private bool updated = false;
    private void Update() {
        if (!stageSelect) {
            updated = false;
            return;
        }

        if (Input.GetButtonDown(Controls.allBackButtonsAxes)) {
            stageSelect = false;
            return;
        }

        if (!updated) {
            SetRotation(Vector3.up * rotateSpeed);
            if (currentStageIndex == 0 && CycleButtons.IsCoOp()) {//set to random on co-op
                Cycle(1);
                Move(false);
            }
        }

        int allHorizontal = Mathf.Clamp((int)(Input.GetAxisRaw(Controls.allHorizontalAxes) + Input.GetAxisRaw(Controls.keyboard.LookHorizontal)), -1, 1);
        if (!moved)//so it only cycles once
            Cycle(allHorizontal);

        moved = allHorizontal >= 1 || allHorizontal <= -1;//a number outside of -1 to 1 inclusive

        Move(true);

        if (Input.GetButtonDown(Controls.allStartButtonsAxes))
            if (currentStageIndex == 0)//random always first
                SceneManager.LoadScene(stages[Random.Range(1, stages.Length)].VersusSceneToLoad);
            else
                SceneManager.LoadScene(CycleButtons.IsCoOp() ? stages[currentStageIndex].CoOpSceneToLoad : stages[currentStageIndex].VersusSceneToLoad);

        updated = true;
    }

    private void Cycle(int amount) {
        if (amount == 0)//for efficiency
            return;
        SetRotation(Vector3.zero);
        currentStageIndex += amount;
        currentStageIndex = Mathf.Clamp(currentStageIndex, CycleButtons.IsCoOp() ? 1 : 0, stages.Length - 1);
        SetRotation(Vector3.up * rotateSpeed);
    }

    private void SetRotation(Vector3 rotation) {
        stages[currentStageIndex].GameObject.GetComponent<Rotate>().rotation = rotation;
    }

    private void Move(bool lerp) {
        for (int i = 0; i < stages.Length; i++) {
            Transform t = stages[i].GameObject.transform;
            t.position = Vector3.Lerp(t.position, TargetPosition(i), lerp ? lerpRate * Time.deltaTime : 1);
            t.localScale = Vector3.Lerp(t.localScale, TargetScale(i), lerp ? lerpRate * Time.deltaTime : 1);
            if (i != currentStageIndex)
                t.rotation = Quaternion.Lerp(t.rotation, selectedPosition.rotation, lerp ? lerpRate * Time.deltaTime : 1);
        }
    }

    private Vector3 TargetPosition(int index) {
        return selectedPosition.position + selectedPosition.right * (index - currentStageIndex) * offset;
    }

    private Vector3 TargetScale(int index) {
        return stages[index].OriginalScale * (index == currentStageIndex ? selectedScaleMultiplier : 1);
    }

    public void SetStageSelect(bool b) {//for buttons
        stageSelect = b;
    }

}
