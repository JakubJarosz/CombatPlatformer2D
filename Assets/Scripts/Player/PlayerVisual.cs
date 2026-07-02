using UnityEngine;

public class PlayerVisual : MonoBehaviour {

    private Animator anim;
    private PlayerController controller;
    private Health health;

    [SerializeField] private PlayerDetection detection;

    private void Awake() {
        anim = GetComponent<Animator>();
        controller = GetComponentInParent<PlayerController>();
        health = GetComponentInParent<Health>();
    }

    private void Start() {
        health.TriggerDeath += Health_TriggerDeath;
        health.TriggerHurt += Health_TriggerHurt;
    }

    private void Health_TriggerHurt() {
        anim.SetTrigger("Hurt");
    }

    private void Health_TriggerDeath() {
        anim.SetTrigger("Death");
    }

    private void Update() {
        anim.SetFloat("MoveInput", controller.GetMoveInput());
        anim.SetFloat("YVelocity", controller.GetYVelocity());
        anim.SetBool("IsGrounded", detection.IsGrounded());
    }
}
