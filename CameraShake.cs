using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    public void Shake(float duration, float magnitude) {
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    //this assumes there is something else pulling the camera to it's intended position
    private IEnumerator ShakeCoroutine(float duration, float magnitude) {
        float startTime = Time.time;
        while (startTime + duration >= Time.time) {
            transform.position += transform.InverseTransformDirection(new Vector3(Random.Range(-magnitude, magnitude) / 2, Random.Range(-magnitude, magnitude), 0));
            yield return null;
        }
    }

}
