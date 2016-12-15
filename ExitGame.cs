using UnityEngine;
using System.Collections;

public class ExitGame : MonoBehaviour {

    private void Update() {
        if (Input.GetButtonDown(Controls.allBackButtonsAxes))
            Destroy(gameObject);
        else if (Input.GetButtonDown(Controls.allStartButtonsAxes))
            Application.Quit();
    }

}
