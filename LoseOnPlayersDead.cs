using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[RequireComponent(typeof(LevelController))]
public class LoseOnPlayersDead : MonoBehaviour {

    [SerializeField]
    private Text loseText;

    [SerializeField]
    private string message = "LEVEL FAILED";

    [SerializeField]
    private float loseDuration = 3;

    private List<Health> healths = new List<Health>();

    private bool lost = false;

    private void Start() {
        foreach (GameObject player in Scripts.Players.players)
            healths.Add(player.GetComponent<Health>());
    }

    private void Update() {
        if (!lost && AllPlayersDead())
            LoseGame();
    }

    private bool AllPlayersDead() {
        foreach(Health health in healths) {
            if (!health.IsDead())
                return false;
        }
        return true;
    }

    private void LoseGame() {
        lost = true;
        loseText.gameObject.SetActive(true);
        loseText.text = message;
        GetComponent<LevelController>().enabled = false;
        StartCoroutine(ReturnToStart(loseDuration));
    }

    private IEnumerator ReturnToStart(float seconds) {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("Start");
    }
}
