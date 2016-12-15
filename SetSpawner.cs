using UnityEngine;
using System.Collections;

public class SetSpawner : MonoBehaviour {

    private void OnEnable() {
        Scripts.Players.AddSpawner(gameObject);
    }

    private void OnDisable() {
        Scripts.Players.RemoveSpawner(gameObject);
    }

}
