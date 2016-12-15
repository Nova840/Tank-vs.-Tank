using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LookAtPlayerOrAxis))]
public class AISpawnBullet : MonoBehaviour {

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private string animationName;

    [SerializeField]
    private float launchForce = 1, shotInterval = 10;

    [SerializeField]
    private float offset = 0;

    [SerializeField]
    private AudioSource[] sounds;

    [SerializeField]
    private float pitchVariation = 0;

    [SerializeField]
    private BulletType currentBulletType = BulletType.Regular;

    private float timeLastShot = 0;
    private int consecutiveHits = 0;

    [SerializeField]
    private int consecutiveHitThreshold = 5;

    private TargetPlayers targetPlayers;

    private void Awake() {
        timeLastShot = Time.timeSinceLevelLoad;

        LookAtPlayerOrAxis look = GetComponent<LookAtPlayerOrAxis>();
        look.lookX = false;
        look.lookY = true;
        look.lookZ = true;
        //has to be these values to work (except z in most cases)

        targetPlayers = transform.root.GetComponent<TargetPlayers>();
    }

    private void Update() {
        if (!targetPlayers.Target)
            return;

        //set barrel's angle (for looks only)
        if (transform.parent != null) {
            float parentAngle = CalculateTrajectoryAngle();
            if (!float.IsNaN(parentAngle)) {
                transform.parent.localEulerAngles = new Vector3(
                    parentAngle,
                    transform.parent.localEulerAngles.y,
                    transform.parent.localEulerAngles.z
                    );
                transform.parent.GetComponent<LimitXRotation>().Limit();
            }
        }

        //set the shot's angle
        float angle = CalculateTrajectoryAngle();
        if (!float.IsNaN(angle))
            transform.eulerAngles = new Vector3(
                angle,
                transform.eulerAngles.y,
                transform.eulerAngles.z
                );

        if (CanShoot()) {
            consecutiveHits = 0;
            Shoot();
            if (animator)
                animator.Play(animationName);
            timeLastShot = Time.timeSinceLevelLoad;
        }
    }

    private bool CanShoot() {
        bool hit;
        Vector3 hitPoint = DrawTrajectory.GetHitPoint(
            transform.position,
            (transform.forward * launchForce / CarBullets.GetPrefab(currentBulletType).GetComponent<Rigidbody>().mass) * Time.fixedDeltaTime,//[2]
            2000,
            out hit
            );

        return hit && WouldHitPlayer(hitPoint, currentBulletType) &&
            !IsAnimationActive() && !IsCoolDown() &&
            !targetPlayers.Target.GetComponent<Health>().IsDead() &&
            consecutiveHits >= consecutiveHitThreshold;
    }

    private bool IsCoolDown() {
        return !(timeLastShot < Time.timeSinceLevelLoad - shotInterval);
    }

    private bool IsAnimationActive() {
        if (!animator)
            return false;
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }

    private bool WouldHitPlayer(Vector3 point, BulletType bulletType) {
        bool hit = Vector3.Distance(point, targetPlayers.Target.position) <= CarBullets.GetPrefab(bulletType).GetComponent<ExplodeOnHit>().ExplosionPrefab.GetComponent<RegularExplosion>().ExplosionRadius;
        if (hit)
            consecutiveHits++;
        return hit;
    }

    private void Shoot() {
        GameObject bullet = (GameObject)Instantiate(CarBullets.GetPrefab(currentBulletType), transform.position, transform.rotation);
        //[2]
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * launchForce);
        foreach (Collider c in transform.root.GetComponentsInChildren<Collider>()) {
            Physics.IgnoreCollision(bullet.GetComponent<Collider>(), c);
        }

        if (sounds.Length > 0) {
            AudioSource sound = sounds[Random.Range(0, sounds.Length)];
            sound.pitch = Random.Range(1 - pitchVariation, 1 + pitchVariation);
            sound.Play();
        } else
            Debug.LogWarning("No Sound");
    }

    private float CalculateTrajectoryAngle() {
        float v0 = ((transform.forward * launchForce / CarBullets.GetPrefab(currentBulletType).GetComponent<Rigidbody>().mass) * Time.fixedDeltaTime /*+ rBInherit.velocity*/).magnitude;
        Vector2 distance = new Vector2(transform.InverseTransformPoint(targetPlayers.Target.position).z, targetPlayers.Target.position.y - transform.position.y);
        float g = Physics.gravity.magnitude;
        //Angle formula: https://en.wikipedia.org/wiki/Trajectory_of_a_projectile#Angle_.7F.27.22.60UNIQ--postMath-00000010-QINU.60.22.27.7F_required_to_hit_coordinate_.28x.2Cy.29
        float angle =
            Mathf.Atan(
            (Mathf.Pow(v0, 2) -/*[1]*/ Mathf.Sqrt(Mathf.Pow(v0, 4) - g * (g * Mathf.Pow(distance.x, 2) + 2 * distance.y * Mathf.Pow(v0, 2))))
            / (g * distance.x)
            )
            * Mathf.Rad2Deg;

        angle *= -1;
        angle += offset;
        return angle;
    }

}
/*
[1]: + doesn't work when I thought it would aim high rather than low. Not because of the rotation limiter.

[2]: Does not add inherited velocity because I'm not sure how to accurately factor that into the formula.

[3]: F = ma == F/m = a
     v = at
     v + inherited v [2]
*/
