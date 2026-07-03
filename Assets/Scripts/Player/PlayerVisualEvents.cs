using UnityEngine;

public class PlayerVisualEvents : MonoBehaviour
{
    [SerializeField] private PlayerMeleeHitBox hitBox;
    private PlayerAttack attack;

    private void Awake() {
        attack = GetComponentInParent<PlayerAttack>();
    }

    public void AttackFinish() {
        attack.OnAttackFinish();
    }

    public void CanQueueAttack() {
        attack.CanQueueAttack();
        hitBox.MeleeHit();
    }
}
