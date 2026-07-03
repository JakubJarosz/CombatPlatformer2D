using UnityEngine;

public class PlayerMeleeHitBox : MonoBehaviour
{
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask targetLayer;
    private PlayerController playerController;
    private PlayerAttack playerAttack;

    private Vector2 meleePosition;

    private void Awake() {
        playerController = GetComponentInParent<PlayerController>();
        playerAttack = GetComponentInParent<PlayerAttack>();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        if (playerController != null ) {
            Vector2 localPos = transform.localPosition;
            localPos.x *= playerController.facingDir;
            meleePosition = (Vector2)transform.parent.position + localPos;
            Gizmos.DrawWireSphere(meleePosition, attackRadius);
        }
    }

    public void MeleeHit() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(meleePosition, attackRadius, targetLayer);
        if (hits.Length == 0) return; 
        foreach(Collider2D hit in hits) {
            IDamageable damage = hit.GetComponent<IDamageable>();
            if (damage != null) {
                damage.TakeDamage(playerAttack.GetDamage());
            }
        }
    }
}
