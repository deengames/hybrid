using Hybrid.Core.Character;
using NUnit.Framework;

namespace Hybrid.Core.UnitTests.Character;

[TestFixture]
public class PlayerTests
{
    [Test]
    public void Constructor_SetsSkillPointsToPositiveValue()
    {
        // Arrange/Act
        var player = new Player();
        // Assert
        Assert.That(player.SkillPoints, Is.GreaterThan(0));
    }

    [Test]
    public void Learn_ReturnsTrue_ForNewlyLearnedSkills()
    {
        // Arrange
        var player = new Player();

        // Act/Assert
        Assert.True(player.Learn("Fireball"));
        Assert.True(player.Learn("Bubble Shield"));
        Assert.True(player.Learn("Zygomatic Brain"));
    }

    [Test]
    public void Learn_ReturnsFalse_ForPreviouslyLearnedSkills()
    {
        // Arrange
        var player = new Player();
        player.Learn("Thunder Spikes");

        // Act/Assert
        Assert.False(player.Learn("Thunder Spikes"));
    }
}