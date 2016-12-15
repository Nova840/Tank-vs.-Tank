using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Still has to have an explosion prefab to determine the color of the trail
public class AirStrike : Explodeable {

    [SerializeField]
    private GameObject bulletPrefab = null, airStrikePointPrefab = null;

    [SerializeField]
    private float spawnHeight = 250;
    [SerializeField]
    private Vector3 startVelocity = new Vector3(0, -100, 0);

    private GameObject bullet;
    private List<GameObject> airStrikePoints = new List<GameObject>();
    private bool activated;

    private void OnCollisionEnter() {
        if (activated)
            return;

        GetComponent<Rigidbody>().isKinematic = true;

        foreach (GameObject player in Scripts.Players.players) {
            GameObject airStrikePoint = (GameObject)Instantiate(airStrikePointPrefab, transform.position, Quaternion.identity);
            airStrikePoint.GetComponent<LookAtCamera>().cameraTransform = Scripts.Players.playerCameras[Players.PlayerIndex(player)].transform;
            airStrikePoint.layer = Players.PlayerNumber(player) + 10;
            airStrikePoints.Add(airStrikePoint);
        }

        bullet = (GameObject)Instantiate(bulletPrefab, transform.position + Vector3.up * spawnHeight, transform.rotation);
        bullet.GetComponent<Explodeable>().playerNumber = playerNumber;
        bullet.GetComponent<Rigidbody>().velocity = startVelocity;

        activated = true;
    }

    private void Update() {
        if (activated && !bullet) {
            while (airStrikePoints.Count > 0) {
                Destroy(airStrikePoints[0]);
                airStrikePoints.RemoveAt(0);
            }
            Destroy(gameObject);
        }
    }

}
