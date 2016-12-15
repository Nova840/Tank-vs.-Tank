using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CountdownInText : MonoBehaviour {

    //not assigned in the editor for dead text, but could be for other uses
    public float countdownTime;

    [SerializeField]
    private float countdownMin = 0;

    [SerializeField]
    private string stringToReplaceNumber = "#", stringToReplacePlurals = "(s)";

    private Text text;

    private string originalString;

    private void Awake() {
        text = GetComponent<Text>();
        originalString = text.text;
    }

    private void Update() {
        countdownTime -= Time.deltaTime;
        int numToDisplay = Mathf.CeilToInt(Mathf.Max(countdownMin, countdownTime));
        text.text = originalString.
            Replace(stringToReplaceNumber, numToDisplay.ToString()).
            Replace(stringToReplacePlurals, numToDisplay == 1 ? "" : "s");
    }

}
