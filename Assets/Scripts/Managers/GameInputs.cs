using System;
using UnityEngine;

public class GameInputs : MonoBehaviour
{
    private InputActions inputActions;

    public event Action JumpPressed;
    public event Action AttackPressed;
    public event Action<bool> BlockHeld;

    private void Awake() {
        inputActions = new InputActions();
        inputActions.Enable();
        inputActions.Player.Jump.performed += ctx => JumpPressed?.Invoke();
        inputActions.Player.Attack.performed += ctx => AttackPressed?.Invoke();
        inputActions.Player.Block.started += ctx => BlockHeld?.Invoke(true);
        inputActions.Player.Block.canceled += ctx => BlockHeld?.Invoke(false);
    }

    public float GetMoveInput() {
        return inputActions.Player.Move.ReadValue<float>();
    }

    public void DisableAllInputs() {
        inputActions.Disable();
    }
}
