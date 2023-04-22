using Hybrid.Core.Dungeon;
using NUnit.Framework;

namespace Hybrid.Core.UnitTests.Dungeon;

[TestFixture]
class FloorGeneratorTests
{
    [Test]
    public void Generate_GeneratesConnectedDungeon()
    {
        var iterations = 0;
        while (iterations++ < 100)
        {
            // Arrange/Act
            var actual = FloorGenerator.Generate();
            var connectedRooms = new List<Room>() { actual[0, 0] };

            // Assert
            for (var y = 0; y < FloorGenerator.RoomsHigh; y++)
            {
                for (var x = 0; x < FloorGenerator.RoomsWide; x++)
                {
                    // Basic connection check for isolated rooms
                    var current = actual[x, y];
                    Assert.That(current.Left != null || current.Right != null || current.Down != null || current.Up != null);

                    // Fully connected
                    if (current.Left != null && !connectedRooms.Contains(current.Left))
                    {
                        connectedRooms.Add(current.Left);
                    }
                    if (current.Right != null && !connectedRooms.Contains(current.Right))
                    {
                        connectedRooms.Add(current.Right);
                    }
                    if (current.Up != null && !connectedRooms.Contains(current.Up))
                    {
                        connectedRooms.Add(current.Up);
                    }
                    if (current.Down != null && !connectedRooms.Contains(current.Down))
                    {
                        connectedRooms.Add(current.Down);
                    }
                }
            }
            // Fully connected
            Assert.That(connectedRooms.Count, Is.EqualTo(FloorGenerator.RoomsWide * FloorGenerator.RoomsHigh));
        }
    }
}