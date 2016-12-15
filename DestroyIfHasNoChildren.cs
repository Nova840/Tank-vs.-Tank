using UnityEngine;
using System.Collections;

public class DestroyIfHasNoChildren : MonoBehaviour {

    [SerializeField]
    private float interval = 60;

    private void Awake() {
        StartCoroutine(DestroyIfZeroChildren(interval));
    }

    private IEnumerator DestroyIfZeroChildren(float seconds) {
        while (true) {
            yield return new WaitForSeconds(seconds);
            if (GetComponentsInChildren<Transform>().Length <= 1)//accounting for this gameobject
                Destroy(gameObject);
        }
    }
}
