using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartButtons : MonoBehaviour {

    [SerializeField]
    private GameObject exitConfirmationPrefab;
    private GameObject exitConfirmation = null;

    public void OnStartButtonClick(string sceneToLoad) {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void OnVolumeSliderValueChanged(float value) {
        AudioListener.volume = value;
    }

    public void OnFullscreenButtonClick() {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void OnExitButtonClick() {
        if (!exitConfirmation)
            exitConfirmation = Instantiate(exitConfirmationPrefab);
    }

}
