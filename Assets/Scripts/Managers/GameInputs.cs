using System;
using UnityEngine;

public class GameInputs : MonoBehaviour
{
    private InputActions inputActions;

    public event Action JumpPressed;

    private void Awake() {
        inputActions = new InputActions();
        inputActions.Enable();
        inputActions.Player.Jump.performed += ctx => JumpPressed?.Invoke();
    }

    public float GetMoveInput() {
        return inputActions.Player.Move.ReadValue<float>();
    }
}
