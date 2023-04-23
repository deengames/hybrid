using Hybrid.Core.Dungeon.Generators;

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

    public IList<Room> QueryRooms {
        get
        {
            var toReturn = new List<Room>();
            for (var y = 0; y < FloorGenerator.RoomsHigh; y++)
            {
                for (var x = 0; x < FloorGenerator.RoomsWide; x++)
                {
                    toReturn.Add(this.Rooms[x, y]);
                }
            }
            return toReturn;
        }
    }
}