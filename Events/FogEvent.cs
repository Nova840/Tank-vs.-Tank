using UnityEngine;
using System.Collections;

public class FogEvent : Event {
    //assumes fog is set to exponential squared
    [SerializeField]
    private float density = .02f, fillSpeed = 1e-5f;

    //original density set in lighting
    private float originalDensity;

    private new void Start() {
        base.Start();
        originalDensity = RenderSettings.fogDensity;
        RenderSettings.fog = true;
    }

    private void OnDestroy() {
        RenderSettings.fog = false;
    }

    private void Update() {
        float targetDensity = HasDurationFinished() ? originalDensity : density;
        RenderSettings.fogDensity = Mathf.MoveTowards(RenderSettings.fogDensity, targetDensity, fillSpeed);

        if (RenderSettings.fogDensity == originalDensity)
            Destroy(gameObject);
    }

}
