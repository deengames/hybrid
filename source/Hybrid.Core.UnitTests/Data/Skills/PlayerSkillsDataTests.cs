using Hybrid.Core.Data.Skills;
using NUnit.Framework;

namespace Hybrid.Core.UnitTests.Data.Skills;

[TestFixture]
class PlayerSkillsDataTests
{
    [Test]
    public void AllData_HasAllFieldsSet()
    {
        // Arrange?
        var actualSkills = PlayerSkillsData.AllSkills;
        // Assert
        foreach (var skill in actualSkills)
        {
            Assert.That(skill.Name, Is.Not.Empty);
            Assert.That(skill.Species, Is.Not.Empty);
            Assert.That(skill.Effect, Is.Not.Empty);
            Assert.That(skill.Description, Is.Not.Empty);
            Assert.That(skill.LearningCost, Is.GreaterThan(0));
        }
    }

    [Test]
    public void GetUnlearnedSkills_ReturnsUnlearnedSkills()
    {
        // Arrange
        var learned = new string[] { PlayerSkillsData.AllSkills[0].Name };
        // Act
        var actual = PlayerSkillsData.GetUnlearnedSkills(learned);
        // Assert
        Assert.That(actual.Select(s => s.Name).All(s => !learned.Contains(s)));
    }
}