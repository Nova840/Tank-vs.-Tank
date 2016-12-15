using UnityEngine;
using System.Collections;

public class TargetPlayers : MonoBehaviour {

    [SerializeField]
    private float changeInterval = 10;

    private Transform target;
    public Transform Target { get { return target; } }

    private void Start() {//Awake happens before players are added to list
        StartCoroutine(ChangeTarget());
    }

    private IEnumerator ChangeTarget() {
        while (true) {
            Transform closest = null;
            foreach (GameObject player in Scripts.Players.players) {
                if ((!closest || IsCloser(player.transform, closest)) && !player.GetComponent<Health>().IsDead()) {
                    closest = player.transform;
                }
            }
            target = closest;
            yield return new WaitForSeconds(changeInterval);
        }
    }

    private bool IsCloser(Transform playerTransform, Transform closest) {
        return Vector3.Distance(transform.position, playerTransform.position) < Vector3.Distance(transform.position, closest.position);
    }

}
