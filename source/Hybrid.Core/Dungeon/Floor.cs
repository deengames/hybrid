namespace Hybrid.Core.Dungeon;

public class Floor
{
    public Room[,] Rooms { get; private set; }
    public Room StartRoom { get; private set; }

    public Floor(Room[,] rooms, Room start)
    {
        this.Rooms = rooms;
        this.StartRoom = start;
    }
}