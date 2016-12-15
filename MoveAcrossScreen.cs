using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Text))]
public class MoveAcrossScreen : MonoBehaviour {

    private RectTransform rectTransform;

    [SerializeField]
    private RectTransform overlayCanvas;

    [SerializeField]
    private float speed = 200;

    [SerializeField]
    private bool moveLeft = true;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();

        //Sometimes flickers on first frame if put in Start
        rectTransform.anchorMin = new Vector2(moveLeft ? 0 : 1, .5f);
        rectTransform.anchorMax = new Vector2(moveLeft ? 0 : 1, .5f);
        GetComponent<Text>().alignment = moveLeft ? TextAnchor.MiddleLeft : TextAnchor.MiddleRight;
        rectTransform.anchoredPosition = new Vector2(overlayCanvas.rect.width * -Dir() + rectTransform.rect.width / 2 * -Dir(), 0);
    }

    private void Update() {
        rectTransform.anchoredPosition += new Vector2(speed * Time.unscaledDeltaTime * Dir(), 0);
        if (OffScreen(moveLeft))
            Destroy(overlayCanvas.gameObject);
    }

    private bool OffScreen(bool left) {
        if (left)
            return rectTransform.anchoredPosition.x <= -rectTransform.rect.width / 2;
        else
            return rectTransform.anchoredPosition.x >= rectTransform.rect.width / 2;
    }

    private int Dir() {
        return moveLeft ? -1 : 1;
    }

}
