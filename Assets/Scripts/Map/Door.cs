using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private TransitionDirection dir;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponentInParent<PlayerController>() != null) {
            Transform player = collision.transform;
            RoomManager.instance.Transition(dir, player);
        }
    }
}
