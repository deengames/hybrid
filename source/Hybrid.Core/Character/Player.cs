using System.Text;
using Hybrid.Core.Data.Skills;
using Hybrid.Core.Monsters;
using Hybrid.Core.Skills;

namespace Hybrid.Core.Character;

public class Player : Actor
{
    public int SkillPoints { get; private set; } = 5;
    
    public int Level { get; private set; } = 1;

    public int Xp { get { return _xp; } }
    internal const int XpPerLevelUp = 20;

    private int _xp = 0;

    private const int HealthPerLevelUp = 5;

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
        if (this.Skills.Contains(skill.Name))
        {
            return false;
        }

        // this.Skills.Add(skill.Name);
        this.Skills = new List<string>(this.Skills) { skill.Name }.ToArray();
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
        message.Append(SkillManager.Instance.OnPreAttack(this, this.Skills));
        
        // ON-attack skills
        var result = this.MeleeAttack(weakest);
        var damage = result.Item1;
        message.AppendLine($"[highlight]You[/] attack the [dark]{weakest.Name}[/] for [highlight]{damage}[/] damage.");
        if (!string.IsNullOrWhiteSpace(result.Item2))
        {
            // Attack messages
            message.Append(result.Item2);
        }

        // POST-skills
        if (weakest.Health > 0)
        {
            message.Append(SkillManager.Instance.OnPostAttack(this, weakest, this.Skills));
        }

        if (weakest.Health <= 0)
        {
            message.AppendLine($" [highlight]{weakest.Name} DIES![/] You gain [highlight]{weakest.Cost}[/] experience points!");
        }

        return message.ToString();
    }

    // TODO: move into skill manager someday
    public int GetTotalDefense()
    {
        var caparace = PlayerSkillsData.Get("Carapace");
        var toReturn = this.GetToughness();
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

        this.TotalHealth += HealthPerLevelUp;
        this.Heal(HealthPerLevelUp);
        
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

        var result = this.Attack(monster, this.Skills);
        return result;
    }

    // Returns true if we LEVELLED UP~!
    public bool OnMonsterDied(Actor whoDied)
    {
        if (whoDied == null || whoDied == this)
        {
            return false;
        }

        var monster = whoDied as Monster;

        // It's complicated. Because descending levels you up, too.
        var previousXp = _xp;
        this._xp += monster.Cost;
        if (_xp / XpPerLevelUp > previousXp / XpPerLevelUp)
        {
            this.LevelUp();
            return true;
        }

        return false;
    }

    public int XpToLevelUp()
    {
        // It's simple, yaar; you level-up every 20 XP. Easy peasy.
        var currentLevel = Xp / XpPerLevelUp;
        var nextLevel = currentLevel + 1;
        var nextXpRequired = nextLevel * XpPerLevelUp;
        return nextXpRequired - _xp;
    }
}