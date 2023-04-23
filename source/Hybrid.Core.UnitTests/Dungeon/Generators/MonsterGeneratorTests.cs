using Hybrid.Core.Dungeon.Generators;
using NUnit.Framework;

namespace Hybrid.Core.UnitTests.Dungeon.Generators;

[TestFixture]
class MonsterGeneratorTests
{
    [Test]
    public void Generate_GeneratesAtMostEnoughMonsters()
    {
        for (var floorNumber = 1; floorNumber <= 10; floorNumber++)
        {
            // Arrange
            var floor = FloorGenerator.Generate(floorNumber);
            // Act
            MonsterGenerator.Generate(floor);
            // Assert
            var expectedPoints = floorNumber * MonsterGenerator.PointsPerFloorMultiplier;
            foreach (var room in floor.Rooms)
            {
                var roomMonsterCost = room.Monsters.Sum(m => m.Cost);
                Assert.That(roomMonsterCost == 0 || roomMonsterCost <= expectedPoints,
                    $"Room {room.X},{room.Y} cost on floor {floorNumber} should be zero or less than {expectedPoints}, but it's {roomMonsterCost}: " + String.Join(", ", room.Monsters));
            }
        }
    }
}