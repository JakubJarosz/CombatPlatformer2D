using UnityEngine;

public class PlayerBlock : MonoBehaviour {
    [SerializeField] private GameInputs inputs;

    public bool isBlocking { get; private set; }

    private void Start() {
        inputs.BlockHeld += Inputs_BlockHeld;
    }

    private void Inputs_BlockHeld(bool obj) {
        isBlocking = obj;
    }
}
