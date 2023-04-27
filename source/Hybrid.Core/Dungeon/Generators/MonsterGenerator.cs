using Hybrid.Core.Data;

namespace Hybrid.Core.Dungeon.Generators;

public class MonsterGenerator
{
    internal const int PointsPerFloorMultiplier = 10;
    private const int MaxMonsters = 5;

    private static Random _random = new Random();

    public static void Generate(Floor floor)
    {
        var floorNumber = floor.FloorNumber;
        // Floor number affects quality/type, and quantity. Non-linear difficulty.
        var totalPoints = floorNumber * PointsPerFloorMultiplier;

        // Potential monsters: floor number, +- 1
        var data = MonstersData.All;
        var minMonsterIndex = Math.Min(floorNumber - 1, data.Length - 1);
        var maxMonsterIndex = Math.Min(floorNumber + 1, data.Length - 1);

        var numPacks = 10; // experimentally derived
        var monsterRooms = floor.QueryRooms.OrderBy(r => _random.Next()).Take(numPacks);
        
        foreach (var room in monsterRooms)
        {
            // I don't know why some rooms appear twice even though every list has unique entries.
            // This is a stupid fix.
            room.Monsters.Clear();
            // GenerateMonstersByPoints(data, minMonsterIndex, maxMonsterIndex, room, totalPoints);
            var next = _random.Next(minMonsterIndex, maxMonsterIndex + 1);
            room.Monsters.Add(data[next]);
        }
    }

    private static void GenerateMonstersByPoints(Monster[] data, int minMonsterIndex, int maxMonsterIndex, Room room, int totalPoints)
    {
        var roomPointsLeft = totalPoints - _random.Next(0, (int)0.2 * totalPoints); // 80%-100%
        var iterations = MaxMonsters;
        while (roomPointsLeft > 0 && iterations-- > 0)
        {
            var next = _random.Next(minMonsterIndex, maxMonsterIndex + 1);
            var monster = data[next];
            if (roomPointsLeft >= monster.Cost)
            {
                roomPointsLeft -= monster.Cost;
                room.Monsters.Add(monster.Clone());
            }
        }
    }
}