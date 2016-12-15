using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]//collider for OnTriggerStay
public class SlowExplosion : Explosion {

    [SerializeField]
    private float damageInterval = .25f, fadeAfter = 5;

    private Dictionary<Health, float> healthTimes = new Dictionary<Health, float>();
    private float timeAwoken;

    private new void Awake() {
        base.Awake();
        timeAwoken = Time.time;
    }

    private void FixedUpdate() {
        if (ShouldFade())
            Fade();
        Expand();
    }

    private bool ShouldFade() {
        return timeAwoken + fadeAfter <= Time.time;
    }

    private void OnTriggerStay(Collider other) {
        GameObject root = other.transform.root.gameObject;

        if (ShouldFade())
            return;

        if (root.CompareTag("Target"))
            Destroy(root);
        else if (root.CompareTag("Player"))
            AddDamage(root.GetComponent<Health>());//Don't need to do distinct root gameobjects because the timer will prevent duplicates.
    }

    private void AddDamage(Health health) {
        if (!healthTimes.ContainsKey(health)) {
            healthTimes.Add(health, Time.time);
            health.CurrentHealth -= 1;
        } else if (healthTimes[health] + damageInterval < Time.time) {
            health.CurrentHealth -= 1;
            healthTimes[health] = Time.time;
        }
    }

}
