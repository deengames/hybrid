namespace Hybrid.Core.Data.Skills;

public static class PlayerSkillsData
{
    public static SkillData[] AllSkills = new SkillData[]
    {
        new SkillData()
        {
            Name = "Carapace",
            Species = "Arachanid",
            Effect = "Increases defense by +5 per level",
            Description = "Grow a tough exterior carapace like the giant arachanids of Burkaan",
            LearningCost = 2
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