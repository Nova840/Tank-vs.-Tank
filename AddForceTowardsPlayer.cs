using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody), typeof(Explodeable))]
public class AddForceTowardsPlayer : MonoBehaviour {

    private Explodeable explodeable;
    private Rigidbody rb;

    [SerializeField]
    private float force = 100, dragIfTracking = .5f;

    private void Awake() {
        explodeable = GetComponent<Explodeable>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        GameObject tank = ClosestOtherTank(!CycleButtons.IsCoOp());//tutorial tracks other players for now
        rb.drag = tank ? dragIfTracking : 0;
        if (tank)
            rb.AddForce((tank.transform.position - transform.position) * force);
    }

    private GameObject ClosestOtherTank(bool players) {
        List<GameObject> tanks = players ? Scripts.Players.players : Scripts.Players.enemies;
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;//always trigger the if statement
        foreach (GameObject tank in tanks) {
            float distance = Vector3.Distance(transform.position, tank.transform.position);
            if (distance < closestDistance && (!players || (Players.PlayerNumber(tank) != explodeable.playerNumber && !tank.GetComponent<Health>().IsDead()))) {
                closest = tank;
                closestDistance = distance;
            }
        }
        return closest;
    }

}
