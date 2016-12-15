using UnityEngine;
using System.Collections;

public class LookAtPlayerOrAxis : MonoBehaviour {

    [SerializeField]
    private Vector3 offset = Vector3.zero;

    [SerializeField]
    public bool lookX = true, lookY = true, lookZ = true;

    [SerializeField]
    private bool useLateUpdate = false;

    private TargetPlayers targetPlayers;

    private void Awake() {
        targetPlayers = transform.root.GetComponent<TargetPlayers>();
    }

    private void Update() {
        if (!useLateUpdate)
            LookAt();
    }

    private void LateUpdate() {
        if (useLateUpdate)
            LookAt();
    }

    private void LookAt() {
        if (!targetPlayers.Target)
            return;

        transform.LookAt(targetPlayers.Target);

        transform.localEulerAngles = new Vector3(
            lookX ? transform.localEulerAngles.x : 0,
            lookY ? transform.localEulerAngles.y : 0,
            lookZ ? transform.localEulerAngles.z : 0
        );

        transform.localEulerAngles += offset;
    }

}
