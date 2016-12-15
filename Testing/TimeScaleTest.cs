using UnityEngine;
using System.Collections;

public class TimeScaleTest : MonoBehaviour {

    [SerializeField, Range(0, 100)]
    private float scale = 1;

    [SerializeField, Range(0, 1)]
    private float scale01 = 1;

    void Update() {
        Time.timeScale = scale * scale01;
    }

}
