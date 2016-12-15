using UnityEngine;
using System.Collections;

public class CameraTarget : MonoBehaviour {

    private Transform cameraTransform;

    [SerializeField, Range(0, 1)]
    private float positionLerpRate = .2f, rotationLerpRate = .2f;

    private void Awake() {
        cameraTransform = transform.Find("Player Camera");
    }

    private void FixedUpdate() {
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, transform.position, positionLerpRate);
        cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, transform.rotation, rotationLerpRate);
    }

}
