using UnityEngine;

public class EnemyMeleeHitBox : MonoBehaviour
{

    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private EnemyDetection enemyDetection;
    private EnemyAttack enemyAttack;

    private Vector2 meleePosition;
    private int meleeDamage;

    private void Awake() {
        enemyAttack = GetComponentInParent<EnemyAttack>();
    }

    private void Start() {
        enemyAttack.PerformMeleeAttack += EnemyAttack_PerformMeleeAttack;
    }

    private void EnemyAttack_PerformMeleeAttack(AttackDataSO obj) {
        meleeDamage = obj.damage;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        if (enemyDetection != null) {
            Vector2 localPos = transform.localPosition;
            localPos.x *= enemyDetection.FacingDir();
            meleePosition = (Vector2)transform.parent.position + localPos;
            Gizmos.DrawWireSphere(meleePosition, attackRadius);
        }
    }

    public void MeleeHit() {
        Collider2D hit = Physics2D.OverlapCircle(meleePosition, attackRadius, targetLayer);
        if (hit == null) return;
        IDamageable damage = hit.GetComponent<IDamageable>();
        if (damage != null) {
            damage.TakeDamage(meleeDamage);
        }
    }
}
