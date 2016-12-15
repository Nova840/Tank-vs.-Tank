using UnityEngine;
using System.Collections;

public class Geyser : Explodeable {

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float minUpForce = 10000, maxUpForce = 50000, maxAngle = 5;
    [SerializeField]
    private int numBullets = 4;

    private void OnCollisionEnter(Collision other) {
        GetComponent<Rigidbody>().isKinematic = true;

        Transform aim = new GameObject().transform;//never instantiated
        aim.position = transform.position;

        Collider[] bulletColliders = new Collider[numBullets];

        for (int i = 0; i < numBullets; i++) {
            aim.eulerAngles = new Vector3(Random.Range(0, maxAngle) - 90, Random.Range(0f, 360), 0);
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, aim.position, aim.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(aim.forward * Random.Range(minUpForce, maxUpForce));
            bullet.GetComponent<Explodeable>().playerNumber = playerNumber;
            bulletColliders[i] = bullet.GetComponent<Collider>();
            bulletColliders[i].isTrigger = true;
            bullet.GetComponent<SetTrigger>().SetTriggerAfterTime(false, 1);
        }

        Collider thisCollider = GetComponent<Collider>();

        for (int i = 0; i < bulletColliders.Length; i++) {
            Physics.IgnoreCollision(bulletColliders[i], thisCollider);
            for (int j = i + 1; j < bulletColliders.Length; j++)
                Physics.IgnoreCollision(bulletColliders[i], bulletColliders[j]);
        }

        Destroy(gameObject);
    }

}
