using UnityEngine;
using System.Collections;

public class ExplodeOnHit : Explodeable {

    private void OnCollisionEnter(Collision other) {
        Explode();
    }

}
