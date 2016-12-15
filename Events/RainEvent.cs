using UnityEngine;
using System.Collections;

public class RainEvent : Event {

    [SerializeField]
    private GameObject prefab;
    private float rangeX, rangeZ, centerX, centerZ;
    [SerializeField]
    private float maxSpin = 1, interval = 1, intervalVariation = 1, spawnHeight = 250;
    [SerializeField]
    private Vector3 startVelocity = new Vector3(0, -100, 0);

    private new void Start() {
        base.Start();

        LevelSize levelSize = GameObject.Find("Level").GetComponent<LevelSize>();
        rangeX = levelSize.SizeX;
        rangeZ = levelSize.SizeZ;
        centerX = levelSize.CenterX;
        centerZ = levelSize.CenterZ;

        StartCoroutine(Spawner(interval));
    }

    private IEnumerator Spawner(float seconds) {
        while (!HasDurationFinished()) {
            GameObject g = (GameObject)Instantiate(
                prefab,
                new Vector3(
                    centerX + Random.Range(-rangeX, rangeX),
                    spawnHeight,
                    centerZ + Random.Range(-rangeZ, rangeZ)
                    ),
                Quaternion.identity
                );
            g.transform.localEulerAngles = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
            Rigidbody rb = g.GetComponent<Rigidbody>();
            rb.angularVelocity = new Vector3(Random.Range(-maxSpin, maxSpin), Random.Range(-maxSpin, maxSpin), Random.Range(-maxSpin, maxSpin));
            rb.velocity = startVelocity;
            yield return new WaitForSeconds(Random.Range(seconds - intervalVariation, seconds + intervalVariation));
        }
        Destroy(gameObject);
    }

}
