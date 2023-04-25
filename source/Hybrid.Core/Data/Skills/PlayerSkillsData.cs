using Hybrid.Core.Character.Skills;

namespace Hybrid.Core.Data.Skills;

public static class PlayerSkillsData
{
    public const int CarapaceToughnessPerLevel = 5;

    public static SkillData[] AllSkills = new SkillData[]
    {
        new SkillData
        {
            Name = "Carapace",
            Species = "Arachanid",
            Effect = $"Increases defense by {CarapaceToughnessPerLevel} per level",
            Description = "Grow a tough exterior carapace like the giant arachanids of Burkaan",
            LearningCost = 2,
        },
        new SkillData
        {
            Name = "Four Arms",
            Species = "Mantidae", // Mantis
            Effect = "Adds two additional attacks per round",
            Description = "Grow two additional powerful mantis-like forearms",
            LearningCost = 1,
        },
        new SkillData
        {
            Name = "Regeneration",
            Species = "Blattodea", // Cockroach
            Effect = $"Regenerate {(int)(RegenerationSkill.HealthPerRound * 100)}% of your total health per round",
            Description = "With the genes of the  \"unkillable\" Red Spotted Cockroach, your body recovers in battle",
            LearningCost = 3,
        },
        new SkillData
        {
            Name = "Stinger",
            Species = "Vespidae", // wasp
            Effect = "Each attack inflicts venom. Amount increases per level",
            Description = "Tiny stringers from the Lunartata wasps cover your arms and hands",
            LearningCost = 2,
        },
        new SkillData
        {
            Name = "Blood Horn",
            Species = "Culicidae", // mosquito,
            Effect = "Adds an extra attack that heals you",
            Description = "Sprout a long, thin proboscis from your forehad",
            LearningCost = 2,
        },
        new SkillData
        {
            Name = "Slow Spores",
            Species = "Fungi",
            Effect = "Decreases a monster's speed every time it attacks you",
            Description = "Release a cloud of Sriped Mycota spores around you",
            LearningCost = 1,
        }
    };
    
    public static SkillData Get(string name) => AllSkills.Single(s => s.Name == name);

    public static IEnumerable<SkillData> GetUnlearnedSkills(string[] skills)
    {
        return PlayerSkillsData.AllSkills.ExceptBy(skills, s => s.Name).OrderBy(s => s.Species).ThenBy(s => s.Name);
    }
}

public struct SkillData
{
    public string Name { get; set; }
    public string Species { get; set; } // Family, class, species, whatever
    public string Effect { get; set; }
    public string Description { get; set; }
    public int LearningCost { get; set; }
}