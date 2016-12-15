using UnityEngine;
using System.Collections;

public abstract class Explodeable : MonoBehaviour {

    [SerializeField]
    protected GameObject explosionPrefab;
    public GameObject ExplosionPrefab { get { return explosionPrefab; } }
    [HideInInspector]
    public int playerNumber = 0;

    protected void Explode() {
        GameObject bullet = (GameObject)Instantiate(explosionPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Explosion>().playerNumber = playerNumber;
        Destroy(gameObject);
    }

    private void OnDestroy() {
        Transform trail = transform.Find("Trail");
        if (trail != null)//Sometimes get messages about one of these being null. Prevents some of those errors.
            trail.parent = null;
    }

}
