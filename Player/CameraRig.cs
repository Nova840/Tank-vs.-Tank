using UnityEngine;
using System.Collections;

public class CameraRig : MonoBehaviour {

    [SerializeField]
    private bool lockX = true, lockY = false, lockZ = true;
    [SerializeField]
    private Vector3 inputRotation;
    [SerializeField]
    private Transform barrel;
    private Transform gun;

    private Quaternion lastRotation;

    private PlayerControls playerControls;

    [SerializeField, Range(0, 1)]
    private float slowSpeed = .15f;

    private void Awake() {
        playerControls = transform.root.GetComponent<PlayerControls>();//too early to get the controls object off of this and store only that
        gun = barrel.parent;
    }

    private void LateUpdate() {
        transform.rotation = lastRotation;

        transform.Rotate(inputRotation * Input.GetAxis(playerControls.controls.LookHorizontal) *
            (1 - Mathf.Clamp(Input.GetAxisRaw(playerControls.controls.SlowAim), 0, 1 - slowSpeed)) * Time.deltaTime * 100);

        transform.eulerAngles = new Vector3(
            lockX ? 0 : barrel.localEulerAngles.x,
            lockY ? 0 : transform.eulerAngles.y,
            lockZ ? 0 : transform.eulerAngles.z
        );

        lastRotation = transform.rotation;
    }

    public void ResetRotation() {
        transform.localEulerAngles = Vector3.zero;
        lastRotation = transform.rotation;
        barrel.localEulerAngles = Vector3.zero;
        gun.localEulerAngles = Vector3.zero;
    }
}
