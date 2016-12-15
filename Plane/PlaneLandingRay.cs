using UnityEngine;
using System.Collections;
using System.Linq;

public class PlaneLandingRay : MonoBehaviour {

    [SerializeField]
    private float distance = 50;

    private bool hit = false;
    public bool Hit { get { return hit; } }

    private void Update() {
        Debug.DrawRay(transform.position, Vector3.down * distance, Color.blue, .0000001f);
        hit = Physics.RaycastAll(transform.position, Vector3.down, distance).Any(r => !r.transform.root.CompareTag("Player"));
    }

}
