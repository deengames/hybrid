using System.Diagnostics;

namespace Hybrid.Core.Dungeon;

[DebuggerDisplay("{X}, {Y}")]
public class Room
{
    // monsters go in here
    public Room? North = null;
    public Room? East = null;
    public Room? South = null;
    public Room? West = null;
    public int X = -1;
    public int Y = -1;

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
            this.East = target;
            target.West = this;
        }
        else if (this.X > target.X)
        {
            this.West = target;
            target.East = this;
        }

        if (this.Y < target.Y)
        {
            this.South = target;
            target.North = this;
        }
        else if (this.Y > target.Y)
        {
            this.North = target;
            target.South = this;
        }
    }

    internal IEnumerable<Room> GetConnections()
    {
        var toReturn = new List<Room>();

        if (this.North != null)
        {
            toReturn.Add(this.North);
        }
        if (this.East != null)
        {
            toReturn.Add(this.East);
        }
        if (this.South != null)
        {
            toReturn.Add(this.South);
        }
        if (this.West != null)
        {
            toReturn.Add(this.West);
        }

        return toReturn;
    }
}