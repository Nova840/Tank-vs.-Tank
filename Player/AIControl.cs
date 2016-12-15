using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CarControl))]
public class AIControl : MonoBehaviour {

    [SerializeField]
    private HitDetect hitDetect;

    private CarControl carControl;

    private void Awake() {
        carControl = GetComponent<CarControl>();
    }

    private void Update() {
        float forward = 1;
        float right = 0;
        if (hitDetect.GetHitForward() || !hitDetect.GetMissDown()) {
            forward *= -1;
            right = 1;
        }
        carControl.Control(forward, right);
    }

}
