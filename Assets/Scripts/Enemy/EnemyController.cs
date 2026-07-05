using System;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private Rigidbody2D rb;
    private SpriteRenderer enemySprite;
    private EnemyDetection detection;
    private EnemyAttack enemyAttack;
    private Health health;
    private AttackHitEffects attackEffects;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    private bool isKnocked;
    private bool isDead;

    private enum EnemyState {
        Idle,
        Walk,
        Attack,
        Stunned
    }

    private EnemyState state;

    public event Action PerformMeleeAttack;
    public event Action PerformRangeAttack;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        enemyAttack = GetComponent<EnemyAttack>();
        attackEffects = GetComponent<AttackHitEffects>();
        health = GetComponent<Health>();
        enemySprite = GetComponentInChildren<SpriteRenderer>();
        detection = GetComponentInChildren<EnemyDetection>();
    }

    private void Start() {
        enemyAttack.PerformMeleeAttack += EnemyAttack_PerformMeleeAttack;
        enemyAttack.PerformRangeAttack += EnemyAttack_PerformRangeAttack;
        health.TriggerDeath += Health_TriggerDeath;
        attackEffects.IsKnocked += AttackEffects_IsKnocked;
    }

    private void Update() {
        HandleState();

        switch (state) {
            case EnemyState.Idle:
                HandleIdle();
                break;
            case EnemyState.Walk:
                HandleWalk();
                break;
            case EnemyState.Attack:
                HandleAttack();
                break;
            case EnemyState.Stunned:
                HandleStunned();
                break;
        }
    }

    private void HandleState() {
        if (isKnocked || isDead) {
            state = EnemyState.Stunned;
            return;
        }

        if (detection.IsInMeleeRange() || detection.IsInRangeRange()) {
            state = EnemyState.Attack;
        } else if (detection.IsPlayerDetected()) {
            state = EnemyState.Walk;
        } else {
            state = EnemyState.Idle;
        }
    }

    private void HandleIdle() {
        rb.linearVelocity = Vector3.zero;
    }

    private void HandleWalk() {
        rb.linearVelocity = new Vector2(moveSpeed * detection.FacingDir(), rb.linearVelocity.y);
        Flip();
    }

    private void HandleAttack() {
        rb.linearVelocity = Vector3.zero;
        Flip();
    }

    private void HandleStunned() {
        
    }

    private void EnemyAttack_PerformRangeAttack(AttackDataSO obj) {
        if (state == EnemyState.Attack) {
            PerformRangeAttack?.Invoke();
        }
    }

    private void EnemyAttack_PerformMeleeAttack(AttackDataSO obj) {
        if (state == EnemyState.Attack) {
            PerformMeleeAttack?.Invoke();
        }
    }

    private void AttackEffects_IsKnocked(bool value) {
        isKnocked = value;
    }

    private void Health_TriggerDeath() {
        gameObject.layer = LayerMask.NameToLayer("Dead");
        isDead = true;
    }

    private void Flip() {
        enemySprite.flipX = detection.FacingDir() < 0;
    }

    public bool IsWalking() {
        return state == EnemyState.Walk;
    }
}