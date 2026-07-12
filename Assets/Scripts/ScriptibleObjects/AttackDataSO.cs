using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack")]
public class AttackDataSO : ScriptableObject
{
    [Header("Damage stats")]
    public int damage;
    public int staggerDamage;

    [Header("Knockback Effect")]
    public float knockbackXForce;
    public float knockbackYForce;
    public float knockbackTime;

    [Header("Hitstop Effect")]
    public float hitstopTime;

    [Header("Camera shake")]
    public float cameraShakeTime;
    public float cameraShakeStrength;

}
