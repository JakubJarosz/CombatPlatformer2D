using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    private Animator anim;
    private EnemyController controller;
    private Health health;

    private void Awake() {
        anim = GetComponent<Animator>();
        controller = GetComponentInParent<EnemyController>();
        health = GetComponentInParent<Health>();
    }

    private void Start() {
        controller.PerformMeleeAttack += EnemyAttack_PerformMeleeAttack;
        controller.PerformRangeAttack += EnemyAttack_PerformRangeAttack;
        health.TriggerDeath += Health_TriggerDeath;
    }

    private void Health_TriggerDeath() {
        anim.SetTrigger("Death");
    }

    private void Update() {
        anim.SetBool("Move", controller.IsWalking());
    }
    private void EnemyAttack_PerformRangeAttack() {
        anim.SetTrigger("Range");
    }

    private void EnemyAttack_PerformMeleeAttack() {
        anim.SetTrigger("Melee");
    }
}
