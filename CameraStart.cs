using UnityEngine;
using System.Collections;

public class CameraStart : MonoBehaviour {

    private void Start() {
        name = transform.root.name + " Camera";
        transform.parent = null;
        Camera cam = GetComponent<Camera>();

        if (name == "Player 1 Camera") {
            GetComponent<AudioListener>().enabled = true;
            cam.cullingMask = ~((1 << 12) | (1 << 13) | (1 << 14));
        } else if (name == "Player 2 Camera") {
            cam.cullingMask = ~((1 << 11) | (1 << 13) | (1 << 14));
        } else if (name == "Player 3 Camera") {
            cam.cullingMask = ~((1 << 11) | (1 << 12) | (1 << 14));
        } else if (name == "Player 4 Camera") {
            cam.cullingMask = ~((1 << 11) | (1 << 12) | (1 << 13));
        }

        //can simplify the following
        int numberOfPlayers = Players.NumberOfPlayers;

        if (numberOfPlayers == 2) {

            Rect rect = new Rect(0, 0, .5f, 1);
            if (name == Scripts.Players.players[0].name + " Camera") {
                GetComponent<AudioListener>().enabled = true;
            } else if (name == Scripts.Players.players[1].name + " Camera") {
                rect.x = .5f;
            }

            cam.rect = rect;

        } else if (numberOfPlayers == 3) {

            Rect rect = new Rect(0, 0, .5f, .5f);
            if (name == Scripts.Players.players[0].name + " Camera") {
                GetComponent<AudioListener>().enabled = true;
                rect.y = .5f;
            } else if (name == Scripts.Players.players[1].name + " Camera") {
                rect.x = .5f;
                rect.y = .5f;
            } else if (name == Scripts.Players.players[2].name + " Camera") {
                //x and y stay 0
            }

            cam.rect = rect;

        } else if (numberOfPlayers == 4) {

            Rect rect = new Rect(0, 0, .5f, .5f);
            if (name == Scripts.Players.players[0].name + " Camera") {
                GetComponent<AudioListener>().enabled = true;
                rect.y = .5f;
            } else if (name == Scripts.Players.players[1].name + " Camera") {
                rect.x = .5f;
                rect.y = .5f;
            } else if (name == Scripts.Players.players[2].name + " Camera") {
                //x and y stay 0
            } else if (name == Scripts.Players.players[3].name + " Camera") {
                rect.x = .5f;
            }

            cam.rect = rect;

        }

    }

}
