using UnityEngine;
using System.Collections;

public class RotateGun : MonoBehaviour {

    [SerializeField]
    private Transform cameraRig;
    [SerializeField]
    private float offset, maxDelta = 1;

    private void FixedUpdate() {
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            Mathf.MoveTowardsAngle(transform.localEulerAngles.y, cameraRig.localEulerAngles.y + offset, maxDelta),
            transform.localEulerAngles.z
            );
    }

}
