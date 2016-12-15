using UnityEngine;
using System.Collections;

public class SetEnemy : MonoBehaviour {

    private void OnEnable() {
        Scripts.Players.AddToEnemyList(gameObject);
    }

    private void OnDisable() {
        Scripts.Players.RemoveEnemyFromList(gameObject);
    }

}
