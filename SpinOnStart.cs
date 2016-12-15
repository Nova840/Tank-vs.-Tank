using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class SpinOnStart : MonoBehaviour {

    private void Start() {
        GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
    }

}
