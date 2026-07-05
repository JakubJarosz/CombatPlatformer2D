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
    private int comboStep;
    private AttackDataSO currentAttackData;

    public event Action TryToAttack;
    public event Action RequestNextAttack;
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
        comboStep = 0;
        EndAttack?.Invoke();
    }

    public void StartAttack() {
        isAttacking = true;
        attackInputValidated = false;
        comboStep++;
        currentAttackData = GetAttackData();
        if (comboStep > 3)
            comboStep = 1;
    }

    public void CanQueueAttack() {
        canQueueAttack = true;
    }

    public void OnAttackFinish() {
        if (attackInputValidated) {
            RequestNextAttack?.Invoke();
        } else {
            ResetCombo();
        }
    }

    private AttackDataSO GetAttackData() {
        return comboStep == 3 ? heavyMeleeSO : lightMeleeSO;
    }

    public AttackDataSO GetCurrentAttackData() {
        return currentAttackData;
    }
}
