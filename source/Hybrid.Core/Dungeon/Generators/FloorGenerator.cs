namespace Hybrid.Core.Dungeon.Generators;

public static class FloorGenerator
{
    internal const int RoomsWide = 4;
    internal const int RoomsHigh = 4;

    private static readonly Random random = new Random();

    public static Floor Generate(int floorNumber)
    {
        var floor = GenerateStructure(floorNumber);
        AddRandomConnections(floor);
        MonsterGenerator.Generate(floor);
        return floor;
    }

    private static void AddRandomConnections(Floor floor)
    {
        var numConnections = 4;
        while (numConnections > 0)
        {
            var source = GetRandomRoom(floor);
            var unconnected = Room.GetNeighbours(source, floor.Rooms).Except(source.GetConnections());
            if (unconnected.Any())
            {
                var target = unconnected.ElementAt(random.Next(unconnected.Count()));
                numConnections -= 1;
            }
        }
    }

    private static Floor GenerateStructure(int floorNumber)
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

        var startX = random.Next(RoomsWide);
        var startY = random.Next(RoomsHigh);
        var current = rooms[startX, startY];

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

        return new Floor(rooms, rooms[startX, startY], floorNumber);
    }

    private static Room GetRandomRoom(Floor floor)
    {
        var x = random.Next(RoomsWide);
        var y = random.Next(RoomsHigh);
        return floor.Rooms[x, y];
    }
}