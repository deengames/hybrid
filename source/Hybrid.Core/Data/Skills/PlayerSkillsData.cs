namespace Hybrid.Core.Data.Skills;

public static class PlayerSkillsData
{
    public static SkillData[] AllSkills = new SkillData[]
    {
        new SkillData
        {
            Name = "Carapace",
            Species = "Arachanid",
            Effect = "Increases defense by +5 per level",
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
            Effect = "Regenerate 3% of your total health per round",
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
            Effect = "Absorb health each time you kill a monster",
            Description = "Sprouts a proboscis from your head which you can use to feast on the dead",
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