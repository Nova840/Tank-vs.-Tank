using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Players : MonoBehaviour {

    [HideInInspector]
    public List<GameObject> players = new List<GameObject>();//assumes players are in order
    [HideInInspector]
    public List<GameObject> playerCameras = new List<GameObject>();

    public static ControlScheme[] playerControlSchemes = new ControlScheme[4];

    public static int NumberOfPlayers {
        get {
            return playerControlSchemes.Count(c => c != null);
        }
    }

    [HideInInspector]
    public List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> spawners = new List<GameObject>();

    private List<Health> enemyHealths = new List<Health>();

    public void AddPlayer(GameObject player) {
        players.Add(player);
        playerCameras.Add(player.transform.Find("Camera Rig/Camera Target/Player Camera").gameObject);
    }

    public void AddToEnemyList(GameObject enemy) {
        enemies.Add(enemy);
        enemyHealths.Add(enemy.GetComponent<Health>());
    }

    public void RemoveEnemyFromList(GameObject enemy) {
        enemyHealths.Remove(enemy.GetComponent<Health>());
        enemies.Remove(enemy);
    }

    public bool enemiesDead() {
        foreach (Health h in enemyHealths) {
            if (!h.IsDead())
                return false;
        }
        return true;
    }

    public int NumberOfSpawners() {
        return spawners.Count;
    }

    public void AddSpawner(GameObject spawner) {
        spawners.Add(spawner);
    }

    public void RemoveSpawner(GameObject spawner) {
        spawners.Remove(spawner);
    }

    public GameObject GetPlayer(int playerNumber) {
        return players.Find(player => player.name == "Player " + playerNumber);
    }

    public static int PlayerNumberAtIndex(int index) {
        return PlayerNumber(Scripts.Players.players[index]);
    }

    public static int PlayerNumber(GameObject player) {
        string name = player.name;
        return int.Parse(name.Substring(name.Length - 1, 1));
    }

    public static int PlayerIndex(GameObject player) {
        List<GameObject> players = Scripts.Players.players;
        for (int i = 0; i < players.Count; i++)
            if (players[i] == player)
                return i;
        return -1;
    }

    public static ControlScheme GetControlSchemeForPlayer(int playerNumber) {
        return playerControlSchemes[playerNumber - 1];
    }

}
