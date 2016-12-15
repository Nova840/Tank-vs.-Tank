using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AISpawner : MonoBehaviour {

    [SerializeField]
    private GameObject spawnpointsContainer = null, AIPrefab = null;

    [SerializeField]
    private bool useKey = false;

    [SerializeField]
    private float interval = 30;//put a negative number to stop them from spawning

    [SerializeField]
    private KeyCode key = KeyCode.G;

    private List<Transform> spawnpoints;

    private void Awake() {
        List<Transform> temp = spawnpointsContainer.GetComponentsInChildren<Transform>().ToList();
        temp.Remove(spawnpointsContainer.transform);
        spawnpoints = temp;
        if (!useKey)
            StartCoroutine(SpawnTimer(interval));
    }

    private void Update() {
        if (useKey && Input.GetKeyDown(key))
            SpawnAI();
    }


    private IEnumerator SpawnTimer(float seconds) {
        while (true) {
            yield return new WaitForSeconds(seconds);
            SpawnAI();
        }
    }

    private void SpawnAI() {
        Transform spawnpoint = spawnpoints[Random.Range(0, spawnpoints.Count)];
        Instantiate(AIPrefab, spawnpoint.position, spawnpoint.rotation);
    }

}
