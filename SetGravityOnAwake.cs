using UnityEngine;
using System.Collections;

public class SetGravityOnAwake : MonoBehaviour {

    [SerializeField]
    private Vector3 gravity;

    private void Awake() {
        Physics.gravity = gravity;
    }
}
