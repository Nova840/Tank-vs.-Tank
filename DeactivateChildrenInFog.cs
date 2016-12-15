using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeactivateChildrenInFog : MonoBehaviour {

    private bool childerenActive = true;

    private List<GameObject> children = new List<GameObject>();

    private void Awake() {
        foreach (Transform t in GetComponentsInChildren<Transform>())
            if (t != transform)
                children.Add(t.gameObject);
    }

    private void Update() {
        if (childerenActive && RenderSettings.fog)
            SetChildrenActive(false);
        else if (!childerenActive && !RenderSettings.fog)
            SetChildrenActive(true);
    }

    private void SetChildrenActive(bool active) {
        childerenActive = active;
        foreach (GameObject g in children)
            g.SetActive(active);
    }

}
