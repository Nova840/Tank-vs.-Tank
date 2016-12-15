using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SetTextToStage : MonoBehaviour {

    private Text text;

    private void Awake() {
        text = GetComponent<Text>();
    }

    private void Update() {
        int index = StageSelectController.CurrentStageIndex;
        text.text = index > 0 ? "Stage " + index : "Random";
    }
}
