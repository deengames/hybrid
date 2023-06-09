using Hybrid.Core.Skills;

namespace Hybrid.Core.Character;

public abstract class Actor
{
    public string Name { get; set; }
    public int Health { get; internal set; }
    public int TotalHealth { get; internal set; }
    public int Strength { get; internal set; }
    internal int Toughness { get; set; }

    // Temporary strength change, if you can RAGE.
    internal int StrengthModifier { get; set; }
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
        target.TakeDamage(damage);        
        var message = SkillManager.Instance.OnAttack(this, target, skills);
        message += SkillManager.Instance.OnAttacked(this, target, target.Skills);
        return new Tuple<int, string>(damage, message);
    }

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            this.Health = Math.Max(this.Health - damage, 0);
        }
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
        var rawDamage = (this.Strength + this.StrengthModifier) - target.GetToughness();
        var adjusted = (int)Math.Ceiling(rawDamage * multiplier);
        return Math.Max(adjusted, 0);
    }

    public override string ToString()
    {
        return $"{this.Name} {this.Health}/{this.TotalHealth}";
    }
}