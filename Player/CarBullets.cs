using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarBullets : MonoBehaviour {

    private static Dictionary<BulletType, GameObject> bulletPrefabs = null;

    [SerializeField]
    private int maxBullets = 10;
    public int MaxBullets { get { return maxBullets; } }

    private AmmoDisplayController ammoDisplayController;

    public class BulletInfo {

        private BulletType type;
        private int amount = 0;

        public BulletInfo(BulletType type) {
            this.type = type;
        }

        public BulletInfo(BulletInfo b) {
            type = b.type;
            amount = b.amount;
        }

        public BulletType Type { get { return type; } }
        public int Amount { get { return amount; } set { amount = value; } }

        public static implicit operator bool (BulletInfo b) {
            return b != null;
        }

    }

    private List<BulletInfo> bullets = new List<BulletInfo>();
    //needs to be a list to keep track of the order in which they were added

    public List<BulletInfo> BulletsCopy() {
        List<BulletInfo> bulletsCopy = new List<BulletInfo>();
        foreach (BulletInfo b in bullets)
            bulletsCopy.Add(new BulletInfo(b));
        return bulletsCopy;
    }

    private void Awake() {
        if (bulletPrefabs == null)
            bulletPrefabs = new Dictionary<BulletType, GameObject>() {
                {BulletType.Regular, Resources.Load<GameObject>("Bullet")},
                {BulletType.Red, Resources.Load<GameObject>("RedBullet")},
                {BulletType.Blue, Resources.Load<GameObject>("BlueBullet")},
                {BulletType.Splitter, Resources.Load<GameObject>("Splitter")},
                {BulletType.Mine, Resources.Load<GameObject>("Mine")},
                {BulletType.VerticalSplitter, Resources.Load<GameObject>("VerticalSplitter")},
                {BulletType.Fire, Resources.Load<GameObject>("FireBullet")},
                {BulletType.Cube, Resources.Load<GameObject>("SquareBullet")},
                {BulletType.DoubleSplitter, Resources.Load<GameObject>("DoubleSplitter")},
                {BulletType.AirStrike, Resources.Load<GameObject>("AirStrike")},
                {BulletType.Grenade, Resources.Load<GameObject>("Grenade")},
                {BulletType.SplitterMine, Resources.Load<GameObject>("SplitterMine")},
                {BulletType.Seeker, Resources.Load<GameObject>("Seeker")},
                {BulletType.Geyser, Resources.Load<GameObject>("Geyser")},
            };
    }

    private void Start() {
        ammoDisplayController = Scripts.PlayerUI.PlayerScreens[Players.PlayerIndex(gameObject)].GetComponentInChildren<AmmoDisplayController>();
    }

    public void AddBullets(BulletType bullet, int amount = 1) {
        int bulletCount = BulletCount();
        if (bulletCount >= maxBullets || amount <= 0)
            return;

        if (bulletCount + amount > maxBullets)
            amount = maxBullets - bulletCount;

        BulletInfo bulletInfo = bullets.Find(b => b.Type == bullet);
        if (!bulletInfo) {
            bulletInfo = new BulletInfo(bullet);
            bullets.Add(bulletInfo);
        }
        bulletInfo.Amount += amount;

        StartCoroutine(CalculateAmmo());
    }

    private IEnumerator CalculateAmmo() {
        yield return new WaitForEndOfFrame();
        //Would come in diagonally if it's added at index 0 because of what happens before shoot in SpawnBullet.
        ammoDisplayController.CalculateAmmo();
    }

    public bool Shoot(BulletType bullet) {
        BulletInfo bulletInfo = bullets.Find(b => b.Type == bullet);
        if (!bulletInfo)
            return false;
        bulletInfo.Amount--;
        if (bulletInfo.Amount <= 0)
            bullets.Remove(bulletInfo);

        ammoDisplayController.CalculateAmmo();//if you use the coroutine, it will delay a frame and the display will move a couple pixels down.
        return true;
    }

    public int BulletIndex(BulletType bullet) {
        return bullets.FindIndex(b => b.Type == bullet);
    }

    public BulletType TypeAtIndex(int index) {
        return bullets[index].Type;
    }

    public int BulletCount(BulletType bullet) {
        BulletInfo bulletInfo = bullets.Find(b => b.Type == bullet);
        return bulletInfo ? bulletInfo.Amount : 0;
    }

    public int BulletCount(int index) {
        return bullets[index] ? bullets[index].Amount : 0;
    }

    public int UniqueBullets() {
        return bullets.Count;
    }

    public int BulletCount() {
        int sum = 0;
        foreach (BulletInfo b in bullets)
            sum += b.Amount;
        return sum;
    }

    public static GameObject GetPrefab(BulletType bullet) {
        return bulletPrefabs[bullet];
    }

}

public enum BulletType {
    Regular, Red, Blue, Splitter, Mine, VerticalSplitter, Fire, Cube, DoubleSplitter, AirStrike, Grenade, SplitterMine, Seeker, Geyser, NumberOfTypes
}