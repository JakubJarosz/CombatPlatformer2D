using Cinemachine;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;

    private Room belongRoom;

    private Transform cameraTransform;
    private Vector3 originalSpritePosition;
    private Vector3 lastCameraPosition;
    private bool skipNextFrame;

    private void Awake() {
        belongRoom = GetComponentInParent<Room>();
    }

    private void Start() {
        cameraTransform = Camera.main.transform;
        originalSpritePosition = transform.position;
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        RoomManager.instance.roomEntered += Instance_playerLeftTheRoom;
        Debug.Log(originalSpritePosition);
    }

    private void Instance_playerLeftTheRoom(Room room) {
        //if (room != belongRoom) return;

        transform.position = originalSpritePosition;
        lastCameraPosition = cameraTransform.position;

        skipNextFrame = true; 
    }

    private void LateUpdate() {
        if (skipNextFrame) {
            lastCameraPosition = cameraTransform.position;
            skipNextFrame = false;
            return;
        }

        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        if (belongRoom == RoomManager.instance.currentRoom) {
            transform.position += new Vector3(
                deltaMovement.x * parallaxEffectMultiplier.x,
                deltaMovement.y * parallaxEffectMultiplier.y,
                0f);
        }

  
        lastCameraPosition = cameraTransform.position;
    }
}
