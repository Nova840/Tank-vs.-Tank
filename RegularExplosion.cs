using UnityEngine;
using System.Collections;
using System.Linq;

//require Renderer in parent class
public class RegularExplosion : Explosion {

    [SerializeField]
    private float explosionForce = 20000, upwardsModifier = 0;
    [SerializeField]
    private ForceMode forceMode = ForceMode.Impulse;
    [SerializeField]
    private int damage = 10;

    private new void Start() {
        base.Start();

        //saving the distinct roots into a variable prevents newly instantiated objects (like tank parts) from being affected by the explosion.
        foreach (GameObject g in DistinctRootGameObjects(Physics.OverlapSphere(transform.position, ExplosionRadius)))
            if (g.CompareTag("Player"))
                g.GetComponent<Health>().CurrentHealth -= damage;
            else if (g.CompareTag("Target"))
                Destroy(g);


        foreach (GameObject g in DistinctRootGameObjects(Physics.OverlapSphere(transform.position, ExplosionRadius)))
            foreach (Rigidbody r in g.GetComponentsInChildren<Rigidbody>())
                if (!IsSamePlayerBullet(r.gameObject))
                    r.AddExplosionForce(explosionForce, transform.position, ExplosionRadius, upwardsModifier, forceMode);

    }

    private void FixedUpdate() {
        Fade();
        Expand();
    }

    private static GameObject[] DistinctRootGameObjects(Component[] components) {
        GameObject[] roots = new GameObject[components.Length];
        for (int i = 0; i < components.Length; i++)
            roots[i] = components[i].transform.root.gameObject;
        return roots.Distinct().ToArray();
    }

}
