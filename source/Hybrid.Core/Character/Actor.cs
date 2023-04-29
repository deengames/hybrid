using Hybrid.Core.Skills;

namespace Hybrid.Core.Character;

public abstract class Actor
{
    public string Name { get; set; }
    public int Health { get; internal set; }
    public int TotalHealth { get; internal set; }
    public int Strength { get; internal set; }
    internal int Toughness { get; set; }

    // Temporary toughness change based on burns (negative), boosts (positive).
    public int ToughnessModifier { get; set; }
    public int Speed { get; internal set; }
    public string[] Skills { get; set; } = new string[0];

    /// <summary>
    /// Returns marked-up string with message, e.g. [highlight]you[/] need a [dark]vacation[/] ...
    /// </summary>
    public abstract string TakeTurn(List<Actor> actors);

    // Returns damage amount and message
    public abstract Tuple<int, string> MeleeAttack(Actor target);

    public Tuple<int, string> Attack(Actor target, string[] skills, float multiplier = 1.0f)
    {
        var damage = CalculateDamage(target, multiplier);
        if (damage > 0)
        {
            target.Health = Math.Max(target.Health - damage, 0);
        }
        
        var message = SkillManager.Instance.OnAttack(this, target, skills);
        return new Tuple<int, string>(damage, message);
    }

    public void Heal(int recovery)
    {
        var amount = Math.Min(recovery, this.TotalHealth - this.Health);
        this.Health += amount;
    }

    public int GetToughness()
    {
        return this.Toughness + this.ToughnessModifier;
    }

    internal virtual int CalculateDamage(Actor target, float multiplier)
    {
        // Changing strength here is uninuitive, because it changes damage in weird ways.
        var rawDamage = this.Strength - target.GetToughness();
        var adjusted = (int)Math.Ceiling(rawDamage * multiplier);
        return Math.Max(adjusted, 0);
    }
}