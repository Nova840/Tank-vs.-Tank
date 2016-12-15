using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerSpawn : MonoBehaviour {

    [SerializeField]
    private CenterOfMass centerOfMass;

    public bool ableToRespawn = false;

    private PlayerControls playerControls;
    private GameObject healthbars;
    private CameraRig cameraRig;

    [HideInInspector]
    public PlayerUI playerUI;//set for all players in PlayerUI

    private void Awake() {
        playerControls = GetComponent<PlayerControls>();
        healthbars = transform.Find("Healthbars").gameObject;
        cameraRig = transform.Find("Camera Rig").GetComponent<CameraRig>();
    }

    private void Start() {//Awake happens before this is given a spawnpoint in SpawnPlayers
        Spawn();
    }

    private void Update() {
        if (ableToRespawn && playerControls.controls && Input.GetButtonDown(playerControls.controls.Respawn))
            //playerControls.controls can be null the first frame
            Spawn();
    }

    private Transform TransformToSpawn() {
        List<Transform> spawnpoints = new List<Transform>(Scripts.SpawnPlayers.Spawnpoints);
        spawnpoints.Shuffle();

        foreach (Transform spawnpoint in spawnpoints)
            //a sphere with a diameter of 10 is about player size
            if (!Physics.OverlapSphere(spawnpoint.position, 5).Any(c => c.transform.root.CompareTag("Player")))
                return spawnpoint;

        Debug.LogWarning("No Available Spawnpoint");
        return null;
    }

    public void Spawn() {
        Transform spawnpoint = TransformToSpawn();
        playerUI.RemoveRespawn(Players.PlayerIndex(gameObject));

        if (spawnpoint != null) {
            transform.position = spawnpoint.position;
            transform.rotation = spawnpoint.rotation;
        } else {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            Debug.LogWarning("No Spawnpoint");
        }

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

        rigidbody.isKinematic = false;
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
            r.enabled = true;

        foreach (Collider c in GetComponentsInChildren<Collider>())
            c.enabled = true;


        Health health = GetComponent<Health>();
        health.CurrentHealth = health.StartingHealth;

        healthbars.SetActive(true);

        cameraRig.ResetRotation();

        centerOfMass.ResetCenterOfMass();
    }

}
