using UnityEngine;
using System.Collections;

public class BarrelAngle : MonoBehaviour {

    [SerializeField]
    private Vector3 rotation;

    private PlayerControls playerControls;

    [SerializeField, Range(0, 1)]
    private float slowSpeed = .15f;

    private void Awake() {
        playerControls = transform.root.GetComponent<PlayerControls>();//too early to get the controls object off of this and store only that
    }

    private void FixedUpdate() {
        if (playerControls.controls)
            transform.Rotate(rotation * Input.GetAxis(playerControls.controls.LookVertical) * (1 - Mathf.Clamp(Input.GetAxisRaw(playerControls.controls.SlowAim), 0, 1 - slowSpeed)));
    }

}
