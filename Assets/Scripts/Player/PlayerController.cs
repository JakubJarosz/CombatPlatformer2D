using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer playerSprite;
    private PlayerDetection detection;

    [SerializeField] private GameInputs gameInput;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    // Movement variables
    private float moveInputs;

    private enum PlayerState {
        Idle,
        Walk,
        Jump,
        Fall,
        Attack
    }

    private PlayerState state;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        detection = GetComponentInChildren<PlayerDetection>();
    }

    private void Update() {
        moveInputs = gameInput.GetMoveInput();
        HandleState();
        switch (state) {
            case PlayerState.Idle:
                HandleIdle();
                break;
            case PlayerState.Walk:
                HandleWalk();
                break;
            case PlayerState.Attack:
                HandleAttack();
                break;
            case PlayerState.Jump:
                HandleJump();
                break;
            case PlayerState.Fall:
                HandleFall();
                break;

        }
    }

    private void HandleState() {
        if (!detection.IsGrounded()) {
            state = rb.linearVelocity.y > 0 ? PlayerState.Jump : PlayerState.Fall;
        } else if (moveInputs != 0) {
            state = PlayerState.Walk;
        } else {
            state = PlayerState.Idle;
        }
    }

    private void HandleIdle() {
        rb.linearVelocity = Vector2.zero;
    }

    private void HandleWalk() {
        rb.linearVelocity = new Vector2(moveInputs * moveSpeed, rb.linearVelocity.y);
        Flip();
    }

    private void HandleAttack() {
        rb.linearVelocity = Vector2.zero;
    }

    private void HandleJump() {
        rb.linearVelocity = new Vector2(moveInputs * moveSpeed, rb.linearVelocity.y);
        Flip();
    }

    private void HandleFall() {
        rb.linearVelocity = new Vector2(moveInputs * moveSpeed, rb.linearVelocity.y);
        Flip();
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
}
