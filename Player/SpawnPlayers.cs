using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPlayers : MonoBehaviour {

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private GameObject spawnPointsContainer;
    private List<Transform> spawnpoints = new List<Transform>();
    public List<Transform> Spawnpoints { get { return spawnpoints; } }

    private void Awake() {
        foreach (Transform t in spawnPointsContainer.GetComponentsInChildren<Transform>())
            if (t != spawnPointsContainer.transform)
                spawnpoints.Add(t);
    }

    private void Start() {
        for (int i = 0; i < Players.playerControlSchemes.Length; i++) {
            if (!Players.playerControlSchemes[i])
                continue;

            GameObject player = Instantiate(playerPrefab);
            int playerNumber = i + 1;
            player.name = "Player " + playerNumber;
            player.transform.Find("Tank_v3_2/Gun/Barrel/Bullet Spawnpoint/Line").gameObject.layer = playerNumber + 10;

            Scripts.Players.AddPlayer(player);
        }
    }

}
