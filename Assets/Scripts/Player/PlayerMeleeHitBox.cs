using System;
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
                // Apply damage
                AttackDataSO attackData = playerAttack.GetCurrentAttackData();
                damage.TakeDamage(attackData.damage);

                // Apply stagger
                Stagger stagger = hit.GetComponent<Stagger>();
                stagger.DealStaggerDamage(attackData.staggerDamage);

                // Apply effects if there are any
                AttackHitEffects hitEffects = hit.GetComponent<AttackHitEffects>();
                if (hitEffects != null) {
                    float dir = Mathf.Sign(hit.transform.position.x - transform.parent.position.x);
                    Vector2 knockbackDir = new Vector2(dir, attackData.knockbackYForce);
                    hitEffects.ApplyEffects(knockbackDir, attackData.knockbackXForce, attackData.knockbackTime);
                }
            }
        }
    }
}
