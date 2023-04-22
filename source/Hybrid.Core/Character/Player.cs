using Hybrid.Core.Data.Skills;

namespace Hybrid.Core.Character;

public class Player
{
    public int SkillPoints { get; private set; } = 5;
    public string[] Skills { get { return _skills.ToArray(); } }

    private List<string> _skills = new List<string>();

    /// <summary>
    /// Learns a skill, and returns true if newly-learned (false if previously learned)
    /// </summary>
    public bool Learn(SkillData skill)
    {
        if (_skills.Contains(skill.Name))
        {
            return false;
        }

        _skills.Add(skill.Name);
        this.SkillPoints -= skill.LearningCost;
        return true;
    }
}