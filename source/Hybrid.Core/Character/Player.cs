using System.Text;
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
        { "Stinger", typeof(StingerSkill ) },
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
        
        var result = this.MeleeAttack(weakest);
        var damage = result.Item1;
        message += $"[highlight]You[/] attack the [dark]{weakest.Name}[/] for [highlight]{damage}[/] damage.\n";
        if (!string.IsNullOrWhiteSpace(result.Item2))
        {
            message += result.Item2 + "\n";
        }

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

    public void LevelUp()
    {
        this.Level++;
        this.SkillPoints++;
    }

    // Returns: <damage, message>
    public override Tuple<int, string> MeleeAttack(Actor target)
    {
        var monster = target as Monster;
        if (monster == null)
        {
            throw new ArgumentException(nameof(target));
        }

        var result = this.Attack(monster);
        return result;
    }

    ////////// TODO: MOVE INTO SKILL MANAGER (SINGLETON)
    public string OnRoundEnd()
    {
        var message = new StringBuilder();
        foreach (var skill in _skillImplementations)
        {
            message.AppendLine(skill.OnRoundEnd());
        }
        return message.ToString();
    }

    internal Tuple<int, string> Attack(Monster monster, float multiplier = 1.0f)
    {
        var damage = CalculateDamage(monster, multiplier);
        if (damage > 0)
        {
            monster.Health = Math.Max(monster.Health - damage, 0);
        }
        
        // Apply skills that take effect for EVERY ATTACK
        var message = "";
        foreach (var skill in _skillImplementations)
        {
            if (monster.Health > 0)
            {
                message += skill.OnAttack(monster);
            }
        }

        return new Tuple<int, string>(damage, message);
    }

    private int CalculateDamage(Actor target, float multiplier)
    {
        // Changing strength here is uninuitive, because it changes damage in weird ways.
        var rawDamage = this.Strength - target.Toughness;
        var adjusted = (int)Math.Ceiling(rawDamage * multiplier);
        return Math.Max(adjusted, 0);
    }
}