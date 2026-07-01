using UnityEngine;

public class Health : MonoBehaviour, IDamageable {

    [SerializeField] private int health;


    public void TakeDamage(int damage) {
        health -= damage;
        Debug.Log(health);
    }
}
