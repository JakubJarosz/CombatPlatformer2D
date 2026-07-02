using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer playerSprite;
    private PlayerJump playerJump;
    private PlayerAttack playerAttack;
    private PlayerBlock playerBlock;
    private PlayerDetection detection;

    [SerializeField] private GameInputs gameInput;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    // Movement variables
    private float moveInputs;

    // Event variables
    public event Action PerformJump;
    public event Action<bool> PerformBlock;
    public event Action PerformAttack;

    private enum PlayerState {
        Idle,
        Walk,
        Air,
        Attack,
        Block
    }

    private PlayerState currentState;
    private PlayerState previousState;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        playerJump = GetComponent<PlayerJump>();
        playerBlock = GetComponent<PlayerBlock>();
        playerAttack = GetComponent<PlayerAttack>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        detection = GetComponentInChildren<PlayerDetection>();
    }

    private void Start() {
        playerJump.JumpPerformed += PlayerJump_JumpPerformed;
        playerAttack.TryToAttack += PlayerAttack_TryToAttack;
    }

    private void Update() {
        moveInputs = gameInput.GetMoveInput();
        HandleState();
        Movement();
        switch (currentState) {
            case PlayerState.Idle:
                break;
            case PlayerState.Walk:
                HandleWalk();
                break;
            case PlayerState.Attack:
                HandleAttack();
                break;
            case PlayerState.Air:
                HandleAir();
                break;
            case PlayerState.Block:
                HandleBlock();
                break;
        }
        Debug.Log(currentState);
    }

    private void HandleState() {
        previousState = currentState;
        if (playerBlock.isBlocking && detection.IsGrounded() && playerAttack.isAttacking) {
            currentState = PlayerState.Block;
        } else if (!detection.IsGrounded()) {
            currentState = PlayerState.Air;
        } else if (moveInputs != 0) {
            currentState = PlayerState.Walk;
        } else {
            currentState = PlayerState.Idle;
        }
        if (currentState != previousState) {
            HandleStateChange();
        }
    }

    private void HandleStateChange() {
        bool IsBlocking = currentState == PlayerState.Block;
        PerformBlock?.Invoke(IsBlocking);
        GetComponent<Health>().CanTakeDamage(!IsBlocking);
        if (currentState == PlayerState.Attack) {
            PerformAttack?.Invoke();
        }
    }

    private void Movement() {
        rb.linearVelocity = new Vector2(moveInputs * moveSpeed, rb.linearVelocity.y);
    }

    private void HandleWalk() {
        Flip();
    }

    private void HandleAttack() {
        rb.linearVelocity = Vector2.zero;
    }

    private void HandleAir() {
        Flip();
    }
    // Event functions
    private void PlayerJump_JumpPerformed() {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        PerformJump?.Invoke();
    }

    private void PlayerAttack_TryToAttack() {
        if (detection.IsGrounded()) {
            currentState = PlayerState.Attack;
            PerformAttack?.Invoke();
        }
    }

    private void HandleBlock() {
        rb.linearVelocity = Vector2.zero;
    }

    // Helper functions
    private void Flip() {
        if (moveInputs == 0) return;
        playerSprite.flipX = moveInputs < 0;
    }

    // Return functions

    public float GetMoveInput() {
        return moveInputs != 0 ? 1 : 0;
    }

    public float GetYVelocity() {
        return rb.linearVelocity.y;
    }
}
