using UnityEngine;
using System.Collections;

public class CreateHealthbars : MonoBehaviour {

    [SerializeField]
    private GameObject HealthbarPrefab;

    private void Start() {
        StartCoroutine(Create());
    }

    private IEnumerator Create() {
        yield return new WaitForEndOfFrame();//cameras that come after player haven't been created yet.
        foreach (GameObject player in Scripts.Players.players) {
            if (player == transform.root.gameObject)
                continue;

            GameObject healthbar = Instantiate(HealthbarPrefab);
            healthbar.transform.SetParent(transform, false);
            int playerNumber = Players.PlayerNumber(player);
            healthbar.layer = playerNumber + 10;
            healthbar.GetComponent<LookAtCamera>().cameraTransform = GameObject.Find("Player " + playerNumber + " Camera").transform;
        }
    }

}
