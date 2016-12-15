using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderColor : MonoBehaviour {

    [SerializeField]
    private Image imageToChange;
    [SerializeField]
    private Color fullColor = Color.white, emptyColor = Color.white;
    private Slider slider;

    private void Awake() {
        slider = GetComponent<Slider>();
    }

    public void OnSliderValueChanged(float value) {
        imageToChange.color = Color.Lerp(emptyColor, fullColor, value / (slider.maxValue - slider.minValue));
    }

}
