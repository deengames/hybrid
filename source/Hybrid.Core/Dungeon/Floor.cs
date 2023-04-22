namespace Hybrid.Core.Dungeon;

public class Floor
{
    public Room[,] Rooms { get; private set; }
    public Room StartRoom { get; private set; }
    public int FloorNumber { get; private set; }

    public Floor(Room[,] rooms, Room start, int floorNumber)
    {
        this.Rooms = rooms;
        this.StartRoom = start;
        this.FloorNumber = floorNumber;
    }
}