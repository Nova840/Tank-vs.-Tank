using UnityEngine;
using System.Collections;

public class StartPlayersCamera : MonoBehaviour {

    [SerializeField]
    private Transform cameraTarget0 = null, cameraTarget1 = null, cameraTarget2 = null, cameraTarget3 = null, cameraTarget4 = null, stageSelectTarget = null;
    private Transform[] cameraTargets = new Transform[5];

    [HideInInspector]
    public int targetIndex = 0;

    [SerializeField]
    private float positionLerpRate = .1f, rotationLerpRate = .1f;

    private void Awake() {
        cameraTargets[0] = cameraTarget0;
        cameraTargets[1] = cameraTarget1;
        cameraTargets[2] = cameraTarget2;
        cameraTargets[3] = cameraTarget3;
        cameraTargets[4] = cameraTarget4;
    }

    private void Start() {//Awake happens before this in UnregisterIfControllerDisconnected
        Move(false);
    }

    private void Update() {
        Move(true);
    }

    private void Move(bool lerp) {
        Vector3 targetPosition = StageSelectController.stageSelect ? stageSelectTarget.position : cameraTargets[targetIndex].position;
        Quaternion targetRotation = StageSelectController.stageSelect ? stageSelectTarget.rotation : cameraTargets[targetIndex].rotation;

        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, lerp ? positionLerpRate * Time.deltaTime : 1);
        Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, targetRotation, lerp ? rotationLerpRate * Time.deltaTime : 1);
    }
}
