using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]//collider for OnTriggerEnter
public class TriggerExplosion : Explosion {
    [SerializeField]
    private int damage = 10;
    [SerializeField]
    private float explosionForce = 20000, upwardsModifier = 0;
    [SerializeField, Range(0, 1)]
    private float transparencyThreshold = .2f;
    [SerializeField]
    private ForceMode forceMode = ForceMode.Impulse;

    private List<GameObject> alreadyHit = new List<GameObject>();

    private new void Awake() {
        base.Awake();
    }

    private void FixedUpdate() {
        Fade();
        Expand();
    }

    private void OnTriggerEnter(Collider other) {
        if (mat.GetColor("_TintColor").a < transparencyThreshold)
            return;

        GameObject root = other.transform.root.gameObject;

        if (root.CompareTag("Target")) {
            Destroy(root);
            return;
        }

        if (alreadyHit.Contains(root) || IsSamePlayerBullet(other.gameObject))
            return;

        Rigidbody rb = root.GetComponent<Rigidbody>();
        if (rb)
            rb.AddExplosionForce(explosionForce, transform.position,
                (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3, upwardsModifier, forceMode);

        if (root.CompareTag("Player"))
            root.GetComponent<Health>().CurrentHealth -= damage;

        alreadyHit.Add(root);
    }

}
