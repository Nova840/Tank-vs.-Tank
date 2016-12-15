using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class DrawTrajectory : MonoBehaviour {

    private static Material aimGuide;
    private static GameObject aimPointPrefab;
    private GameObject aimPoint;
    private SpawnBullet spawnBullet;
    private Rigidbody rbInherit;
    private Health health;
    private Transform cameraTransform;

    private LineRenderer line;
    private int layer;

    private int insideColliders = 0;

    [SerializeField]
    private float lineWidth = .5f;
    [SerializeField]
    private int maxVerts = 2000;

    private void Awake() {
        if (!aimGuide)
            aimGuide = Resources.Load<Material>("Aim Guide");
        if (!aimPointPrefab)
            aimPointPrefab = Resources.Load<GameObject>("Aim Point");

        line = GetComponent<LineRenderer>();
        line.name = "Line";
        line.SetWidth(lineWidth, lineWidth);
        line.material = aimGuide;

        rbInherit = transform.root.GetComponent<Rigidbody>();
        spawnBullet = transform.parent.GetComponent<SpawnBullet>();
        health = transform.root.GetComponent<Health>();
    }

    private void Start() {//Awake happens before player name is set.
        string rootName = transform.root.name;
        layer = int.Parse(rootName.Substring(rootName.Length - 1, 1)) + 10;

        cameraTransform = GameObject.Find(rootName + " Camera").transform;
    }

    private void LateUpdate() {//Update causes flickering
        if (!health.IsDead())
            DrawTraject(
                transform.position,
                //F = ma == F/m = a
                //v = at
                //v + inherited v
                (transform.forward * spawnBullet.LaunchForce / CarBullets.GetPrefab(spawnBullet.CurrentBulletType).GetComponent<Rigidbody>().mass) * Time.fixedDeltaTime + rbInherit.velocity,
                insideColliders > 0 ? 0 : maxVerts
                );
        else if (aimPoint)
            Destroy(aimPoint.gameObject);
    }

    private void DrawTraject(Vector3 startPos, Vector3 startVelocity, int maxVerts) {
        bool hit;
        int verts;
        Vector3 hitPoint = GetHitPoint(startPos, startVelocity, maxVerts, out hit, out verts);

        if (hit) {
            if (aimPoint) {
                aimPoint.transform.position = hitPoint;
            } else {
                aimPoint = (GameObject)Instantiate(aimPointPrefab, hitPoint, Quaternion.identity);
                aimPoint.name = transform.root.name + " Aim Point";
                aimPoint.layer = layer;
                aimPoint.GetComponent<LookAtCamera>().cameraTransform = cameraTransform;
            }
        } else if (aimPoint)
            Destroy(aimPoint);

        line.SetVertexCount(verts);

        Vector3 pos = startPos;
        Vector3 vel = startVelocity;
        Vector3 grav = Physics.gravity;

        for (int i = 0; i < verts; i++) {
            line.SetPosition(i, pos);

            vel += grav * Time.fixedDeltaTime;
            pos += vel * Time.fixedDeltaTime;
        }

    }

    public static Vector3 GetHitPoint(Vector3 startPos, Vector3 startVelocity, int maxVerts, out bool hit, out int verts) {
        Vector3 pos = startPos;
        Vector3 vel = startVelocity;
        Vector3 grav = Physics.gravity;
        hit = false;
        verts = 1;
        RaycastHit hitInfo = new RaycastHit();
        LayerMask lm = ~((1 << 8) | (1 << 9) | (1 << 15));//all layers except 8, 9, and 15.
        for (int i = 0; i < maxVerts - 1; i++) {
            Debug.DrawRay(pos, vel * Time.fixedDeltaTime, Color.red, .0000001f); //For use in the editor
            verts++;
            if (Physics.Raycast(pos, vel, out hitInfo, vel.magnitude * Time.fixedDeltaTime, lm)) {
                //[1]
                hit = true;
                break;
            }

            vel += grav * Time.fixedDeltaTime;
            pos += vel * Time.fixedDeltaTime;
        }

        return hitInfo.point; // == Vector3.zero if never hit
    }

    public static Vector3 GetHitPoint(Vector3 startPos, Vector3 startVelocity, int maxVerts, out bool hit) {
        int verts;
        return GetHitPoint(startPos, startVelocity, maxVerts, out hit, out verts);
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.isTrigger && other.gameObject.layer != 9)
            insideColliders++;
    }

    private void OnTriggerExit(Collider other) {
        if (!other.isTrigger && other.gameObject.layer != 9)
            insideColliders--;
    }

}

/*
[1]:
A small amount of precision is lost because of it being a raycast instead of a spherecast to account for the bullet's thickness.
Spherecast doesn't register a hit if the sphere overlaps the collider it hits while in it's original position (same as a CheckSphere with the same arguments),
so it flickers as it moves across the ground.
Can't use a CheckSphere to get the (potential) original hit because CheckSphere doesn't give you a RaycastHit (nor does OverlapSphere).
I think this is because it has no way of knowing where the initial point of contact would be if it starts already overlaping something.
*/
