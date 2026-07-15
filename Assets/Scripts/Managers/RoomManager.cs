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
    [SerializeField] private FadeAwayEffect fadeEffect;

    private CinemachineConfiner2D confiner;

    private Dictionary<RoomName, Room> rooms = new ();
    public Room currentRoom { get; private set; }

    public event Action<Room> roomEntered;

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

        //Start transition
        StartCoroutine(TransitionCoroutine(dir, player, spawnLocation));
    }

    private IEnumerator TransitionCoroutine(TransitionDirection dir, Transform player, Transform spawnLocation) {
        float dirNumb = dir == TransitionDirection.Right || dir == TransitionDirection.Top ? 1 : -1;
        PlayerController controller = player.GetComponent<PlayerController>();
        float timer = 1f; // timer for fading in and out screen as well as for how long the player moves automaticly

        // Start Moving in the correct direction (still in the original room) and fade in the screen
        controller.StartTransition(dirNumb);
        StartCoroutine(fadeEffect.FadeIn(timer));

        yield return new WaitForSeconds(timer + 0.2f);
   
        // After a sec teleport player to new Room and change the camera bound
        player.position = spawnLocation.position;
        controller.MidTransition();
        confiner.m_BoundingShape2D = currentRoom.GetCameraBounds();
        roomEntered?.Invoke(currentRoom); // fix the background
        // Player in new room logic
        StartCoroutine(NewRoomCoroutine(controller, timer));
    }

    private IEnumerator NewRoomCoroutine(PlayerController controller, float timer) {
        StartCoroutine(fadeEffect.FadeOut(timer));
        yield return new WaitForSeconds(timer);
        controller.EndTransition();
    }
}
