using UnityEngine;
using System.Collections;

public class MoveLevel : MonoBehaviour {

    [SerializeField]
    private Transform origin = null, target = null;

    [SerializeField]
    private float maxMovement = 1;

    private bool movingToTarget = false;

    private void FixedUpdate() {
        Vector3 currentTarget = movingToTarget ? target.position : origin.position;
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, maxMovement);
    }

    public void Move() {
        movingToTarget = !movingToTarget;
    }

}
