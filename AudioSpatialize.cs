using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioSpatialize : MonoBehaviour {

    private void Start() {
        if (Scripts.Players.players.Count > 1) {
            foreach (AudioSource a in GetComponents<AudioSource>()) {
                a.spatialBlend = 0;
                a.volume = .5f;
            }
        }
    }

}
