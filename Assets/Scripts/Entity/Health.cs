using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable {

    [SerializeField] private int health;
    [SerializeField] private float hurtTimer;
    [SerializeField] private float deathTimer;

    public event Action TriggerDeath;
    public event Action<float> TriggerHurt;
    public event Action Hit;

    private bool canTakeDamage = true;

    public void TakeDamage(int damage) {
        Hit?.Invoke();
        if (!canTakeDamage) return;
        health -= damage;
        if (health <= 0) {
            Death();
        } else {
            TriggerHurt?.Invoke(hurtTimer);
            Debug.Log(health);
        }
    }

    public bool CanTakeDamage() {
        return canTakeDamage;
    }

    private void Death() {
        TriggerDeath?.Invoke();
        StartCoroutine(DeathDelay());
    }

    private IEnumerator DeathDelay() {
        yield return new WaitForSeconds(deathTimer);
        Destroy(gameObject);
    }

    public void CanTakeDamage(bool canTake) {
        canTakeDamage = canTake;
    }
}
