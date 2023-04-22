namespace Hybrid.Core.Dungeon;

public static class FloorGenerator
{
    internal const int RoomsWide = 4;
    internal const int RoomsHigh = 4;

    private static readonly Random random = new Random();

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

        var roomX = random.Next(RoomsWide);
        var roomY = random.Next(RoomsHigh);
        var current = rooms[roomX, roomY];

        var connected = new List<Room>() { current };
        while (connected.Count < RoomsWide * RoomsHigh)
        {
            var adjacents = Room.GetNeighbours(current, rooms).Except(connected);
            Room target = null;
            
            while (!adjacents.Any())
            {
                current = connected[random.Next(connected.Count)];
                adjacents = Room.GetNeighbours(current, rooms).Except(connected);
            }
            
            target = adjacents.ElementAt(random.Next(adjacents.Count()));
            current.ConnectTo(target);
            connected.Add(target);
            current = target;
        }
        return rooms;
    }
}