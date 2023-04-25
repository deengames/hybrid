using Hybrid.Core.Character.Skills;
using Hybrid.Core.Data.Skills;
using Hybrid.Core.Dungeon;

namespace Hybrid.Core.Character;

public class Player : Actor
{
    public int SkillPoints { get; private set; } = 5;
    public string[] Skills { get { return _skills.ToArray(); } }
    
    public int Level { get; private set; } = 1;

    private List<string> _skills = new List<string>();

    // TODO: array perhaps. But I don't like: Skills, _skills, and now _implementations?
    private List<ISkill> _skillImplementations = new List<ISkill>();


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
        if (skill.Name == "Regeneration")
        {
            _skillImplementations.Add(new RegenerationSkill(this));
        }

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

        var message = "";
        foreach (var skill in _skillImplementations)
        {
            message += skill.PreTurn();
        }
        
        var damage = this.MeleeAttack(weakest);

        message += $"[highlight]You[/] attack the [dark]{weakest.Name}[/] for [highlight]{damage}[/] damage.";
        if (weakest.Health <= 0)
        {
            message += $" [highlight]{weakest.Name} DIES![/]";
        }

        return message;
    }

    public int GetTotalDefense()
    {
        var caparace = PlayerSkillsData.Get("Carapace");
        var toReturn = this.Toughness;
        if (this.Skills.Contains(caparace.Name))
        {
            toReturn += (this.Level * PlayerSkillsData.CarapaceToughnessPerLevel);
        }
        return toReturn;
    }

    public override int MeleeAttack(Actor target)
    {
        var damage = Math.Max(this.Strength - target.Toughness, 0);
        if (damage > 0)
        {
            target.Health = Math.Max(target.Health - damage, 0);
        }
        return damage;
    }
}