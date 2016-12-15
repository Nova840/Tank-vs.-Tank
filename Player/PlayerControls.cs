using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

    [HideInInspector]
    public ControlScheme controls;

    private void Start() {//works because SpawnPlayers is set to happen earlier in the script execution order
        if (name == "Player 1")
            controls = Players.GetControlSchemeForPlayer(1);
        else if (name == "Player 2")
            controls = Players.GetControlSchemeForPlayer(2);
        else if (name == "Player 3")
            controls = Players.GetControlSchemeForPlayer(3);
        else if (name == "Player 4")
            controls = Players.GetControlSchemeForPlayer(4);
    }
}
