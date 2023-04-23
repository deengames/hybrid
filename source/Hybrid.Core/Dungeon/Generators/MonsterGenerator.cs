using Hybrid.Core.Data;

namespace Hybrid.Core.Dungeon.Generators;

public class MonsterGenerator
{
    private static Random random = new Random();

    public static void Generate(Floor floor)
    {
        var floorNumber = floor.FloorNumber;
        // Floor number affects quality/type, and quantity. Non-linear difficulty.
        var totalCost = floorNumber * 10;

        // Potential monsters: floor number, +- 1
        var data = MonstersData.All;
        var minMonsterIndex = Math.Min(floorNumber - 1, data.Length - 1);
        var maxMonsterIndex = Math.Min(floorNumber + 1, data.Length - 1);

        var numPacks = FloorGenerator.RoomsWide * FloorGenerator.RoomsHigh / 2;
        var monsterRooms = floor.QueryRooms.OrderBy(r => random.Next()).Take(numPacks);
        
        foreach (var room in monsterRooms)
        {
            var roomCost = totalCost;
            var iterations = 10;
            while (iterations-- > 0 && roomCost > 0)
            {
                var next = random.Next(minMonsterIndex, maxMonsterIndex + 1);
                var monster = data[next].Clone();
                if (monster.Cost <= roomCost)
                {
                    roomCost -= monster.Cost;
                    room.Monsters.Add(monster);
                }
            }
            Console.WriteLine("Done: " + string.Join(", ", room.Monsters));
        }
    }
}