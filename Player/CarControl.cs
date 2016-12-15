using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarControl : MonoBehaviour {

    [System.Serializable]
    private class Wheel {
        [SerializeField]
        private Transform transform;
        [SerializeField]
        private WheelCollider collider;
        [SerializeField]
        private Vector3 spinAxis;
        //If the wheel isn't facing forward in the mesh, you will need to rotate it by this amount.
        //leave at 0 unless wheels are spinning on the wrong axis.

        public Transform Transform { get { return transform; } }
        public WheelCollider Collider { get { return collider; } }
        public Vector3 SpinAxis { get { return spinAxis; } }
    }

    [SerializeField]
    private List<Wheel> frontWheels = new List<Wheel>(),
        middleWheels = new List<Wheel>(),
        backWheels = new List<Wheel>();
    private List<Wheel> allWheels = new List<Wheel>();

    [SerializeField]
    private float motorTorque = 3000,
        maxRPM = 250,
        maxSteerAngle = 15,
        brakeTorque = 3000,
        steerSpeed = 1;

    private void Awake() {
        allWheels.AddRange(frontWheels);
        allWheels.AddRange(middleWheels);
        allWheels.AddRange(backWheels);
    }

    public void Control(float vertical, float horizontal) {

        float brakeTorque = 0;
        if (vertical == 0)//If no vertical input
            brakeTorque = this.brakeTorque;

        //Steering
        foreach (Wheel wheel in frontWheels)
            wheel.Collider.steerAngle = Mathf.MoveTowards(wheel.Collider.steerAngle, horizontal * maxSteerAngle, steerSpeed);

        foreach (Wheel wheel in backWheels)
            wheel.Collider.steerAngle = Mathf.MoveTowards(wheel.Collider.steerAngle, -horizontal * maxSteerAngle, steerSpeed);


        foreach (Wheel wheel in allWheels) {
            wheel.Collider.brakeTorque = brakeTorque;

            float torque = vertical * motorTorque;

            //too much forward or too much backward
            if ((wheel.Collider.rpm > maxRPM && vertical > 0) || (wheel.Collider.rpm < -maxRPM && vertical < 0))
                torque = 0;

            wheel.Collider.motorTorque = torque;
            //wheel.Collider.motorTorque = (wheel.Collider.rpm <= maxRPM && wheel.Collider.rpm >= -maxRPM) ? vertical * motorTorque : 0;

            //Set the wheel mesh to collider position.
            Quaternion quat;
            Vector3 pos;
            wheel.Collider.GetWorldPose(out pos, out quat);
            wheel.Transform.position = pos;
            wheel.Transform.rotation = quat;
            wheel.Transform.Rotate(wheel.SpinAxis);
        }
    }

}