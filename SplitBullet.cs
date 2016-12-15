using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class SplitBullet : MonoBehaviour {

    [SerializeField]
    private GameObject splitInto = null;

    [SerializeField]
    private float splitForce = 100f, splitTime = 1.5f;

    [SerializeField]
    private bool splitVertical = false;

    private Rigidbody rbInherit = null;
    private Explodeable explodeable;

    private void Awake() {
        rbInherit = GetComponent<Rigidbody>();
        explodeable = GetComponent<Explodeable>();
        Invoke("Split", splitTime);
    }

    private void Split() {
        GameObject b1 = MakeBullet(splitForce);
        GameObject b2 = MakeBullet(-splitForce);
        Physics.IgnoreCollision(b1.GetComponent<Collider>(), b2.GetComponent<Collider>());
        transform.Find("Trail").parent = null;
        Destroy(gameObject);
    }

    private GameObject MakeBullet(float force) {
        GameObject g = (GameObject)Instantiate(splitInto, transform.position, transform.rotation);
        g.GetComponent<Explodeable>().playerNumber = explodeable.playerNumber;
        Rigidbody rb = g.GetComponent<Rigidbody>();
        rb.velocity = rbInherit.velocity;
        rb.AddForce((splitVertical ? transform.up : transform.right) * force);
        return g;
    }

}
