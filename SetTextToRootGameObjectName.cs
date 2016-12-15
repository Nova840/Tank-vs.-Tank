using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SetTextToRootGameObjectName : MonoBehaviour {

    private void Start() {
        GetComponent<Text>().text = transform.root.name;
    }

}
