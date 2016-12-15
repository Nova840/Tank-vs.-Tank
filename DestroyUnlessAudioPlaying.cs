using UnityEngine;
using System.Collections;

public class DestroyUnlessAudioPlaying : MonoBehaviour {

    private AudioSource[] sounds;

    private void Awake() {
        sounds = GetComponents<AudioSource>();
    }

    private void Update() {
        foreach (AudioSource audio in sounds)
            if (audio.isPlaying)
                return;

        Destroy(gameObject);
    }

}
