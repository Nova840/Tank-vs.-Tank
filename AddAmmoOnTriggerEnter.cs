using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class AddAmmoOnTriggerEnter : MonoBehaviour {

    [System.Serializable]
    public class BulletsToAdd {
        public BulletType type;
        public int amount = 1;
    }

    public BulletsToAdd[] bullets;

    [SerializeField]
    private bool destroyGameObject = false;

    private List<GameObject> playersEntered = new List<GameObject>();

    private void OnTriggerEnter(Collider other) {
        GameObject root = other.transform.root.gameObject;
        if (!root.CompareTag("Player") || playersEntered.Contains(root) || !root.GetComponent<UserControl>())
            return;

        playersEntered.Add(root);

        CarBullets carBullets = root.GetComponent<CarBullets>();

        foreach (BulletsToAdd b in bullets)
            carBullets.AddBullets(b.type, b.amount);

        if (destroyGameObject)
            Destroy(gameObject);
    }

}
