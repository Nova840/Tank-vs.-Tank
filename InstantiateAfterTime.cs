using UnityEngine;
using System.Collections;

public class InstantiateAfterTime : MonoBehaviour {

    [SerializeField]
    private GameObject toInstantiate;
    [SerializeField]
    private float instantiateAfter = 1;

    private void Start() {
        StartCoroutine(InstantiateThis());
    }

    private IEnumerator InstantiateThis() {
        yield return new WaitForSeconds(instantiateAfter);
        Instantiate(toInstantiate, transform.position, transform.rotation);
    }
}
