using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderToVolume : MonoBehaviour {

    private void Awake() {
        GetComponent<Slider>().value = AudioListener.volume;
    }

}
