using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

    [SerializeField]
    private float time = 30;

    void Awake() {
        Destroy(gameObject, time);
    }

}
