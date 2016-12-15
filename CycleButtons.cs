using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CycleButtons : MonoBehaviour {

    [System.Serializable]
    private class StartButton {
        [SerializeField]
        private GameObject gameObject;
        private Button button;
        private Image image;
        [SerializeField]
        private int minPlayers;

        public void Assign() {//gameObject == null if assigned in constructor
            button = gameObject.GetComponent<Button>();
            image = gameObject.GetComponent<Image>();
        }

        public GameObject GameObject { get { return gameObject; } }
        public Button Button { get { return button; } }
        public Image Image { get { return image; } }
        public int MinPlayers { get { return minPlayers; } }
    }

    [SerializeField]
    private List<StartButton> startButtons = new List<StartButton>();

    [SerializeField]
    private bool reverseDirection = false;

    private bool moved = false;

    [SerializeField]
    private int startingIndex = 0;
    private static int currentButtonIndex = -1;
    public static bool IsCoOp() {
        return currentButtonIndex == 0;//co-op is index 0
    }

    [SerializeField]
    private Color highlightColor = Color.white;

    private void Awake() {
        if (currentButtonIndex == -1)
            currentButtonIndex = startingIndex;
        foreach (StartButton startButton in startButtons)
            startButton.Assign();
        CalculateButtons();
    }

    private void Update() {
        if (StageSelectController.stageSelect || Players.NumberOfPlayers <= 0)
            return;

        int allVertical = Mathf.Clamp((int)(Input.GetAxisRaw(Controls.allVerticalAxes) - Input.GetAxisRaw(Controls.keyboard.LookVertical)), -1, 1);//also scroll for keyboard up/down

        if (reverseDirection)
            allVertical = -allVertical;

        if (!moved && allVertical != 0)//so it only cycles once and doesn't do extra work
            Cycle(allVertical);

        moved = allVertical >= 1 || allVertical <= -1; //a number outside of -1 to 1 inclusive

        CalculateInteractable();

        if (Input.GetButtonDown(Controls.allStartButtonsAxes) && Players.NumberOfPlayers >= startButtons[currentButtonIndex].MinPlayers)
            startButtons[currentButtonIndex].Button.onClick.Invoke();
    }

    private void Cycle(int amount) {
        currentButtonIndex += amount;
        currentButtonIndex = Mathf.Clamp(currentButtonIndex, 0, startButtons.Count - 1);
        CalculateButtons();
    }

    private void CalculateButtons() {
        for (int i = 0; i < startButtons.Count; i++)
            startButtons[i].Image.color = currentButtonIndex == i ? highlightColor : Color.white;
    }

    private void CalculateInteractable() {
        foreach (StartButton startButton in startButtons)
            startButton.Button.interactable = Players.NumberOfPlayers >= startButton.MinPlayers;
    }

    public void SetCurrentButtonIndex(int index) {//for buttons in case they're clicked
        currentButtonIndex = index;
        CalculateButtons();
    }

}
