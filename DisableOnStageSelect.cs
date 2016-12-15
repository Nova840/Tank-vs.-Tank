using UnityEngine;
using System.Collections;

public class DisableOnStageSelect : MonoBehaviour {

    [SerializeField]
    private Behaviour behaviour;

    private void Update() {
        behaviour.enabled = !StageSelectController.stageSelect;
    }

}
