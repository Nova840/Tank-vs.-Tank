using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    [SerializeField]
    private GameObject pausedCanvasPrefab = null, exitButton = null;
    private GameObject pausedCanvas;
    private float originalTimeScale = 1;

    private bool paused = false;

    private void Update() {
        if (ShouldPause())
            PauseGame(!paused);
    }

    public void PauseGame(bool pause) {
        exitButton.SetActive(pause);
        if (!paused && pause) {
            originalTimeScale = Time.timeScale;
            Time.timeScale = 0;
            pausedCanvas = Instantiate(pausedCanvasPrefab);
        } else if (paused && !pause) {
            Time.timeScale = originalTimeScale;
            Destroy(pausedCanvas);
        }
        paused = pause;
    }

    private void OnApplicationFocus(bool focusStatus) {
        if (!focusStatus)
            PauseGame(true);
    }

    private bool ShouldPause() {
        return (Input.GetButtonDown(Controls.allStartButtonsAxes) && !Input.GetButtonDown(Controls.keyboard.Start))
            || Input.GetButtonDown(Controls.keyboard.Back);//all start axes with keyboard's start and back switched
    }

}
