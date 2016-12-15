using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CarControl))]
public class DummyPlayerControl : MonoBehaviour {

    private CarControl carControl;

    private void Awake() {
        carControl = GetComponent<CarControl>();
    }

    private void Update() {
        carControl.Control(0, 0);
    }

}
