using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private RoomDataSO roomDataSO;
    [SerializeField] private PolygonCollider2D roomBounds;
    [SerializeField] private EntryPoint[] entryPoints;

    public RoomName GetRoomName() {
        return roomDataSO.roomName;
    }
}
