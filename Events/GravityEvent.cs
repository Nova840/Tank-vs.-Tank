using UnityEngine;
using System.Collections;

public class GravityEvent : Event {

    [SerializeField]
    private Vector3 gravity = new Vector3(0, -10, 0);

    [SerializeField]
    private float changeSpeed = .05f;

    private Vector3 originalGravity;

    private new void Start() {
        base.Start();
        originalGravity = Physics.gravity;
    }

    private void FixedUpdate() {
        Vector3 targetGravity = HasDurationFinished() ? originalGravity : gravity;
        Physics.gravity = Vector3.MoveTowards(Physics.gravity, targetGravity, changeSpeed);

        if (Physics.gravity == originalGravity)
            Destroy(gameObject);
    }

}
