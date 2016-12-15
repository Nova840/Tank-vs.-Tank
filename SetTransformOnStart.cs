using UnityEngine;
using System.Collections;

public class SetTransformOnStart : MonoBehaviour {

    [SerializeField]
    private string playerTransformPath;

    private void Start() {//Awake happens before the parent is set in Health
        Transform playerTransform = transform.parent.parent.Find(playerTransformPath);
        transform.position = playerTransform.position;
        transform.eulerAngles = playerTransform.eulerAngles;
        transform.parent = null;
    }

}
