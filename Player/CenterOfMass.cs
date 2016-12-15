using UnityEngine;
using System.Collections;

public class CenterOfMass : MonoBehaviour {

    [SerializeField]
    private Rigidbody rBody;

    private void Start() {
        ResetCenterOfMass();//For AI tanks
    }

    public void ResetCenterOfMass() {
        rBody.centerOfMass = transform.localPosition;
    }

}
