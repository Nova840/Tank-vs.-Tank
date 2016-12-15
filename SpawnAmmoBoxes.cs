using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpawnAmmoBoxes : MonoBehaviour {

    [SerializeField]
    private GameObject ammoBoxPrefab = null, spawnpointsContainer = null;
    [SerializeField]
    private float intervalMin = 5, intervalMax = 10;
    [SerializeField]
    private int initialSpawns = 0, minBullets = 1, maxBullets = 3, maxAmmoBoxes = 8;

    private List<GameObject> ammoBoxes = new List<GameObject>();
    private List<Transform> spawnpoints = new List<Transform>();

    private void Awake() {
        foreach (Transform spawnpoint in spawnpointsContainer.GetComponentsInChildren<Transform>())
            if (spawnpoint != spawnpointsContainer.transform)
                spawnpoints.Add(spawnpoint);
    }

    private void Start() {
        StartCoroutine(Spawner());
        for (int i = 0; i < initialSpawns; i++)
            Spawn();
    }

    private IEnumerator Spawner() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(intervalMin, intervalMax));
            ammoBoxes.RemoveAll(b => !b);
            if (ammoBoxes.Count < maxAmmoBoxes)
                Spawn();
        }
    }

    private void Spawn() {
        Transform spawnpoint = FindSpawnpoint();
        if (!spawnpoint)
            return;
        GameObject ammoBox = Instantiate(ammoBoxPrefab);
        ammoBox.GetComponent<AddAmmoOnTriggerEnter>().bullets = GenerateRandomBullets();
        ammoBox.transform.position = spawnpoint.position;
        ammoBoxes.Add(ammoBox);
    }

    private AddAmmoOnTriggerEnter.BulletsToAdd[] GenerateRandomBullets() {
        AddAmmoOnTriggerEnter.BulletsToAdd[] bullets = new AddAmmoOnTriggerEnter.BulletsToAdd[Random.Range(minBullets, maxBullets + 1)];

        for (int i = 0; i < bullets.Length; i++) {
            bullets[i] = new AddAmmoOnTriggerEnter.BulletsToAdd();
            bullets[i].type = (BulletType)Random.Range(0, (int)BulletType.NumberOfTypes);
            //amount defaults to 1
        }

        return bullets;
    }

    private Transform FindSpawnpoint() {
        List<Transform> tempSpawnpoints = new List<Transform>(spawnpoints);
        tempSpawnpoints.Shuffle();
        foreach (Transform spawnpoint in tempSpawnpoints) {
            if (!Physics.OverlapSphere(spawnpoint.position, 5).Any(
                c => c.transform.root.CompareTag("Player") || c.transform.root.CompareTag("Ammo Box")
                )) {
                return spawnpoint;
            }
        }
        return null;
    }

}
