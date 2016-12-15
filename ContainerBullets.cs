using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContainerBullets : MonoBehaviour {

    [System.Serializable]
    private class BulletChance {
        [SerializeField]
        private BulletType type;
        [SerializeField]
        private int min = 1, max = 5;

        public BulletType Type { get { return type; } }
        public int Min { get { return min; } }
        public int Max { get { return max; } }
    }

    [SerializeField]
    private BulletChance[] chances;

    private Dictionary<BulletType, int> bullets = new Dictionary<BulletType, int>();

    private void Awake() {
        /*
        for (int i = 0; i < (int)BulletType.NumberOfTypes; i++) {
            if (i == (int)BulletType.Regular)
                continue;
            int rand = Random.Range(0, 5);
            if (rand != 0)
                bullets.Add((BulletType)i, rand);
        }
        */
        foreach(BulletChance bc in chances) {
            int rand = Random.Range(bc.Min, bc.Max + 1);
            if (rand != 0)
                bullets.Add(bc.Type, rand);
        }
    }

    /*public Dictionary<CarBullets.BulletType, int> GetBulletsCopy() {
        return new Dictionary<CarBullets.BulletType, int>(bullets);
    }*/

    public void CollectBullets() {
        foreach (KeyValuePair<BulletType, int> b in bullets)
            Scripts.Players.GetPlayer(1).GetComponent<CarBullets>().AddBullets(b.Key, b.Value);
        //get player at index 0 temporary, come back if I ever add back containers.
        Destroy(gameObject);
    }
}