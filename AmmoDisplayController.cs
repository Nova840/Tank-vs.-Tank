using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmmoDisplayController : MonoBehaviour {

    [HideInInspector]
    public CarBullets carBullets = null;
    private SpawnBullet spawnBullet = null;

    [HideInInspector]
    public int playerNumber = 0;//set in PlayerUI

    [SerializeField]
    private GameObject ammoDisplayPrefab;
    private Vector3 ammoPrefabScale;

    [SerializeField]
    private float lerpRate = 20;

    private List<AmmoDisplay> ammoDisplays = new List<AmmoDisplay>();

    private void Awake() {
        ammoPrefabScale = ammoDisplayPrefab.transform.localScale;
    }

    private void Start() {//Awake happens before players are in list
        GameObject player = Scripts.Players.GetPlayer(playerNumber);
        carBullets = player.GetComponent<CarBullets>();
        spawnBullet = player.GetComponentInChildren<SpawnBullet>();
    }

    private void Update() {
        for (int i = 0; i < ammoDisplays.Count; i++) {
            RectTransform rt = ammoDisplays[i].GetComponent<RectTransform>();
            rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, new Vector2(rt.rect.width / 2, GetYPosition(rt, i)), lerpRate * Time.deltaTime);
        }
    }

    public void CalculateAmmo() {
        List<CarBullets.BulletInfo> bullets = carBullets.BulletsCopy();

        for (int i = 0; i < ammoDisplays.Count; i++) {
            if (!bullets.Exists(b => b.Type == ammoDisplays[i].Type)) {//if there is no bullet for the ammo display
                StartCoroutine(SlideOff(ammoDisplays[i].GetComponent<RectTransform>()));
                ammoDisplays.RemoveAt(i);
                i--;
            }
        }

        foreach (CarBullets.BulletInfo bullet in bullets) {
            if (!ammoDisplays.Exists(a => a.Type == bullet.Type)) {//if there is no ammo display for the bullet
                GameObject ammoDisplayGameObject = (GameObject)Instantiate(ammoDisplayPrefab, transform);

                RectTransform rt = ammoDisplayGameObject.GetComponent<RectTransform>();
                rt.localScale = ammoPrefabScale;//can change scale automatically if UI shrinks due to more players taking up screen space.
                rt.anchoredPosition = new Vector2(-rt.rect.width / 2, GetYPosition(rt, ammoDisplays.Count));

                AmmoDisplay ammoDisplay = ammoDisplayGameObject.GetComponent<AmmoDisplay>();
                ammoDisplay.SetType(bullet.Type);

                ammoDisplays.Add(ammoDisplay);
            }
        }

        foreach (AmmoDisplay ammoDisplay in ammoDisplays)
            ammoDisplay.LightUp(carBullets.BulletCount(ammoDisplay.Type));

    }

    private IEnumerator SlideOff(RectTransform rt) {
        while (true) {
            yield return new WaitForSeconds(0);
            rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, new Vector2(-rt.rect.width / 2, rt.anchoredPosition.y), lerpRate * Time.deltaTime);
            if (rt.anchoredPosition.x <= -rt.rect.width / 2 + .001f) {//.001f tolerance
                Destroy(rt.gameObject);
                break;
            }
        }
    }

    private float GetYPosition(RectTransform rt, int index) {
        return (index - carBullets.BulletIndex(spawnBullet.CurrentBulletType) - 1) * -Offset(rt) + Offset(rt) / 2;
    }

    private float Offset(RectTransform rt) {
        return rt.rect.height * 1.25f;
    }

}
