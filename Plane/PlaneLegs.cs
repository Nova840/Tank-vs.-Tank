using UnityEngine;
using System.Collections;

public class PlaneLegs : MonoBehaviour {

    [SerializeField]
    private PlaneLandingRay ray;

    [SerializeField]
    private float moveSpeed = 1, downAngle = 0, upAngle = 90;

    private void Update() {
        transform.localEulerAngles = new Vector3(
            Mathf.MoveTowardsAngle(transform.localEulerAngles.x, ray.Hit ? downAngle : upAngle, moveSpeed),
            transform.localEulerAngles.y,
            transform.localEulerAngles.z
            );
    }

}
