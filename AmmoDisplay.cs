using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour {

    private Text text;
    private static Color active = new Color(0, 190 / 255f, 255 / 255f), inactive = new Color(110 / 255f, 110 / 255f, 110 / 255f);

    [SerializeField]
    private GameObject imagesContainer = null;
    private List<Image> images = new List<Image>();

    private BulletType type;
    public BulletType Type { get { return type; } }

    private void Awake() {
        foreach (Image i in imagesContainer.GetComponentsInChildren<Image>())//assumes images are in order
            if (i.gameObject != imagesContainer)
                images.Add(i);
        text = GetComponentInChildren<Text>();
    }

    public void LightUp(int number) {
        for (int i = 0; i < images.Count; i++)
            images[i].color = number > i ? active : inactive;
    }

    public void SetType(BulletType bullet) {
        type = bullet;
        text.text = bullet.ToString().SplitCamelCase();
    }

}
