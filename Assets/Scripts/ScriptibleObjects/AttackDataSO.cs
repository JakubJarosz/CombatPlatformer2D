using System;
using UnityEngine;

[CreateAssetMenu]
public class AttackDataSO : ScriptableObject
{
    public int damage;

    [Header("Knockback Effects")]
    public float knockbackXForce;
    public float knockbackYForce;
    public float knockbackTime;
}
