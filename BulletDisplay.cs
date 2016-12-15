using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class BulletDisplay : MonoBehaviour {

    private CarBullets carBullets;
    //private SpawnBullet spawnBullet;

    [HideInInspector]
    public int playerNumber = -1;

    private Text text;

    private void Awake() {
        text = GetComponent<Text>();
    }

    private void Start() {//Awake happens before players are in list
        GameObject player = Scripts.Players.GetPlayer(playerNumber);
        //spawnBullet = player.GetComponentInChildren<SpawnBullet>();
        carBullets = player.GetComponent<CarBullets>();
    }

    private void Update() {
        text.text = carBullets.BulletCount() + " / " + carBullets.MaxBullets;
    }

}
