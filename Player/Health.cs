using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Health : MonoBehaviour {

    [SerializeField]
    private int startingHealth = 100;
    private int currentHealth;

    [SerializeField]
    private GameObject destroyedPrefab;

    [SerializeField]
    private bool destroyGameObject = true;

    private GameObject healthbars;

    public int StartingHealth { get { return startingHealth; } }

    [HideInInspector]
    public PlayerUI playerUI;//set for all players in PlayerUI

    private bool isAI;

    public int CurrentHealth {
        get { return currentHealth; }
        set {
            if (value <= 0) {
                value = 0;
                if (currentHealth > 0)
                    Die();
            }
            currentHealth = value;
        }
    }

    private void Awake() {
        healthbars = transform.Find("Healthbars").gameObject;
        isAI = GetComponent<AIControl>();
        currentHealth = startingHealth *= isAI ? Players.NumberOfPlayers : 1;
    }

    private void Die() {
        if (!isAI)
            playerUI.DisplayRespawn(Players.PlayerIndex(gameObject));

        if (destroyedPrefab != null) {
            GameObject g = (GameObject)Instantiate(destroyedPrefab, transform.position, transform.rotation);
            g.transform.parent = transform;
            foreach (Rigidbody r in g.GetComponentsInChildren<Rigidbody>())
                r.velocity = GetComponent<Rigidbody>().velocity;
            healthbars.SetActive(false);
        }

        if (destroyGameObject) {
            Destroy(gameObject);
            return;//Don't think this is necessary since this instance is destroyed.
        }

        GetComponent<Rigidbody>().isKinematic = true;
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
            if (FindParentWithTag(r.transform, "Tank Parts") == null)
                r.enabled = false;
        foreach (Collider c in GetComponentsInChildren<Collider>())
            if (FindParentWithTag(c.transform, "Tank Parts") == null)
                c.enabled = false;
    }

    public static Transform FindParentWithTag(Transform t, string tag) {
        while (t.parent != null) {
            if (t.parent.tag == tag)
                return t.parent;
            t = t.parent;
        }
        return null; // Could not find a parent with given tag.
    }

    public bool IsDead() {
        return currentHealth <= 0;
    }

}
