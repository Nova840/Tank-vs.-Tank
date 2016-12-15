using UnityEngine;
using System.Collections;

public class PlayersCanRespawn : MonoBehaviour {

    private void Start() {
        foreach (GameObject player in Scripts.Players.players)
            player.GetComponent<PlayerSpawn>().ableToRespawn = true;
    }

}
