using UnityEngine;
using System.Collections;

public class DestroyIfBelow : MonoBehaviour {

    [SerializeField]
    private float threshold = -1000;

    private void Update() {
        if (transform.position.y < threshold)
            Destroy(gameObject);
    }

}
