using UnityEngine;

public class PlayerBlock : MonoBehaviour {
    [SerializeField] private GameInputs inputs;
    [SerializeField] private float parryWindowTimer;

    public bool isBlocking { get; private set; }
    public bool canParry { get; private set; }
    private float parryTimer;

    private void Start() {
        inputs.BlockHeld += Inputs_BlockHeld;
    }

    // On Block Press Player Blocks and stars ParryWindow that lasts parryWindowTimer amount
    private void Update() {
        if (isBlocking) {
            parryTimer += Time.deltaTime;
            if (parryTimer <= parryWindowTimer ) {
                canParry = true;
            } else {
                canParry = false;
            }
        } else {
            parryTimer = 0f;
        }
    }

    private void Inputs_BlockHeld(bool obj) {
        isBlocking = obj;
    }
}
