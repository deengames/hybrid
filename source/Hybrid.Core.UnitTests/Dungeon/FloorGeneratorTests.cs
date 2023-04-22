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
        var expectedFloorNumber = 7;
        
        while (iterations++ < 100)
        {
            // Arrange/Act
            var actual = FloorGenerator.Generate(expectedFloorNumber);
            var connectedRooms = new List<Room>() { actual.Rooms[0, 0] };

            // Assert
            Assert.That(actual.FloorNumber, Is.EqualTo(expectedFloorNumber));
            Assert.That(actual.StartRoom, Is.Not.Null);
            Assert.That(actual.Rooms[actual.StartRoom.X, actual.StartRoom.Y], Is.EqualTo(actual.StartRoom));

            for (var y = 0; y < FloorGenerator.RoomsHigh; y++)
            {
                for (var x = 0; x < FloorGenerator.RoomsWide; x++)
                {
                    // Basic connection check for isolated rooms
                    var current = actual.Rooms[x, y];
                    Assert.That(current.West != null || current.East != null || current.South != null || current.North != null);

                    // Fully connected
                    if (current.West != null && !connectedRooms.Contains(current.West))
                    {
                        connectedRooms.Add(current.West);
                    }
                    if (current.East != null && !connectedRooms.Contains(current.East))
                    {
                        connectedRooms.Add(current.East);
                    }
                    if (current.North != null && !connectedRooms.Contains(current.North))
                    {
                        connectedRooms.Add(current.North);
                    }
                    if (current.South != null && !connectedRooms.Contains(current.South))
                    {
                        connectedRooms.Add(current.South);
                    }
                }
            }
            // Fully connected
            Assert.That(connectedRooms.Count, Is.EqualTo(FloorGenerator.RoomsWide * FloorGenerator.RoomsHigh));
        }
    }
}