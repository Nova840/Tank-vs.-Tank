using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SetTextToHealth : MonoBehaviour {

    private Text text;
    private Health health;

    [HideInInspector]
    public int playerNumber = 1;

    private void Awake() {
        text = GetComponent<Text>();
    }

    private void Start() {//Awake happens before players are in list
        health = Scripts.Players.GetPlayer(playerNumber).GetComponent<Health>();
    }

    void Update() {
        text.text = health.CurrentHealth.ToString();
    }

}
