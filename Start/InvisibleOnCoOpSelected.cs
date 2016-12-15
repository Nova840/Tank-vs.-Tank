using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class InvisibleOnCoOpSelected : MonoBehaviour {

    private Renderer thisRenderer;

    private void Awake() {
        thisRenderer = GetComponent<Renderer>();
    }

    private void Update() {
        thisRenderer.enabled = !CycleButtons.IsCoOp();
    }

}
