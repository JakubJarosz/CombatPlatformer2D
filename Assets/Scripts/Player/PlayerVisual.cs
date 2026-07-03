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
        controller.PerformBlock += Controller_PerformBlock;
        controller.PerformParry += Controller_PerformParry;
        controller.PerformAttack += Controller_PerformAttack;
        health.TriggerDeath += Health_TriggerDeath;
        health.TriggerHurt += Health_TriggerHurt;
    }

    private void Controller_PerformParry() {
        anim.SetTrigger("Parry");
    }

    private void Controller_PerformAttack() {
        anim.SetTrigger("Attack");
    }

    private void Controller_PerformBlock(bool obj) {
        anim.SetBool("Block", obj);
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
