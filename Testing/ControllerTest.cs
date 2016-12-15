using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ControllerTest : MonoBehaviour {

    [SerializeField]
    private bool axes = true, buttons = true;

    private Text text;

    private void Awake() {
        text = GetComponent<Text>();
    }

    private void Update() {
        text.text = "";
        string str = "";

        if (axes)
            str +=
                "1: " + Input.GetAxisRaw("1") + "\n" +
                "2: " + Input.GetAxisRaw("2") + "\n" +
                "3: " + Input.GetAxisRaw("3") + "\n" +
                "4: " + Input.GetAxisRaw("4") + "\n" +
                "5: " + Input.GetAxisRaw("5") + "\n" +
                "6: " + Input.GetAxisRaw("6") + "\n" +
                "7: " + Input.GetAxisRaw("7") + "\n" +
                "8: " + Input.GetAxisRaw("8") + "\n" +
                "9: " + Input.GetAxisRaw("9") + "\n" +
                "10: " + Input.GetAxisRaw("10") + "\n" +
                "11: " + Input.GetAxisRaw("11") + "\n" +
                "12: " + Input.GetAxisRaw("12") + "\n" +
                "13: " + Input.GetAxisRaw("13") + "\n" +
                "14: " + Input.GetAxisRaw("14") + "\n" +
                "15: " + Input.GetAxisRaw("15") + "\n" +
                "16: " + Input.GetAxisRaw("16") + "\n" +
                "17: " + Input.GetAxisRaw("17") + "\n" +
                "18: " + Input.GetAxisRaw("18") + "\n" +
                "19: " + Input.GetAxisRaw("19") + "\n" +
                "20: " + Input.GetAxisRaw("20") + "\n" +
                "21: " + Input.GetAxisRaw("21") + "\n" +
                "22: " + Input.GetAxisRaw("22") + "\n" +
                "23: " + Input.GetAxisRaw("23") + "\n" +
                "24: " + Input.GetAxisRaw("24") + "\n" +
                "25: " + Input.GetAxisRaw("25") + "\n" +
                "26: " + Input.GetAxisRaw("26") + "\n" +
                "27: " + Input.GetAxisRaw("27") + "\n" +
                "28: " + Input.GetAxisRaw("28") + "\n"
                ;

        if (buttons)
            str +=
                "0: " + Input.GetAxisRaw("B0") + "\n" +
                "1: " + Input.GetAxisRaw("B1") + "\n" +
                "2: " + Input.GetAxisRaw("B2") + "\n" +
                "3: " + Input.GetAxisRaw("B3") + "\n" +
                "4: " + Input.GetAxisRaw("B4") + "\n" +
                "5: " + Input.GetAxisRaw("B5") + "\n" +
                "6: " + Input.GetAxisRaw("B6") + "\n" +
                "7: " + Input.GetAxisRaw("B7") + "\n" +
                "8: " + Input.GetAxisRaw("B8") + "\n" +
                "9: " + Input.GetAxisRaw("B9") + "\n" +
                "10: " + Input.GetAxisRaw("B10") + "\n" +
                "11: " + Input.GetAxisRaw("B11") + "\n" +
                "12: " + Input.GetAxisRaw("B12") + "\n" +
                "13: " + Input.GetAxisRaw("B13") + "\n" +
                "14: " + Input.GetAxisRaw("B14") + "\n" +
                "15: " + Input.GetAxisRaw("B15") + "\n" +
                "16: " + Input.GetAxisRaw("B16") + "\n" +
                "17: " + Input.GetAxisRaw("B17") + "\n" +
                "18: " + Input.GetAxisRaw("B18") + "\n" +
                "19: " + Input.GetAxisRaw("B19") + "\n"
                ;

        str = str.TrimEnd('\r', '\n');
        text.text = str;
    }

}
