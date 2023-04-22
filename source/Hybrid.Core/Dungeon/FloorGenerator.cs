namespace Hybrid.Core.Dungeon;

public static class FloorGenerator
{
    internal const int RoomsWide = 4;
    internal const int RoomsHigh = 4;

    private static readonly Random random = new Random();

    // TODO: unit test. Give me a fully-connected dungeon, and I'm happy.
    public static Room[,] Generate()
    {
        var rooms = new Room[RoomsWide, RoomsHigh];
        for (int y = 0; y < RoomsHigh; y++)
        {
            for (int x = 0; x < RoomsWide; x++)
            {
                var room = new Room(x, y);
                rooms[x, y] = room;
            }
        }

        var done = new List<Room>(); // BFS: white
        var seen = new List<Room>(); // BFS: grey

        var roomX = random.Next(RoomsWide);
        var roomY = random.Next(RoomsHigh);
        var current = rooms[roomX, roomY];

        while (done.Count < RoomsWide * RoomsHigh)
        {
            var neighbours = Room.GetConnections(rooms, roomX, roomY);
            var unconnectedNeighbours = new List<Room>();

            foreach (var neighbour in neighbours)
            {
                if (!seen.Contains(neighbour) && !done.Contains(neighbour))
                {
                    seen.Add(neighbour);
                    unconnectedNeighbours.Add(neighbour);
                }
            }

            var connectTo = unconnectedNeighbours[random.Next(unconnectedNeighbours.Count)];
            current.ConnectTo(connectTo);
            seen.Remove(current);
            done.Add(current);

            var nextIndex = random.Next(seen.Count);
            {
                current = done[random.Next(done.Count)];
                neighbours = Room.GetConnections(rooms, current.X, current.Y);
                foreach (var neighbour in neighbours)
                {
                    // DUPLICATE CODE
                    if (!seen.Contains(neighbour) && !done.Contains(neighbour))
                    {
                        seen.Add(neighbour);
                    }
                }

            }
            var next = seen[nextIndex];
            seen.RemoveAt(nextIndex);
            current = next;
        }

        return rooms;
    }
}