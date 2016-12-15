using UnityEngine;
using System.Collections;

public class SpawnContainer : MonoBehaviour {

    [SerializeField]
    private GameObject containerPrefab;
    [SerializeField]
    private float rangeX = 0, rangeZ = 0,//Should be half the size of the ground times 10.
        maxSpin = 1, interval = 10;
    private void Awake() {
        StartCoroutine(Spawner(interval));
    }

    private IEnumerator Spawner(float seconds) {
        while (true) {
            yield return new WaitForSeconds(seconds);
            GameObject container = (GameObject)Instantiate(
                containerPrefab,
                new Vector3(
                    Random.Range(-rangeX, rangeX),
                    transform.position.y,
                    Random.Range(-rangeZ, rangeZ)),
                Quaternion.identity
                );
            container.transform.localEulerAngles = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
            container.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-maxSpin, maxSpin), Random.Range(-maxSpin, maxSpin), Random.Range(-maxSpin, maxSpin));
        }
    }

}
