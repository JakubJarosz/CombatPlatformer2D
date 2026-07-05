using System;
using UnityEngine;

[CreateAssetMenu]
public class AttackDataSO : ScriptableObject
{
    [Header("Damage stats")]
    public int damage;
    public int staggerDamage;

    [Header("Knockback Effects")]
    public float knockbackXForce;
    public float knockbackYForce;
    public float knockbackTime;
}
