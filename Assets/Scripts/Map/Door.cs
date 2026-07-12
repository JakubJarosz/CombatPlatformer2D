using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private TransitionDirection dir;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            RoomManager.instance.Transition(dir);
        }
    }
}
