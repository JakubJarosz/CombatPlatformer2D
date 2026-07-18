using Cinemachine;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {
    [SerializeField] private Vector2 parallaxEffectMultiplier;

    private Room belongRoom;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private bool skipNextFrame;

    private Vector3 originalSpritePosition;
    private Vector3 spritePositionOnRoomLeave;

    private float lockTimer;
    private float lockDuration = 2f;

    private void Awake() {
        belongRoom = GetComponentInParent<Room>();
    }

    private void Start() {
        cameraTransform = Camera.main.transform;
        originalSpritePosition = transform.position;
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        RoomManager.instance.RoomEntered += Instance_playerLeftTheRoom;
        RoomManager.instance.ExitSequanceTriggered += Instance_ExitSequanceTriggered;
    }

    private void Instance_ExitSequanceTriggered() {
        spritePositionOnRoomLeave = transform.position;
    }

    private void Instance_playerLeftTheRoom(Room room) {
        if (room == belongRoom) return;

        lockTimer = lockDuration;
        transform.position = originalSpritePosition;
        lastCameraPosition = cameraTransform.position;
    }



    private void LateUpdate() {
        Vector3 currentCamPos = cameraTransform.position;
        Vector3 deltaMovement = currentCamPos - lastCameraPosition;

        if (belongRoom == RoomManager.instance.currentRoom) {
            bool hasSavedPosition = spritePositionOnRoomLeave != Vector3.zero;
            bool isLocked = lockTimer > 0f;

            if (!hasSavedPosition || !isLocked) {
                // Normal parallax movement
                Vector3 parallaxDelta = new Vector3(
                    deltaMovement.x * parallaxEffectMultiplier.x,
                    deltaMovement.y * parallaxEffectMultiplier.y,
                    0f);

                transform.position += parallaxDelta;
            } else {
                // Hold position temporarily
                transform.position = spritePositionOnRoomLeave;
                lockTimer -= Time.deltaTime;
            }
        }

        lastCameraPosition = currentCamPos;
    }
}
