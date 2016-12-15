using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class StartOverlay : MonoBehaviour {

    private RectTransform rectTransform;
    private Vector2 originalPosition;
    [SerializeField]
    private float lerpRate = 5;
    [SerializeField]
    private bool showOnTitle = false, showOnPlayers = false, showOnStageSelect = false, moveX = false, moveY = false;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    private void Start() {
        Move(false);
    }

    private void Update() {
        Move(true);
    }

    private void Move(bool lerp) {
        bool offScreen = !ShouldBeOnScreen();

        float x = originalPosition.x;
        float y = originalPosition.y;

        if (offScreen && moveX)
            x = -x;
        if (offScreen && moveY)
            y = -y;

        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, new Vector2(x, y), lerp ? lerpRate * Time.deltaTime : 1);
    }

    private bool ShouldBeOnScreen() {
        return (Players.NumberOfPlayers <= 0 && showOnTitle) ||
            (Players.NumberOfPlayers > 0 && !StageSelectController.stageSelect && showOnPlayers) ||
            (StageSelectController.stageSelect && showOnStageSelect);
    }

}
