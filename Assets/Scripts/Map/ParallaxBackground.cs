using Cinemachine;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;

    private Room belongRoom;

    private Transform cameraTransform;
    private Vector3 originalSpritePosition;
    private Vector3 lastCameraPosition;
    private float textureUnitSizeX;

    private void Awake() {
        belongRoom = GetComponentInParent<Room>();
    }

    private void Start() {
        cameraTransform = Camera.main.transform;
        originalSpritePosition = transform.position;
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        RoomManager.instance.playerLeftTheRoom += Instance_playerLeftTheRoom;
    }

    private void Instance_playerLeftTheRoom() {
        transform.position = originalSpritePosition;
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate() {
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
