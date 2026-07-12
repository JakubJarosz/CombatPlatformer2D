
[System.Serializable]
public class RoomConnection
{
    public RoomName fromRoom;
    public TransitionDirection fromDirection;

    public RoomName toRoom;
    public TransitionDirection toDirection;
}

public enum RoomName {
    DarkForest,
    LightForest,
    Graveyard,
    Caves
}

public enum TransitionDirection {
    Left,
    Top, 
    Right, 
    Bottom
}