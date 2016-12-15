using UnityEngine;
using System.Collections;

public class TimeEvent : Event {

    [SerializeField]
    private float timeScale = 2, changeSpeed = .01f;

    //duration of this event is in scaled time

    private void FixedUpdate() {
        float targetTimeScale = HasDurationFinished() ? 1 : timeScale;
        Time.timeScale = Mathf.MoveTowards(Time.timeScale, targetTimeScale, changeSpeed);
        if (Time.timeScale == 1)
            Destroy(gameObject);
    }

}
