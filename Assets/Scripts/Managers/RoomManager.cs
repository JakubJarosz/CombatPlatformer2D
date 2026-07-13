using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;

    [SerializeField] private RoomGraphSO roomGraph;
    [SerializeField] private Room startingRoom;

    private CinemachineConfiner2D confiner;

    private Dictionary<RoomName, Room> rooms = new ();
    public Room currentRoom { get; private set; }

    public event Action playerLeftTheRoom;

    private void Awake() {
        instance = this;
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();

        // Cashing Rooms in Dictionary due to having only RoomName
        Room[] allRooms = FindObjectsByType<Room>(FindObjectsSortMode.None);
        foreach (var item in allRooms) {
            rooms.Add(item.GetRoomName(), item);
        }

        currentRoom = startingRoom;
    }

    public void Transition(TransitionDirection dir, Transform player) {
        RoomConnection connection = roomGraph.FindConnection(currentRoom.GetRoomName(), dir);
        if (connection == null) return;

        playerLeftTheRoom?.Invoke();
        currentRoom = rooms[connection.toRoom];
        // Moving the Player to connected belongRoom
        Transform spawnLocation = currentRoom.GetEntryPoint(connection.toDirection);
        player.position = spawnLocation.position;

        // Moving camera bound
        confiner.m_BoundingShape2D = currentRoom.GetCameraBounds();
    }
}
