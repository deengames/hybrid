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
    public abstract int MeleeAttack(Actor target);
}