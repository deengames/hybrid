using Hybrid.Core.Character;
using Hybrid.Core.Data.Skills;
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
        Assert.True(player.Learn(PlayerSkillsData.AllSkills.ElementAt(0)));
        Assert.True(player.Learn(PlayerSkillsData.AllSkills.ElementAt(1)));
        Assert.True(player.Learn(PlayerSkillsData.AllSkills.ElementAt(2)));
    }

    [Test]
    public void Learn_ReturnsFalse_ForPreviouslyLearnedSkills()
    {
        // Arrange
        var skill = PlayerSkillsData.AllSkills.ElementAt(3);
        var player = new Player();
        player.Learn(skill);

        // Act/Assert
        Assert.False(player.Learn(skill));
    }
}