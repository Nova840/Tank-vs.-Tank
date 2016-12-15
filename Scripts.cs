using UnityEngine;
using System.Collections;

public class Scripts : MonoBehaviour {

    private static GameObject reference;
    public static GameObject Reference { get { return reference; } }

    private static Players players;
    public static Players Players { get { return players; } }

    private static GameObject gameOverlay;
    public static GameObject GameOverlay { get { return gameOverlay; } }

    private static PlayerUI playerUI;
    public static PlayerUI PlayerUI { get { return playerUI; } }

    private static SpawnPlayers spawnPlayers;
    public static SpawnPlayers SpawnPlayers { get { return spawnPlayers; } }

    private void Awake() {
        reference = gameObject;
        players = GetComponent<Players>();
        gameOverlay = GameObject.Find("Game Overlay Canvas");
        if (gameOverlay)
            playerUI = gameOverlay.GetComponent<PlayerUI>();
        spawnPlayers = GetComponent<SpawnPlayers>();
    }

}
