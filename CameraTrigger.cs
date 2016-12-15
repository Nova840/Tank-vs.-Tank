using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]//Rigidbody required to detect collision
public class CameraTrigger : MonoBehaviour {

    [SerializeField]
    private GameObject green = null, blue = null, gray = null;

    private void OnTriggerEnter(Collider other) {
        OverlayToggle(other, true);
    }

    private void OnTriggerExit(Collider other) {
        OverlayToggle(other, false);
    }

    private void OverlayToggle(Collider other, bool enable) {
        if (other.CompareTag("Explosion"))
            return;

        if (other.gameObject.layer == 10)
            green.SetActive(enable);
        else if (other.gameObject.layer == 9)
            blue.SetActive(enable);
        else if (!other.isTrigger)
            gray.SetActive(enable);
    }

}
