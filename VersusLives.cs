using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(SpawnPlayers))]
public class VersusLives : MonoBehaviour {

    [SerializeField]
    private GameObject completeText;

    [SerializeField]
    private int startingLives = 5;
    private int[] lives;
    public int[] Lives { get { return lives; } }
    private bool[] lastAlive;

    [SerializeField]
    private float respawnTime = 4, endDelay = 3;
    public float RespawnTime { get { return respawnTime; } }

    private bool finished = false;

    private void Start() {
        lives = Enumerable.Repeat(startingLives, Scripts.Players.players.Count).ToArray();
        lastAlive = CalculateAlive();
        for (int i = 0; i < lives.Length; i++)
            SetPlayerLives(i, lives[i]);
    }

    private void Update() {
        if (finished)
            return;

        bool[] alive = CalculateAlive();
        for (int i = 0; i < alive.Length; i++) {
            if (!alive[i] && lastAlive[i]) {//if just died
                lives[i]--;
                SetPlayerLives(i, lives[i]);
                if (lives[i] >= 0)
                    StartCoroutine(RespawnPlayer(i));
            }
        }
        lastAlive = alive;

        bool anyWithLives;
        int winnerIndex = WinnerIndex(out anyWithLives);

        if (winnerIndex != -1)
            Win("Player " + Players.PlayerNumber(Scripts.Players.players[winnerIndex]) + " Wins!");
        else if (!anyWithLives)
            Win("Tie!");
    }

    private static void SetPlayerLives(int playerIndex, int lives) {
        Scripts.PlayerUI.PlayerScreens[playerIndex].GetComponentInChildren<DisplayLives>().SetLives(lives);
    }

    private int WinnerIndex(out bool anyWithLives) {
        int winnerIndex = -1;
        anyWithLives = false;
        for (int i = 0; i < lives.Length; i++) {
            if (lives[i] != 0) {
                anyWithLives = true;
                if (winnerIndex == -1)
                    winnerIndex = i;
                else
                    return -1;
            }
        }
        return winnerIndex;
    }

    private void Win(string message) {
        completeText.SetActive(true);
        completeText.GetComponent<Text>().text = message;
        finished = true;
        StartCoroutine(ReturnToStart());
    }

    private IEnumerator ReturnToStart() {
        yield return new WaitForSeconds(endDelay);
        SceneManager.LoadScene("Start");
    }

    private bool[] CalculateAlive() {
        List<GameObject> players = Scripts.Players.players;
        bool[] alive = new bool[players.Count];
        for (int i = 0; i < players.Count; i++)
            alive[i] = !players[i].GetComponent<Health>().IsDead();
        return alive;
    }

    private IEnumerator RespawnPlayer(int playerIndex) {
        yield return new WaitForSeconds(respawnTime);
        Scripts.Players.players[playerIndex].GetComponent<PlayerSpawn>().Spawn();
    }

}
