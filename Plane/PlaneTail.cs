using UnityEngine;
using System.Collections;

public class PlaneTail : MonoBehaviour {

    [SerializeField]
    private float angle = 20;

    public void Angle(int direction) {
        direction = Mathf.Clamp(direction, -1, 1);
        transform.localEulerAngles = Vector3.left * angle * direction;
    }

}
