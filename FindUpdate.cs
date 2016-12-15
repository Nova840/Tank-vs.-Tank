using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class FindUpdate : MonoBehaviour {

    [SerializeField]
    private Text updateAvailableText;

    private static bool lookedForUpdate = false;
    private static string newVersion = "";

    private IEnumerator Start() {
        if (lookedForUpdate)
            yield break;
        WWW www = new WWW("http://nova840.github.io/tank/version");
        yield return www;
        lookedForUpdate = true;
        newVersion = www.text.TrimEnd('\r', '\n');
        if (Regex.IsMatch(newVersion, @"^\d+[.]\d+[.]\d+$") && newVersion != Application.version)
            updateAvailableText.text = "Version " + newVersion + " Available";
    }

}
