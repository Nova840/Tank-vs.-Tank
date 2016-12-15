using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class SetTrigger : MonoBehaviour {

    private Collider thisCollider;

    private void Awake() {
        thisCollider = GetComponent<Collider>();
    }

    public void SetTriggerAfterTime(bool trigger, float time) {
        StartCoroutine(Set(trigger, time));
    }

    private IEnumerator Set(bool trigger, float time) {
        yield return new WaitForSeconds(time);
        thisCollider.isTrigger = trigger;
    }

}
