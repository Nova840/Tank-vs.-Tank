using UnityEngine;
using System.Collections;

public class UnregisterIfControllerDisconnected : MonoBehaviour {

    private static int numLastControllers;

    [SerializeField]
    private SpawnFakePlayers spawner;

    [SerializeField]
    private GameObject disconnectedText;

    [SerializeField]
    private float flashTextTime = 5;

    private void Awake() {//should happen before Start in StartPlayersCamera
        CheckControllers();
        disconnectedText.SetActive(false);
    }

    private void Update() {
        CheckControllers();
    }

    private void CheckControllers() {
        int numCurrentControllers = Controls.NumberOfControllers();
        if (numLastControllers > numCurrentControllers) {//If disconnected
            for (int i = 0; i < Players.playerControlSchemes.Length; i++)
                Players.playerControlSchemes[i] = null;
            numLastControllers = 0;
            StageSelectController.stageSelect = false;
            spawner.DespawnAllPlayers();
            StartCoroutine(FlashText());
        } else {
            numLastControllers = numCurrentControllers;
        }
    }

    private IEnumerator FlashText() {
        disconnectedText.SetActive(true);
        yield return new WaitForSeconds(flashTextTime);
        disconnectedText.SetActive(false);
    }

}
