using UnityEngine;
using System.Collections;

public class HitDetect : MonoBehaviour {

    [SerializeField]
    private Transform[] forward = null, down = null;

    [SerializeField]
    private float maxDistanceForward = 50, maxDistanceDown = 50;

    public bool GetHitForward() {
        foreach (Transform t in forward) {
            Debug.DrawRay(t.position, t.forward * maxDistanceForward, Color.blue, .01f);
            if (Physics.Raycast(t.position, t.forward, maxDistanceForward, ~(1 << 10)))
                return true;
        }
        return false;
    }

    public bool GetMissDown() {
        foreach (Transform t in down) {
            Debug.DrawRay(t.position, t.forward * maxDistanceDown, Color.blue, .01f);
            if (!Physics.Raycast(t.position, t.forward, maxDistanceDown, ~(1 << 10)))
                return false;
        }
        return true;
    }

}
