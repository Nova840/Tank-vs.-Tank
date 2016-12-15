using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shockwave : MonoBehaviour {

    [SerializeField]
    private Vector3 targetScale = Vector3.one;

    [SerializeField]
    private float scaleLerpRate = .1f, colorLerpRate = .1f;

    [SerializeField]
    private Image image;

    private void FixedUpdate() {
        Color c = image.color;
        c.a = 0;
        image.color = Color.Lerp(image.color, c, colorLerpRate);

        if (image.color.a <= .0001f)
            Destroy(gameObject);

        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleLerpRate);
    }

}
