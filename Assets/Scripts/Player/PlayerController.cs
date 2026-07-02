using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer playerSprite;
    private PlayerJump playerJump;
    private PlayerDetection detection;

    [SerializeField] private GameInputs gameInput;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    // Movement variables
    private float moveInputs;

    // Event variables
    public event Action PerformJump;

    private enum PlayerState {
        Idle,
        Walk,
        Air,
        Attack
    }

    private PlayerState state;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        playerJump = GetComponent<PlayerJump>();
        col = GetComponent<Collider2D>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        detection = GetComponentInChildren<PlayerDetection>();
    }

    private void Start() {
        playerJump.JumpPerformed += PlayerJump_JumpPerformed;
    }

    private void Update() {
        moveInputs = gameInput.GetMoveInput();
        HandleState();
        Movement();
        switch (state) {
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
        }
        Debug.Log(state);
    }

    private void HandleState() {
        if (!detection.IsGrounded()) {
            state = PlayerState.Air;
        } else if (moveInputs != 0) {
            state = PlayerState.Walk;
        } else {
            state = PlayerState.Idle;
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
