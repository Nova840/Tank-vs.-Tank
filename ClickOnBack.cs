using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ClickOnBack : MonoBehaviour {

    private Button button;

    [SerializeField]
    private bool switchKeyboard = false;

    private void Awake() {
        button = GetComponent<Button>();
    }

    private void Update() {
        if (ShouldClick())
            button.onClick.Invoke();
    }

    private bool ShouldClick() {
        bool allBackButtons = Input.GetButtonDown(Controls.allBackButtonsAxes);
        if (!switchKeyboard)
            return allBackButtons;
        return (allBackButtons && !Input.GetButtonDown(Controls.keyboard.Back))
            || Input.GetButtonDown(Controls.keyboard.Start);//all back axes with keyboard's start and back switched;
    }

}
