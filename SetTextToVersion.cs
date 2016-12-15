using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SetTextToVersion : MonoBehaviour {

    private void Start() {
        GetComponent<Text>().text = Application.version;
    }

}
