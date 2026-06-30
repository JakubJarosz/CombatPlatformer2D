using System;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    [SerializeField] private GameInputs inputs;
    private PlayerDetection detection;

    public event Action JumpPerformed;

    private void Awake() {
        detection = GetComponentInChildren<PlayerDetection>();
    }

    private void Start() {
        inputs.JumpPressed += Inputs_JumpPressed;
    }

    private void Inputs_JumpPressed() {
        if (!detection.IsGrounded()) return;
        JumpPerformed?.Invoke();
    }
}
