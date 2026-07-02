using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable {

    [SerializeField] private int health;
    [SerializeField] private float deathTimer;

    public event Action TriggerDeath;
    public event Action TriggerHurt;

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            Death();
        } else {
            TriggerHurt?.Invoke();
        }
    }

    private void Death() {
        TriggerDeath?.Invoke();
        StartCoroutine(DeathDelay());
    }

    private IEnumerator DeathDelay() {
        yield return new WaitForSeconds(deathTimer);
        Destroy(gameObject);
    }
}
