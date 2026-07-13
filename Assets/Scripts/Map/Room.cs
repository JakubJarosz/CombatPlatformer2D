using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private RoomDataSO roomDataSO;
    [SerializeField] private PolygonCollider2D roomBounds;
    [SerializeField] private EntryPoint[] entryPoints;

    public RoomName GetRoomName() {
        return roomDataSO.roomName;
    }

    public Transform GetEntryPoint(TransitionDirection dir) {
        foreach (var entryPoint in entryPoints) {
            if (entryPoint.direction == dir) 
                return entryPoint.point;
        }
        return null;
    }

    public PolygonCollider2D GetCameraBounds() {
        return roomBounds;
    }
}
