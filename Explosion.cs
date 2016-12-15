using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public abstract class Explosion : MonoBehaviour {

    [HideInInspector]
    public int playerNumber = 0;

    [SerializeField]
    protected float fadeLerpRate = .1f, expandLerpRate = .2f, explosionDiameter = 100;
    public float ExplosionRadius { get { return explosionDiameter / 2; } }//for mine bullet

    [SerializeField]
    protected GameObject soundsContainer;
    protected AudioSource[] sounds;
    [SerializeField]
    protected float pitchVariation = 0;

    protected Material mat;

    protected void Awake() {
        sounds = soundsContainer.GetComponents<AudioSource>();
        mat = GetComponent<Renderer>().material;
    }

    protected void Start() {
        PlaySound();
    }

    protected void Fade() {
        Color colorAlpha0 = mat.GetColor("_TintColor");
        colorAlpha0.a = 0;
        mat.SetColor("_TintColor", Color.Lerp(mat.GetColor("_TintColor"), colorAlpha0, fadeLerpRate));

        if (mat.GetColor("_TintColor").a <= 0.0001f) {//if it has (almost) reached the desired transparency.
            if (soundsContainer)//null sometimes, idk
                soundsContainer.transform.parent = null;
            Destroy(gameObject);

        }
    }

    protected void Expand() {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * explosionDiameter, expandLerpRate);
    }

    protected void PlaySound() {
        if (sounds.Length > 0) {
            AudioSource sound = sounds[Random.Range(0, sounds.Length)];
            sound.pitch = Random.Range(1 - pitchVariation, 1 + pitchVariation);
            sound.Play();
        } else {
            Debug.LogWarning("No Sound");
        }
    }

    protected bool IsSamePlayerBullet(GameObject g) {
        return g.CompareTag("Bullet") && g.GetComponent<Explodeable>().playerNumber == playerNumber;
    }

}
