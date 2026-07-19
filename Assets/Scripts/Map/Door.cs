using System;
using UnityEngine;

public class Door : MonoBehaviour {
    [SerializeField] private TransitionDirection dir;
    [SerializeField] private Vector2 wallDoorSize;
    [SerializeField] private Vector2 floorDoorSize;

    private BoxCollider2D col;
    private Transform entryPoint;
    private Transform exitPoint;

    private float entryOffset = 1.3f;

    private void OnValidate() {
        col = GetComponentInChildren<BoxCollider2D>();
        entryPoint = GetComponentInChildren<EntryMarker>().transform;
        exitPoint = GetComponentInChildren<ExitMarker>().transform;

        UpdateChildPositioning();
    }

    private void UpdateChildPositioning() {
        // + Vector to position entry a little lower so the Player does not spawn in the air, (only for left and right)
        if (SetDirectionOffset() == Vector2.right || SetDirectionOffset() == Vector2.left) {
            entryPoint.localPosition = (entryOffset * SetDirectionOffset()) + new Vector2(0f, -1.1f);
        } else {
            entryPoint.localPosition = (entryOffset * SetDirectionOffset());
        }

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

    public void HandleTriggerExit(Transform player) {
        RoomManager.instance.Transition(dir, player);
    }

}

