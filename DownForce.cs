using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class DownForce : MonoBehaviour {

    [SerializeField]
    private float force = 100f;

    private Rigidbody rb;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

	private void FixedUpdate () {
        rb.AddForce(Vector3.down * force);
	}

}
