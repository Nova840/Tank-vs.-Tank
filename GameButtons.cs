using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameButtons : MonoBehaviour {

    public void OnExitButtonClick() {
        SceneManager.LoadScene("Start");
    }

}
