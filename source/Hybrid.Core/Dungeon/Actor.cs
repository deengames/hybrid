namespace Hybrid.Core.Dungeon;

public abstract class Actor
{
    public string Name { get; set; }
    public int Health { get; internal set; }
    public int TotalHealth { get; internal set; }
    public int Strength { get; internal set; }
    public int Toughness { get; internal set; }
    public int Speed { get; internal set; }

    // Returns marked-up string with message, e.g. [highlight]you[/] need a [dark]vacation[/] ...
    public abstract string TakeTurn(List<Actor> actors);

    // Returns damage amount
    public int MeleeAttack(Actor target)
    {
        var damage = Math.Max(this.Strength - target.Toughness, 0);
        if (damage > 0)
        {
            target.Health = Math.Max(target.Health - damage, 0);
        }
        return damage;
    }
}