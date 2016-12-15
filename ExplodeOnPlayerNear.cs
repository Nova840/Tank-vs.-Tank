using UnityEngine;
using System.Collections;

public class ExplodeOnPlayerNear : Explodeable {

    private RegularExplosion explosion;
    private bool activated = false;

    [SerializeField]
    private float activateDelay = 2, explodeAfterTime = 30;

    private void Awake() {
        explosion = explosionPrefab.GetComponent<RegularExplosion>();
        Invoke("Explode", explodeAfterTime);
    }

    private void OnCollisionEnter() {
        GetComponent<Rigidbody>().isKinematic = true;
        StartCoroutine(ActivateAfterTime(activateDelay));
    }

    private IEnumerator ActivateAfterTime(float seconds) {
        yield return new WaitForSeconds(seconds);
        activated = true;
    }

    private void Update() {
        if (!activated)
            return;

        foreach (GameObject player in Scripts.Players.players) {
            if (WouldHit(player.transform.position))
                Explode();
        }
        foreach (GameObject enemy in Scripts.Players.enemies) {
            if (WouldHit(enemy.transform.position))
                Explode();
        }
    }

    private bool WouldHit(Vector3 position) {
        return Vector3.Distance(position, transform.position) <= explosion.ExplosionRadius;
    }

}
