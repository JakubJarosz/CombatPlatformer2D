using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private PlayerController playerController;

    public bool isPlayerDead = false;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        Health health = playerController.GetComponent<Health>();
        health.TriggerDeath += Health_TriggerDeath;
    }

    private void Health_TriggerDeath() {
        isPlayerDead = true;
    }
}
