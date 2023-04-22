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
    internal static List<Room> GetNeighbours(Room room, Room[,] rooms)
    {
        var toReturn = new List<Room>();
        
        if (room.X > 0)
        {
            toReturn.Add(rooms[room.X - 1, room.Y]);
        }
        if (room.X < FloorGenerator.RoomsWide - 1)
        {
            toReturn.Add(rooms[room.X + 1, room.Y]);
        }

        if (room.Y > 0)
        {
            toReturn.Add(rooms[room.X, room.Y - 1]);
        }
        if (room.Y < FloorGenerator.RoomsHigh - 1)
        {
            toReturn.Add(rooms[room.X, room.Y + 1]);
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