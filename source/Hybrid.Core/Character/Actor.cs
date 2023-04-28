namespace Hybrid.Core.Character;

public abstract class Actor
{
    public string Name { get; set; }
    public int Health { get; internal set; }
    public int TotalHealth { get; internal set; }
    public int Strength { get; internal set; }
    public int Toughness { get; internal set; }
    public int Speed { get; internal set; }

    /// <summary>
    /// Returns marked-up string with message, e.g. [highlight]you[/] need a [dark]vacation[/] ...
    /// </summary>
    public abstract string TakeTurn(List<Actor> actors);

    // Returns damage amount and message
    public abstract Tuple<int, string> MeleeAttack(Actor target);

    public void Heal(int recovery)
    {
        var amount = Math.Min(recovery, this.TotalHealth - this.Health);
        this.Health += amount;
    }
}