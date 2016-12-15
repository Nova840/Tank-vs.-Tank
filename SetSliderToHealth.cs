using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SetSliderToHealth : MonoBehaviour {

    private Health health;
    private Slider slider;

    [HideInInspector]
    public int playerNumber = -1; //if -1 then find the health on the root gameobject

    private void Awake() {
        slider = GetComponent<Slider>();
    }

    private void Start() {
        if (playerNumber == -1)
            health = transform.root.GetComponent<Health>();//Awake happens before parent is set
        else
            health = Scripts.Players.GetPlayer(playerNumber).GetComponent<Health>();//Awake happens before players are in list
    }

    private void Update() {
        slider.value = (float)health.CurrentHealth / health.StartingHealth;
    }

}
