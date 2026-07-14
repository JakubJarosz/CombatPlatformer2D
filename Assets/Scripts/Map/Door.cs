using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private TransitionDirection dir;
    [SerializeField] private Vector2 wallDoorSize;
    [SerializeField] private Vector2 floorDoorSize;

    private BoxCollider2D col;
    private Transform entryPoint;
    private Transform exitPoint;

    private float entryOffset = 2f;
    
    private void OnValidate() {
        col = GetComponentInChildren<BoxCollider2D>();
        entryPoint = GetComponentInChildren<EntryMarker>().transform;
        exitPoint = GetComponentInChildren<ExitMarker>().transform;

        UpdateChildPositioning();
    }

    private void UpdateChildPositioning() {
        entryPoint.localPosition = entryOffset * SetDirectionOffset();
        exitPoint.localPosition = Vector3.zero;
    }

    private Vector2 SetDirectionOffset() {
        switch (dir) {
            case TransitionDirection.Left:
                col.size = wallDoorSize;
                return Vector2.right;

            case TransitionDirection.Right:
                col.size = wallDoorSize;
                return Vector2.left;

            case TransitionDirection.Top:
                col.size = floorDoorSize;
                return Vector2.down;

            case TransitionDirection.Bottom:
                col.size = floorDoorSize;
                return Vector2.up;

            default:
                return Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponentInParent<PlayerController>() != null) {
            Transform player = collision.transform;
            RoomManager.instance.Transition(dir, player);
        }
    }

   
}

