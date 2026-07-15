using UnityEngine;

public class ExitMarker : MonoBehaviour
{
    private Door door;

    private void Awake() {
        door = GetComponentInParent<Door>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponentInParent<PlayerController>() != null) {
            Transform player = collision.transform;
            door.HandleTriggerExit(player);
        }
    }
}
