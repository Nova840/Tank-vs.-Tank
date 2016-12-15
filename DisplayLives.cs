using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DisplayLives : MonoBehaviour {

    [SerializeField]
    private GameObject imagePrefab;
    private Vector3 imagePrefabScale;

    private List<GameObject> images = new List<GameObject>();

    private void Awake() {
        imagePrefabScale = imagePrefab.transform.localScale;
    }

    public void SetLives(int lives) {
        while (images.Count > 0) {
            Destroy(images[0]);
            images.RemoveAt(0);
        }
        for (int i = 1; i < lives; i++) {//displays 1 less life than you have so it doesn't include the life you're on
            GameObject image = (GameObject)Instantiate(imagePrefab, transform);
            image.name = "Life Image " + i;
            RectTransform rt = image.GetComponent<RectTransform>();
            rt.localScale = imagePrefabScale;//can change scale automatically if UI shrinks due to more players taking up screen space.
            rt.anchoredPosition = 15 * (i * 2 - 1) * Vector2.left;
            images.Add(image);
        }
    }

}
