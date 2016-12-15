using UnityEngine;
using System.Collections;

public class DieIfBelow : MonoBehaviour {

    [SerializeField]
    private float threshold = -100;

    [SerializeField]
    private Health health;

    private void Update() {
        if (transform.position.y < threshold)
            health.CurrentHealth = 0;//property takes care of this happening continuously 
    }

}
