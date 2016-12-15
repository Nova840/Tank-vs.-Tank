using UnityEngine;
using System.Collections;

public class SetTimeScaleOnAwake : MonoBehaviour {

    [SerializeField]
    private float timeScale = 1;

    private void Awake() {
        Time.timeScale = timeScale;
    }

}
