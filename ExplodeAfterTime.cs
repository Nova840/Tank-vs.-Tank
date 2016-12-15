using UnityEngine;
using System.Collections;

public class ExplodeAfterTime : Explodeable {

    [SerializeField]
    private float time = 7.5f;

    private void Start() {
        Invoke("Explode", time);
    }

}
