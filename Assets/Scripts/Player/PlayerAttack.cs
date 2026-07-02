using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameInputs inputs;
    [SerializeField] private AttackDataSO attackDataSO;

    public bool isAttacking { get; private set; }
    private bool canQueueAttack;
    private bool attackInputValidated;
    private int comboCount;

    public event Action TryToAttack;

    private void Start() {
        inputs.AttackPressed += Inputs_AttackPressed;
    }

    private void Inputs_AttackPressed() {
        if (!isAttacking) {
            TryAttacking();
            return;
        }
        if (canQueueAttack) {
            attackInputValidated = true;
            canQueueAttack = false;
            return;
        }
        ;
    }

    private void TryAttacking() {
        isAttacking = true;
        attackInputValidated = false;
        comboCount++;
        TryToAttack?.Invoke();
    }

    private void ResetCombo() {
        isAttacking = false;
        canQueueAttack = false;
        comboCount = 0;
    }

    public void CanQueueAttack() {
        canQueueAttack = true;
    }

    public void OnAttackFinish() {
        if (attackInputValidated) {
            if (comboCount == 3)
                comboCount = 0;
            TryAttacking();
        } else {
            ResetCombo();
        }
    }

    //public int GetDamage() {
    //    return comboCount == 3 ? attackSO[1].damage : attackSO[0].damage;
    //}
}
