using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class DestroyOnAnimationDone : MonoBehaviour {

    private Animator animator;

    [SerializeField]
    private string animationName;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
            Destroy(gameObject);
    }
}
