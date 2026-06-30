using UnityEngine;

public class PlayerVisual : MonoBehaviour {

    private Animator anim;
    private PlayerController controller;

    [SerializeField] private PlayerDetection detection;

    private void Awake() {
        anim = GetComponent<Animator>();
        controller = GetComponentInParent<PlayerController>();
    }

    private void Update() {
        anim.SetFloat("MoveInput", controller.GetMoveInput());
        anim.SetFloat("YVelocity", controller.GetYVelocity());
        anim.SetBool("IsGrounded", detection.IsGrounded());
    }
}
