using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerControls), typeof(UserControl), typeof(Rigidbody))]
public class FlipPlayer : MonoBehaviour {

    private PlayerControls playerControls;
    private UserControl userControl;

    private Rigidbody rb;

    [SerializeField]
    private float torqueMultiplier = 50000;

    [SerializeField, Range(.001f, 1)]
    private float valueToFlip = 1;//minimum inclusive, affected by sensetivity

    private void Awake() {
        playerControls = GetComponent<PlayerControls>();
        userControl = GetComponent<UserControl>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if (!playerControls.controls)//null at first
            return;

        bool buttonDown = Input.GetAxisRaw(playerControls.controls.Flip) >= valueToFlip;

        userControl.controling = !buttonDown;
        if (buttonDown) {
            rb.AddRelativeTorque(
                Input.GetAxisRaw(playerControls.controls.Vertical) * torqueMultiplier,
                0,
                -Input.GetAxisRaw(playerControls.controls.Horizontal) * torqueMultiplier);
        }
    }

}
