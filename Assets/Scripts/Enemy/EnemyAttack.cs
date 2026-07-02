using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyDetection detection;

    [SerializeField] private AttackDataSO attackData;
    [SerializeField] private float meleeAttackSpeed;
    [SerializeField] private float rangeAttackSpeed;

    private bool IsInMeleeRange;
    private bool IsInRangeRange;

    private float meleeTimer;
    private float rangeTimer;

    public event Action PerformMeleeAttack;
    public event Action<int> PerformRangeAttack;

    private void Awake() {
        detection = GetComponentInChildren<EnemyDetection>();
    }

    private void Update() {
        IsInMeleeRange = detection.IsInMeleeRange();
        IsInRangeRange = detection.IsInRangeRange();
        MeleeAttack();
        RangeAttack();
    }

    private void MeleeAttack() {
        if (GameManager.instance.isPlayerDead) return;
        if (IsInMeleeRange) {
            meleeTimer += Time.deltaTime;
            if (meleeTimer >= meleeAttackSpeed) {
                meleeTimer = 0f;
                PerformMeleeAttack?.Invoke();
            }
        } else {
            meleeTimer = meleeAttackSpeed / 2;
        }
    }

    private void RangeAttack() {
        if (GameManager.instance.isPlayerDead) return;
        if (IsInRangeRange) {
            rangeTimer += Time.deltaTime;
            if (rangeTimer >= rangeAttackSpeed) {
                rangeTimer = 0f;
                PerformRangeAttack?.Invoke(attackData.damage);
            }
        } else {
            rangeTimer = rangeAttackSpeed / 2;
        }
    }
}
