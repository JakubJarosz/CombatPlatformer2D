using System;
using System.Collections;
using UnityEngine;

public class AttackHitEffects : MonoBehaviour {

    private Rigidbody2D rb;
    public event Action<bool> IsKnocked;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyEffects(Vector2 dir, float force, float knockTime) {
        ApplyKnockback(dir, force, knockTime);
    }

    // Knocback Effect
    private void ApplyKnockback(Vector2 dir, float force, float knockTime) {
        IsKnocked?.Invoke(true);
        rb.AddForce(dir * force, ForceMode2D.Impulse);
        StartCoroutine(FinishKnockback(knockTime));
    }

    private IEnumerator FinishKnockback(float knockTime) {
        yield return new WaitForSeconds(knockTime);
        IsKnocked?.Invoke(false);
    }
}
