using Hybrid.Core.Data.Skills;
using Hybrid.Core.Dungeon;

namespace Hybrid.Core.Character;

public class Player : Actor
{
    public int SkillPoints { get; private set; } = 5;
    public string[] Skills { get { return _skills.ToArray(); } }
    
    private List<string> _skills = new List<string>();

    public Player()
    {
        this.Name = "You";
        this.TotalHealth = 50;
        this.Health = TotalHealth;
        this.Strength = 10;
        this.Toughness = 5;
        this.Speed = 5;
    }

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

    public override string TakeTurn(List<Actor> actors)
    {
        // Who do I kill first? IDK, ig the weakest.
        var weakest = actors.Except(new Actor[] { this }).OrderBy(a => a.Health).FirstOrDefault();
        if (weakest == null)
        {
            // VICTORY!
            return "";
        }
        
        var damage = this.MeleeAttack(weakest);

        var message = $"[highlight]You[/] attack the [dark]{weakest.Name}[/] for [highlight]{damage}[/] damage.";
        if (weakest.Health <= 0)
        {
            message += $" [highlight]{weakest.Name} DIES![/]";
        }

        return message;
    }
}