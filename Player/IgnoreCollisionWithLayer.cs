using UnityEngine;
using System.Collections;
using System.Linq;

public class IgnoreCollisionWithLayer : MonoBehaviour {

    [SerializeField]
    private int layer = 10;

    private void Awake() {
        Collider[] theseColliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in FindObjectsOfType(typeof(Collider)) as Collider[]) {//all colliders in scene
            //this will happen for gameobjects with 2 or more colliders as well, but it's faster than trying to get the distinct gameobjects
            if (collider.gameObject.layer == layer) {//if in layer
                foreach (Collider thisCollider in theseColliders) {//ignore collision with these colliders
                    if (!thisCollider.CompareTag("Camera") && !thisCollider.CompareTag("Aim Guide")) {//unless it's a camera or an aim guide
                        Physics.IgnoreCollision(collider, thisCollider);
                    }
                }
            }
        }
    }
    /*//works but unused
    private static Collider[] DistinctGameObjects(Collider[] colliders) {
        GameObject[] gameObjects = new GameObject[colliders.Length];
        for (int i = 0; i < colliders.Length; i++)
            gameObjects[i] = colliders[i].gameObject;
        gameObjects = gameObjects.Distinct().ToArray();

        Collider[] newColliders = new Collider[gameObjects.Length];
        for (int i = 0; i < newColliders.Length; i++)
            newColliders[i] = gameObjects[i].GetComponent<Collider>();
        return newColliders;
    }
    */
}
