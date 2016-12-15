using UnityEngine;
using System.Collections;

public class PlaneCameraRig : MonoBehaviour {

    [SerializeField]
    private bool lockX = true, lockY = false, lockZ = true;

    private void LateUpdate() {
        transform.localEulerAngles = Vector3.zero;
        transform.eulerAngles = new Vector3(
            lockX ? 0 : transform.eulerAngles.x,
            lockY ? 0 : transform.eulerAngles.y,
            lockZ ? 0 : transform.eulerAngles.z
        );
    }

}
