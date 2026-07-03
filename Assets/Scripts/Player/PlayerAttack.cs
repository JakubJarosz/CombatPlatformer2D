using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameInputs inputs;
    [SerializeField] private AttackDataSO lightMeleeSO;
    [SerializeField] private AttackDataSO heavyMeleeSO;

    public bool isAttacking { get; private set; }
    private bool canQueueAttack;
    private bool attackInputValidated;
    private int comboCount;

    public event Action TryToAttack;
    public event Action EndAttack;

    private void Start() {
        inputs.AttackPressed += Inputs_AttackPressed;
    }

    private void Inputs_AttackPressed() {
        if (!isAttacking) {
            TryToAttack?.Invoke();
            return;
        }
        if (canQueueAttack) {
            attackInputValidated = true;
            canQueueAttack = false;
            return;
        }
    }

    public void ResetCombo() {
        isAttacking = false;
        canQueueAttack = false;
        comboCount = 0;
        EndAttack?.Invoke();
    }

    public void StartAttack() {
        isAttacking = true;
        attackInputValidated = false;
        comboCount++;
    }

    public void CanQueueAttack() {
        canQueueAttack = true;
    }

    public void OnAttackFinish() {
        if (attackInputValidated) {
            if (comboCount == 3)
                comboCount = 0;
            StartAttack();
            TryToAttack?.Invoke();
        } else {
            ResetCombo();
        }
    }

    public int GetDamage() {
        return comboCount == 3 ? heavyMeleeSO.damage : lightMeleeSO.damage;
    }
}
