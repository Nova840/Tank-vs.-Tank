using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    public Vector3 rotation = Vector3.zero;

	private void FixedUpdate () {
        transform.Rotate(rotation);
	}

}
