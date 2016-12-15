using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

    [SerializeField]
    private GameObject targetsContainer;
    private List<GameObject> targets = new List<GameObject>();

    [SerializeField]
    private float endDelay = 3;

    [SerializeField]
    private string endMessage = "Tutorial Complete";

    [SerializeField]
    private Text completeText;

    private bool ended = false;

    [System.Serializable]
    private class Stage {
        [SerializeField]
        private MoveLevel[] moveables;

        [SerializeField]
        private int targetsBeforeEnd = 1;

        [SerializeField]
        private float endShakeDuration = 0, endShakeMagnitude = .2f;

        public MoveLevel[] Moveables { get { return moveables; } }
        public int TargetsBeforeEnd { get { return targetsBeforeEnd; } }
        public float EndShakeDuration { get { return endShakeDuration; } }
        public float EndShakeMagnitude { get { return endShakeMagnitude; } }
    }

    [SerializeField]
    private List<Stage> stages = new List<Stage>();

    private void Awake() {
        foreach (Transform t in targetsContainer.GetComponentsInChildren<Transform>())
            if (t.CompareTag("Target")) {
                targets.Add(t.gameObject);
                t.parent = null;
            }
        Destroy(targetsContainer);
    }

    private void Update() {
        if (ended)
            return;

        int numTargetsDestroyed = targets.Count(t => t == null);

        for (int i = 0; i < stages.Count; i++) {
            if (numTargetsDestroyed >= stages[i].TargetsBeforeEnd) {
                foreach (MoveLevel ml in stages[i].Moveables)
                    ml.Move();
                foreach (GameObject camera in Scripts.Players.playerCameras)
                    camera.GetComponent<CameraShake>().Shake(stages[i].EndShakeDuration, stages[i].EndShakeMagnitude);
                stages.RemoveAt(i);
                i--;
            }
        }

        if (stages.Count <= 0) {
            ended = true;
            Invoke("ReturnToStart", endDelay);
            completeText.gameObject.SetActive(true);
            completeText.text = endMessage;
        }
    }

    private void ReturnToStart() {
        SceneManager.LoadScene("Start");
    }

    public void RemoveTarget(GameObject target) {
        targets.Remove(target);
    }

}
