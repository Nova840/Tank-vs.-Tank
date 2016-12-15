using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Event : MonoBehaviour {

    [SerializeField]
    protected float duration = 30;//"Infinity" will make the event last forever
    protected float startTime;

    [SerializeField]
    protected string eventMessage;
    [SerializeField]
    protected GameObject alertPrefab;

    protected void Start() {
        startTime = Time.time;
        Instantiate(alertPrefab).GetComponentInChildren<Text>().text = eventMessage;
    }

    protected bool HasDurationFinished() {
        return Time.time >= startTime + duration;
    }

}
