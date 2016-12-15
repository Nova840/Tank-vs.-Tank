using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnBullet : MonoBehaviour {

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private string animationName;
    [SerializeField]
    private GameObject shockwave;
    [SerializeField]
    private float shockwavePercentWhite = .5f;
    [SerializeField]
    private float recoilForce = 1000;
    [SerializeField]
    private ForceMode recoilForceMode = ForceMode.Impulse;
    [SerializeField]
    private Rigidbody rbInherit;//RigidBody to inherit velocity from.
    [SerializeField]
    private Health health;
    [SerializeField]
    private CarBullets carBullets;
    [SerializeField]
    private float launchForce = 1;
    public float LaunchForce { get { return launchForce; } }
    [SerializeField]
    private AudioSource[] sounds;
    [SerializeField]
    private float pitchVariation = 0;//make original pitch variable set on awake to allow for different pitches

    private PlayerControls playerControls;

    private int lastBulletCount;

    private BulletType currentBulletType = BulletType.Regular;
    public BulletType CurrentBulletType { get { return currentBulletType; } }

    private void Awake() {
        playerControls = transform.root.GetComponent<PlayerControls>();//too early to get the controls object off of this and store only that
    }

    private void Update() {
        if (!playerControls.controls)//possible to not be set in the beginning
            return;

        if (Input.GetButtonDown(playerControls.controls.CycleDown))
            SwitchBulletType(1);
        if (Input.GetButtonDown(playerControls.controls.CycleUp))
            SwitchBulletType(-1);

        //Assign to first bullet you get if you don't have any
        if (lastBulletCount <= 0 && carBullets.BulletCount() > 0)//if just got a bullet
            currentBulletType = carBullets.TypeAtIndex(0);
        lastBulletCount = carBullets.BulletCount();

        if (Input.GetButton(playerControls.controls.Shoot) && !animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) &&
            !health.IsDead()) {

            int currentBulletIndex = carBullets.BulletIndex(currentBulletType);

            if (!carBullets.Shoot(currentBulletType))
                return;

            //Create bullet
            GameObject bullet = (GameObject)Instantiate(CarBullets.GetPrefab(currentBulletType), transform.position, transform.rotation);
            bullet.GetComponent<Explodeable>().playerNumber = Players.PlayerNumber(transform.root.gameObject);

            //Bullet can't hit your tank
            foreach (Collider c in transform.root.GetComponentsInChildren<Collider>())//[1]
                Physics.IgnoreCollision(bullet.GetComponent<Collider>(), c);

            //Launch bullet
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.velocity = rbInherit.velocity;
            bulletRb.AddForce(transform.forward * launchForce);

            //Barrel recoil animation
            animator.Play(animationName);

            //Recoil
            rbInherit.AddForceAtPosition(-transform.forward * recoilForce, transform.position, recoilForceMode);

            //Shockwave + shockwave color
            Transform shockTransform = ((GameObject)Instantiate(shockwave, transform.position, transform.rotation)).transform;
            Material trailMat = bullet.transform.Find("Trail").GetComponent<TrailRenderer>().material;
            Color trailColor = trailMat.GetColor("_TintColor");
            trailColor.a = 1;
            trailColor = Color.Lerp(trailColor, Color.white, shockwavePercentWhite);
            shockTransform.Find("Image").GetComponent<Image>().color = trailColor;

            //Shockwave does not follow the player
            shockTransform.SetParent(null, true);

            //Play sound
            if (sounds.Length > 0) {
                AudioSource sound = sounds[Random.Range(0, sounds.Length)];
                sound.pitch = Random.Range(1 - pitchVariation, 1 + pitchVariation);
                sound.Play();
            } else {
                Debug.LogWarning("No Sound");
            }

            //Change bullet type if necessary
            if (carBullets.BulletCount(currentBulletType) <= 0 && carBullets.BulletCount() > 0) {
                if (currentBulletIndex == carBullets.UniqueBullets())
                    currentBulletIndex--;
                currentBulletType = carBullets.TypeAtIndex(currentBulletIndex);
            }

        }
    }

    private void SwitchBulletType(int amount, bool loop = false) {//amount can be negative
        if (carBullets.BulletCount() <= 0)
            return;
        int index = carBullets.BulletIndex(currentBulletType);
        do {
            index += amount;
            if (index >= carBullets.UniqueBullets()) {//if incrementing (down)
                if (loop) {
                    index = 0;
                } else {
                    index = carBullets.UniqueBullets() - 1;
                    break;
                }
            } else if (index < 0) {//if decrementing (up)
                if (loop) {
                    index = carBullets.UniqueBullets() - 1;
                } else {
                    index = 0;
                    break;
                }
            }
        } while (carBullets.BulletCount(index) <= 0);
        currentBulletType = carBullets.TypeAtIndex(index);
    }

}

/*
[1]:
here because if I put it in the OnCollisionEnter method on the bullet it would stop on the player before going through.
not on the bullet in Awake because that would involve finding the player every time a bullet was shot.
*/
