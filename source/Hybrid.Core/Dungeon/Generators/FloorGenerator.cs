namespace Hybrid.Core.Dungeon.Generators;

public static class FloorGenerator
{
    private static readonly Random _random = new Random();

    public static Floor Generate(int floorNumber)
    {
        var floor = GenerateStructure(floorNumber);
        AddRandomConnections(floor);
        GenerateStairs(floor);
        MonsterGenerator.Generate(floor);
        return floor;
    }

    private static void GenerateStairs(Floor floor)
    {
        var startRoom = floor.StartRoom;
        var currentRoom = floor.CurrentRoom;
        while (DistanceFrom(startRoom, currentRoom) <= 1.0)
        {
            currentRoom = floor.QueryRooms.OrderBy(r => _random.Next()).First();
        }
        floor.StairsRoom = currentRoom;
    }

    private static double DistanceFrom(Room startRoom, Room currentRoom)
    {
        return Math.Sqrt(Math.Pow(startRoom.X - currentRoom.X, 2f) + Math.Pow(startRoom.Y - currentRoom.Y, 2));
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
                var target = unconnected.ElementAt(_random.Next(unconnected.Count()));
                numConnections -= 1;
            }
        }
    }

    private static Floor GenerateStructure(int floorNumber)
    {
        var rooms = new Room[Floor.RoomsWide, Floor.RoomsHigh];
        for (int y = 0; y < Floor.RoomsHigh; y++)
        {
            for (int x = 0; x < Floor.RoomsWide; x++)
            {
                var room = new Room(x, y);
                rooms[x, y] = room;
            }
        }

        var startX = _random.Next(Floor.RoomsWide);
        var startY = _random.Next(Floor.RoomsHigh);
        var current = rooms[startX, startY];

        var connected = new List<Room>() { current };
        while (connected.Count < Floor.RoomsWide * Floor.RoomsHigh)
        {
            var adjacents = Room.GetNeighbours(current, rooms).Except(connected);
            Room target = null;
            
            while (!adjacents.Any())
            {
                current = connected[_random.Next(connected.Count)];
                adjacents = Room.GetNeighbours(current, rooms).Except(connected);
            }
            
            target = adjacents.ElementAt(_random.Next(adjacents.Count()));
            current.ConnectTo(target);
            connected.Add(target);
            current = target;
        }

        return new Floor(rooms, rooms[startX, startY], floorNumber);
    }

    private static Room GetRandomRoom(Floor floor)
    {
        var x = _random.Next(Floor.RoomsWide);
        var y = _random.Next(Floor.RoomsHigh);
        return floor.Rooms[x, y];
    }
}