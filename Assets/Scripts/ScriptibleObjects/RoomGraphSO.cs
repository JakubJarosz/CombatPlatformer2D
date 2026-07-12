using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rooms/Room Graph")]
public class RoomGraphSO : ScriptableObject
{
    public List<RoomConnection> connections = new List<RoomConnection>();

    public RoomConnection FindConnection(RoomName name, TransitionDirection dir) {
        foreach (var connection in connections) {
            if (connection.fromRoom == name && connection.fromDirection == dir) {
                return connection;
            }
        }
        return null;    
    }
}
