using Cinemachine;
using System;
using System.Collections;
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
        currentRoom = rooms[connection.toRoom];
        Transform spawnLocation = currentRoom.GetEntryPoint(connection.toDirection);
        Debug.Log(spawnLocation);
        StartCoroutine(TransitionCoroutine(dir, player, spawnLocation));


        // Moving camera bound
        confiner.m_BoundingShape2D = currentRoom.GetCameraBounds();
    }

    private IEnumerator TransitionCoroutine(TransitionDirection dir, Transform player, Transform spawnLocation) {
        float dirNumb = dir == TransitionDirection.Right || dir == TransitionDirection.Top ? 1 : -1;
        PlayerController controller = player.GetComponent<PlayerController>();

        // Start Moving in the correct direction (still in the original room)
        controller.StartTransition(dirNumb);
        yield return new WaitForSeconds(1f);

        // After a sec teleport player to new Room
        player.position = spawnLocation.position;
        controller.MidTransition();
        playerLeftTheRoom?.Invoke();

        // Player in new room logic
        StartCoroutine(NewRoomCoroutine(controller));
    }

    private IEnumerator NewRoomCoroutine(PlayerController controller) {
        yield return new WaitForSeconds(1f);
        controller.EndTransition();
    }
}
