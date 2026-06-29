using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer enemySprite;
    private EnemyDetection detection;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    private enum EnemyState {
        Idle,
        Walk,
        Attack
    }

    private EnemyState state;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        enemySprite = GetComponentInChildren<SpriteRenderer>();
        detection = GetComponentInChildren<EnemyDetection>();
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
        }
        Debug.Log(state);
    }

    private void HandleState() {
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
    }

    private void HandleAttack() {
        rb.linearVelocity = Vector3.zero;
    }

    // Helper funciton
    private void Flip() {

    }
}
