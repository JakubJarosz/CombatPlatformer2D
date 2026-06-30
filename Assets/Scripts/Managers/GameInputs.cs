using UnityEngine;

public class GameInputs : MonoBehaviour
{
    private InputActions inputActions;

    private void Awake() {
        inputActions = new InputActions();
        inputActions.Enable();


    }

    public float GetMoveInput() {
        return inputActions.Player.Move.ReadValue<float>();
    }
}
