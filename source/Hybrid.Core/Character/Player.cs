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
    private List<BaseSkill> _skillImplementations = new List<BaseSkill>();

    private static Dictionary<string, Type> SkillToImplementation = new()
    {
        { "Regeneration", typeof(RegenerationSkill) },
        { "Blood Horn", typeof(BloodHornSkill) },
        { "Four Arms", typeof(FourArmsSkill) },
        { "Slow Spores", typeof(SlowSporesSkill ) },
    };

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
        if (SkillToImplementation.ContainsKey(skill.Name))
        {
            var type = SkillToImplementation[skill.Name];
            var instance = Activator.CreateInstance(type, this) as BaseSkill;
            _skillImplementations.Add(instance);
        }

        this.SkillPoints -= skill.LearningCost;

        return true;
    }

    public override string TakeTurn(List<Actor> actors)
    {
        // Who do I kill first? IDK, ig the weakest.
        var weakest = actors.Except(new Actor[] { this }).OrderBy(a => a.Health).FirstOrDefault() as Monster;
        if (weakest == null)
        {
            // VICTORY!
            return "";
        }

        if (this.Health == 0)
        {
            return "";
        }

        // PRE-skills
        var message = "";
        foreach (var skill in _skillImplementations)
        {
            message += skill.PreTurn();
        }
        
        var damage = this.MeleeAttack(weakest);
        message += $"[highlight]You[/] attack the [dark]{weakest.Name}[/] for [highlight]{damage}[/] damage.\n";

        // POST-skills
        if (weakest.Health > 0)
        {
            foreach (var skill in _skillImplementations)
            {
                message += skill.AfterAttack(weakest);
            }
        }

        if (weakest.Health <= 0)
        {
            message += $" [highlight]{weakest.Name} DIES![/]\n";
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
        var monster = target as Monster;
        if (monster == null)
        {
            throw new ArgumentException(nameof(target));
        }

        return this.Attack(monster);
    }

    internal int Attack(Monster target, float multiplier = 1.0f)
    {
        var damage = CalculateDamage(target, multiplier);
        if (damage > 0)
        {
            target.Health = Math.Max(target.Health - damage, 0);
        }
        return damage;
    }

    private int CalculateDamage(Actor target, float multiplier)
    {
        // Changing strength here is uninuitive, because it changes damage in weird ways.
        var rawDamage = this.Strength - target.Toughness;
        var adjusted = (int)Math.Ceiling(rawDamage * multiplier);
        return Math.Max(adjusted, 0);
    }
}