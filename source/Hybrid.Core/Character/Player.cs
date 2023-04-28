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
        SkillManager.Instance.OnPlayerLearn(this, skill.Name);
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
        var message = new StringBuilder();
        message.Append(SkillManager.Instance.OnPlayerPreAttack());

        var result = this.MeleeAttack(weakest);
        var damage = result.Item1;
        message.AppendLine($"[highlight]You[/] attack the [dark]{weakest.Name}[/] for [highlight]{damage}[/] damage.");
        if (!string.IsNullOrWhiteSpace(result.Item2))
        {
            // Attack messages
            message.AppendLine(result.Item2);;
        }

        // POST-skills
        if (weakest.Health > 0)
        {
            message.Append(SkillManager.Instance.OnPlayerPostAttack(weakest));
        }

        if (weakest.Health <= 0)
        {
            message.AppendLine($" [highlight]{weakest.Name} DIES![/]");
        }

        return message.ToString();
    }

    // TODO: move into skill manager someday
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
        
        this.Strength++;
        this.Toughness++;
        this.Speed++;

        this.TotalHealth += 10;
        this.Heal(10);
        
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

    internal Tuple<int, string> Attack(Monster monster, float multiplier = 1.0f)
    {
        var damage = CalculateDamage(monster, multiplier);
        if (damage > 0)
        {
            monster.Health = Math.Max(monster.Health - damage, 0);
        }
        
        var message = SkillManager.Instance.OnPlayerAttack(monster);
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