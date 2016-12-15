using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {

    [HideInInspector]
    public Transform cameraTransform;

    private void Start() {
        Look();
    }

    private void Update() {
        Look();
    }

    private void Look() {
        transform.LookAt(cameraTransform);
    }

}
