using Hybrid.Core.Character;

namespace Hybrid.Core.Dungeon;

public class Monster : Actor
{
    public int Cost { get; set; }

    // Shallow clone.
    public Monster Clone()
    {
        return this.MemberwiseClone() as Monster;
    }

    override public string TakeTurn(List<Actor> actors)
    {
        var player = actors.Single(a => a is Player) as Player;
        if (player.Health <= 0)
        {
            return "";
        }

        var damage = this.MeleeAttack(player).Item1;
        
        var message = $"The [dark]{this.Name}[/] attacks [highlight]you[/] for [highlight]{damage}[/] damage.";
        if (player.Health <= 0)
        {
            message += " [dark]You die ... [/]";
        }
        return message;
    }

    override public Tuple<int, string> MeleeAttack(Actor target)
    {
        var player = target as Player;
        var damage = Math.Max(this.Strength - player.GetTotalDefense(), 0);
        if (damage > 0)
        {
            player.Health = Math.Max(player.Health - damage, 0);
        }
        return new Tuple<int, string>(damage, string.Empty);
    }

    public override string ToString()
    {
        return this.Name;
    }
}