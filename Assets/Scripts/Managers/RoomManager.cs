using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;

    [SerializeField] private RoomGraphSO roomGraph;
    [SerializeField] private Room startingRoom;

    private Dictionary<RoomName, Room> rooms = new ();
    private Room currentRoom;

    private void Awake() {
        instance = this;

        // Cashing Rooms in Dictionary due to having only RoomName
        Room[] allRooms = FindObjectsByType<Room>(FindObjectsSortMode.None);
        foreach (var item in allRooms) {
            rooms.Add(item.GetRoomName(), item);
        }

        currentRoom = startingRoom;
    }

   
    public void Transition(TransitionDirection dir) {
        RoomConnection connection = roomGraph.FindConnection(currentRoom.GetRoomName(), dir);
        if (connection == null) return;

        //currentRoom = connection.
    }
}
