using UnityEngine;
using System.Collections;

//Currently used for the aim point.
public class DestroyOnNextFrame : MonoBehaviour {

    private void Awake() {
        StartCoroutine(DestroyNext());
    }

    private IEnumerator DestroyNext() {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }

}
