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

    public override string TakeTurn(List<Actor> actors)
    {
        var player = actors.Single(a => a is Player);
        var damage = this.MeleeAttack(player);
        
        var message = $"The [dark]{this.Name}[/] attacks [highlight]you[/] for [highlight]{damage}[/] damage.";
        if (player.Health <= 0)
        {
            message += " [dark]You die ... [/]";
        }
        return message;
    }

    public override string ToString()
    {
        return this.Name;
    }
}