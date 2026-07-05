using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    private Animator anim;
    private EnemyController controller;

    private void Awake() {
        anim = GetComponent<Animator>();
        controller = GetComponentInParent<EnemyController>();
    }

    private void Start() {
        controller.PerformMeleeAttack += EnemyAttack_PerformMeleeAttack;
        controller.PerformRangeAttack += EnemyAttack_PerformRangeAttack;
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
