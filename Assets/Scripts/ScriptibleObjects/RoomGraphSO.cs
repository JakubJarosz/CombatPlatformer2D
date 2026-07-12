using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rooms/Room Graph")]
public class RoomGraphSO : ScriptableObject
{
    public List<RoomConnection> connections = new List<RoomConnection>();
}
