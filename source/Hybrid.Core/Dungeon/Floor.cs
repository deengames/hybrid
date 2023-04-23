using Hybrid.Core.Dungeon.Generators;

namespace Hybrid.Core.Dungeon;

public class Floor
{
    internal const int RoomsWide = 4;
    internal const int RoomsHigh = 4;
    
    public Room[,] Rooms { get; private set; }
    public Room StartRoom { get; private set; }
    public int FloorNumber { get; private set; }

    public Floor(Room[,] rooms, Room start, int floorNumber)
    {
        this.Rooms = rooms;
        this.StartRoom = start;
        this.FloorNumber = floorNumber;
    }

    public IList<Room> QueryRooms {
        get
        {
            var toReturn = new List<Room>();
            for (var y = 0; y < Floor.RoomsHigh; y++)
            {
                for (var x = 0; x < Floor.RoomsWide; x++)
                {
                    toReturn.Add(this.Rooms[x, y]);
                }
            }
            return toReturn;
        }
    }
}