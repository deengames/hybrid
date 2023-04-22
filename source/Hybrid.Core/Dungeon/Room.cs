using System.Diagnostics;

namespace Hybrid.Core.Dungeon;

[DebuggerDisplay("{X}, {Y}")]
public class Room
{
    // monsters go in here
    public Room? Up = null;
    public Room? Right = null;
    public Room? Down = null;
    public Room? Left = null;
    public int X = -1;
    public int Y = 0;

    public Room(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    // TODO: unit test
    internal static List<Room> GetConnections(Room[,] rooms, int roomX, int roomY)
    {
        var toReturn = new List<Room>();
        
        if (roomX > 0)
        {
            toReturn.Add(rooms[roomX - 1, roomY]);
        }
        if (roomX < FloorGenerator.RoomsWide - 1)
        {
            toReturn.Add(rooms[roomX + 1, roomY]);
        }

        if (roomY > 0)
        {
            toReturn.Add(rooms[roomX, roomY - 1]);
        }
        if (roomY < FloorGenerator.RoomsHigh - 1)
        {
            toReturn.Add(rooms[roomX, roomY + 1]);
        }

        return toReturn;
    }

    // TODO: test
    internal void ConnectTo(Room target)
    {
        if (this.X < target.X)
        {
            this.Right = target;
            target.Left = this;
        }
        else if (this.X > target.X)
        {
            this.Left = target;
            target.Right = this;
        }

        if (this.Y < target.Y)
        {
            this.Down = target;
            target.Up = this;
        }
        else if (this.Y > target.Y)
        {
            this.Up = target;
            target.Down = this;
        }
    }
}