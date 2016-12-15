using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CarControl), typeof(PlayerControls))]
public class UserControl : MonoBehaviour {

    private CarControl carControl;
    private PlayerControls playerControls;

    [HideInInspector]
    public bool controling = true;

    private void Awake() {
        carControl = GetComponent<CarControl>();
        playerControls = GetComponent<PlayerControls>();//too early to get the controls object off of this and store only that
    }

    private void Update() {
        if (!playerControls.controls)
            return;

        if (controling)
            carControl.Control(Input.GetAxisRaw(playerControls.controls.Vertical), Input.GetAxisRaw(playerControls.controls.Horizontal));
        else
            carControl.Control(0, 0);
    }

}
