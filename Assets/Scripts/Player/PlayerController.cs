using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb;
    private SpriteRenderer playerSprite;
    private Health playerHealth;
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
    public float facingDir { get; private set; }

    private bool isInvincible;

    private bool isTransitioning;
    private TransitionDirection transitionDir;
    private bool isMidTransition;
    private bool transitionJump;

    // Event variables
    public event Action PerformJump;
    public event Action<bool> PerformBlock;
    public event Action PerformParry;
    public event Action PerformAttack;

    private enum PlayerState {
        Idle,
        Walk,
        Air,
        Attack,
        Block,
        Transition,
    }

    private PlayerState currentState;
    private PlayerState previousState;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<Health>();
        playerJump = GetComponent<PlayerJump>();
        playerBlock = GetComponent<PlayerBlock>();
        playerAttack = GetComponent<PlayerAttack>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        detection = GetComponentInChildren<PlayerDetection>();
    }

    private void Start() {
        playerJump.JumpPerformed += PlayerJump_JumpPerformed;
        playerAttack.TryToAttack += PlayerAttack_TryToAttack;
        playerAttack.RequestNextAttack += PlayerAttack_RequestNextAttack;
        playerAttack.EndAttack += PlayerAttack_EndAttack;
        playerHealth.TriggerHurt += PlayerHealth_TriggerHurt;
        playerHealth.TriggerDeath += PlayerHealth_TriggerDeath;
        playerHealth.Hit += PlayerHealth_Hit;
    }

    private void Update() {
        if (!isTransitioning) {
            moveInputs = gameInput.GetMoveInput();
        }

        HandleState();
        Movement();
        MovePlayerDuringTransition();
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
            case PlayerState.Transition:
                HandleTransition();
                break;
        }
    }

    private void HandleState() {
        previousState = currentState;
        if (isTransitioning) {
            currentState = PlayerState.Transition;
            return;
        }
        if (currentState == PlayerState.Attack) return;
        if (playerBlock.isBlocking && detection.IsGrounded()) {
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
        if (!isInvincible) {
            playerHealth.CanTakeDamage(!IsBlocking);
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

    private void HandleBlock() {
        rb.linearVelocity = Vector2.zero;
    }

    private void HandleTransition() {

    }

    // Event functions
    private void PlayerJump_JumpPerformed() {
        if (currentState == PlayerState.Transition) return;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        PerformJump?.Invoke();
    }

    private void PlayerAttack_TryToAttack() {
        if (currentState == PlayerState.Transition) return;
        if (detection.IsGrounded()) {
            currentState = PlayerState.Attack;
            playerAttack.StartAttack();
            PerformAttack?.Invoke();
        }
    }

    private void PlayerAttack_RequestNextAttack() {
        if (detection.IsGrounded()) {
            playerAttack.StartAttack();
            PerformAttack?.Invoke();
        }
    }

    private void PlayerAttack_EndAttack() {
        currentState = PlayerState.Idle;
    }

    private void PlayerHealth_TriggerHurt(float hurtTime) {
        playerAttack.ResetCombo();
        InvincibilityEffect invEff = GetComponent<InvincibilityEffect>();
        if (invEff != null) {
            invEff.StartInvFrames(playerSprite, hurtTime);
            playerHealth.CanTakeDamage(false);
            isInvincible = true;
            StartCoroutine(InviTimer(hurtTime));
        }
    }

    private IEnumerator InviTimer(float hurtTime) {
        yield return new WaitForSeconds(hurtTime);
        isInvincible = false;
        playerHealth.CanTakeDamage(true);
    }

    private void PlayerHealth_Hit() {
        if (playerBlock.canParry) {
            PerformParry?.Invoke();
        }
    }

    private void PlayerHealth_TriggerDeath() {
        gameObject.layer = LayerMask.NameToLayer("Dead");
        gameInput.DisableAllInputs();
    }

    // Helper functions
    private void Flip() {
        if (moveInputs == 0) return;
        playerSprite.flipX = moveInputs < 0;
        facingDir = moveInputs > 0 ? 1 : -1;
    }

    // Return functions

    public float GetMoveInput() {
        return moveInputs != 0 ? 1 : 0;
    }

    public float GetYVelocity() {
        return rb.linearVelocity.y;
    }

    // Used in TransitionRoom inside RoomManager
    public void StartTransition(TransitionDirection dir) {
        isTransitioning = true;
        rb.gravityScale = 0f;
        transitionDir = dir;
    }

    private void MovePlayerDuringTransition() {
        if (!isTransitioning || transitionJump) return;
        float dirNumb = transitionDir == TransitionDirection.Right || transitionDir == TransitionDirection.Top ? 1 : -1;
        if (transitionDir == TransitionDirection.Left || transitionDir == TransitionDirection.Right) {
            rb.linearVelocity = new Vector2(dirNumb * moveSpeed, rb.linearVelocity.y);
            // used to trigger walk animation
            moveInputs = 1;
        } else if (transitionDir == TransitionDirection.Bottom) {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        } else {
            rb.linearVelocity = new Vector2(0f, dirNumb * 12f); // 12 is how fast  the player gets Succked in during transition
        }


        if (isMidTransition) {
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void MidTransition() {
        isMidTransition = true;
        rb.gravityScale = 1f;
    }

    public void PlayerInNewRoomTranistion() {
        isMidTransition = false;
        if (transitionDir == TransitionDirection.Top) {
            transitionJump = true;
            rb.linearVelocity = new Vector2(1f, jumpForce/1.5f);
        }
    }

    public void EndTransition() {
        transitionJump = false;
        isMidTransition = false;
        isTransitioning = false;
    }
}
