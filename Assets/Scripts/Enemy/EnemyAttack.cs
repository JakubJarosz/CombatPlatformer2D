using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyDetection detection;

    [SerializeField] private AttackDataSO meleeAttackData;
    [SerializeField] private AttackDataSO rangeAttackData;
    [SerializeField] private float meleeAttackSpeed;
    [SerializeField] private float rangeAttackSpeed;

    private bool IsInMeleeRange;
    private bool IsInRangeRange;

    private float meleeTimer;
    private float rangeTimer;

    public event Action<AttackDataSO> PerformMeleeAttack;
    public event Action<AttackDataSO> PerformRangeAttack;

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
        if (IsInMeleeRange) {
            meleeTimer += Time.deltaTime;
            if (meleeTimer >= meleeAttackSpeed) {
                meleeTimer = 0f;
                PerformMeleeAttack?.Invoke(meleeAttackData);
            }
        } else {
            meleeTimer = meleeAttackSpeed / 2;
        }
    }

    private void RangeAttack() {
        if (IsInRangeRange && !IsInMeleeRange) {
            rangeTimer += Time.deltaTime;
            if (rangeTimer >= rangeAttackSpeed) {
                rangeTimer = 0f;
                PerformRangeAttack?.Invoke(rangeAttackData);
            }
        } else {
            rangeTimer = rangeAttackSpeed / 2;
        }
    }

    // Used for stagger logic
    public void Stagger() {
        meleeTimer = 0f;
        rangeTimer = 0f;
    }
}
