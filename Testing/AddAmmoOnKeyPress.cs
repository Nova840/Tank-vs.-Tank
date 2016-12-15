using UnityEngine;
using System.Collections;

public class AddAmmoOnKeyPress : MonoBehaviour {

    private void Update() {
        if (Input.GetKeyUp(KeyCode.T))
            for (int i = 1; i < (int)BulletType.NumberOfTypes; i++)
                Scripts.Players.GetPlayer(1).GetComponent<CarBullets>().AddBullets((BulletType)i, 1);

    }

}
