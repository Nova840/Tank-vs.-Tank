using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class AirPlaneMove : MonoBehaviour {

    private Rigidbody rb;

    [SerializeField]
    private Transform leftTailForce = null, rightTailForce = null;
    private int leftDirection = 0, rightDirection = 0;

    [SerializeField]
    private PlaneTail leftPlaneTail = null, rightPlaneTail = null;

    [SerializeField]
    private float maxMoveForce = 5000, minMoveForce = 1200, changeSpeed = 200,
        tailThrust = 500, liftSpeedThresholdMin = 15, liftSpeedThresholdMax = 25, propellerSpeed;

    private float moveForce;

    [SerializeField]
    private Vector3 gravityBoost = Vector3.zero;

    [SerializeField]
    private Text speedText = null;

    [SerializeField]
    private Rotate[] propellerRotates;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        moveForce = minMoveForce;
    }

    private float PercentDistanceFrom90(float angle) {
        return Mathf.Abs(Mathf.Abs(angle - 180) - 90) / 90;
    }

    private float PercentLift(float speed) {
        return (Mathf.Clamp(speed, liftSpeedThresholdMin, liftSpeedThresholdMax) - liftSpeedThresholdMin) / (liftSpeedThresholdMax - liftSpeedThresholdMin);
    }

    private Vector3 VelXZ() {
        return new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }

    private void Update() {
        if (Input.GetKey(KeyCode.W))
            moveForce += changeSpeed;
        if (Input.GetKey(KeyCode.S))
            moveForce -= changeSpeed;

        moveForce = Mathf.Clamp(moveForce, minMoveForce, maxMoveForce);

        leftDirection = rightDirection = 0;
        if (Input.GetKey(KeyCode.DownArrow)) {
            leftDirection--;
            rightDirection--;
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            leftDirection++;
            rightDirection++;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            leftDirection--;
            rightDirection++;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            leftDirection++;
            rightDirection--;
        }
        leftDirection = Mathf.Clamp(leftDirection, -1, 1);
        rightDirection = Mathf.Clamp(rightDirection, -1, 1);

        leftPlaneTail.Angle(leftDirection);
        rightPlaneTail.Angle(rightDirection);

        if (speedText)
            speedText.text = "Speed: " + (int)rb.velocity.magnitude + "\nForward Thrust: " + moveForce;

        foreach (Rotate r in propellerRotates)
            r.rotation = moveForce <= 0 ? Vector3.zero : Vector3.forward * 40;

        if (Input.GetKeyDown(KeyCode.R)) {
            transform.position = new Vector3(0, 5, 0);
            transform.rotation = Quaternion.identity;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            moveForce = 0;
        }
    }

    private void FixedUpdate() {
        rb.AddForce(gravityBoost * rb.mass);
        rb.AddForce(transform.forward * moveForce);
        rb.AddForce((-Physics.gravity - gravityBoost) * rb.mass * PercentDistanceFrom90(transform.localEulerAngles.z) *
            PercentDistanceFrom90(transform.localEulerAngles.x) * PercentLift(VelXZ().magnitude));

        rb.AddForceAtPosition(leftTailForce.up * leftDirection * tailThrust * PercentLift(rb.velocity.magnitude), leftTailForce.position);
        rb.AddForceAtPosition(rightTailForce.up * rightDirection * tailThrust * PercentLift(rb.velocity.magnitude), rightTailForce.position);

    }

}
