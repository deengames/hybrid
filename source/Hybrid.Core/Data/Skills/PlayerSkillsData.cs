namespace Hybrid.Core.Data.Skills;

public static class PlayerSkillsData
{
    public static SkillData[] AllSkills = new SkillData[] {
        new SkillData() {
            Name = "Carapace",
            Effect = "Increases defense by +5 per level",
            Description = "Grow a tough exterior carapace like the giant arachanids of Burkaan",
            LearningCost = 2
        }
    };
}

public struct SkillData
{
    public string Name { get; set; }
    public string Effect { get; set; }
    public string Description { get; set; }
    public int LearningCost { get; set; }
}