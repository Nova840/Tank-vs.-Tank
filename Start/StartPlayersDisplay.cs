using UnityEngine;
using System.Collections;
using System.Linq;

public class StartPlayersDisplay : MonoBehaviour {

    [SerializeField]
    private GameObject player1Display = null, player2Display = null, player3Display = null, player4Display = null;
    private GameObject[] displays = new GameObject[4];

    private GameObject[] playerJoinControls = new GameObject[4];
    private GameObject[] playerJoinControlsA = new GameObject[4];
    private GameObject[] playerLeaveControlsY = new GameObject[4];
    private GameObject[] playerLeaveControlsR = new GameObject[4];

    private void Awake() {
        displays[0] = player1Display;
        displays[1] = player2Display;
        displays[2] = player3Display;
        displays[3] = player4Display;

        for (int i = 0; i < 4; i++) {
            playerJoinControls[i] = displays[i].transform.Find("Join Controls").gameObject;
            playerJoinControlsA[i] = displays[i].transform.Find("Join Controls (A)").gameObject;
            playerLeaveControlsY[i] = displays[i].transform.Find("Leave Controls (Y)").gameObject;
            playerLeaveControlsR[i] = displays[i].transform.Find("Leave Controls (R)").gameObject;
        }
    }

    private void Update() {

        bool foundEmpty = false;
        bool hasKeyboard = Players.playerControlSchemes.Any(c => c == Controls.keyboard);

        for (int i = 0; i < 4; i++) {
            ControlScheme controlScheme = Players.playerControlSchemes[i];

            if (!controlScheme && !foundEmpty) {
                playerJoinControls[i].SetActive(!hasKeyboard);
                playerJoinControlsA[i].SetActive(hasKeyboard);
                foundEmpty = true;
            } else {
                playerJoinControls[i].SetActive(false);
                playerJoinControlsA[i].SetActive(false);
            }

            playerLeaveControlsY[i].SetActive(controlScheme && controlScheme != Controls.keyboard);
            playerLeaveControlsR[i].SetActive(controlScheme && controlScheme == Controls.keyboard);
        }

    }

}
