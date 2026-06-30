using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    private Animator anim;
    private EnemyController controller;
    private EnemyAttack enemyAttack;

    private void Awake() {
        anim = GetComponent<Animator>();
        controller = GetComponentInParent<EnemyController>();
        enemyAttack = GetComponentInParent<EnemyAttack>();
    }

    private void Start() {
        enemyAttack.PerformMeleeAttack += EnemyAttack_PerformMeleeAttack;
        enemyAttack.PerformRangeAttack += EnemyAttack_PerformRangeAttack;
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
