using System.Text;
using Hybrid.Core.Character;
using Hybrid.Core.Skills;

namespace Hybrid.Core.Monsters;

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

        // PRE-skills
        var message = new StringBuilder();
        message.Append(SkillManager.Instance.OnPreAttack(this, this.Skills));
        
        // ON-attack skills
        var result = this.MeleeAttack(player);
        var damage = result.Item1;        
        message.Append($"The [dark]{this.Name}[/] attacks [highlight]you[/] for [highlight]{damage}[/] damage. ");
        if (!string.IsNullOrWhiteSpace(result.Item2))
        {
            message.Append(result.Item2);
        }
        
        // POST-skills
        if (player.Health > 0)
        {
            message.Append(SkillManager.Instance.OnPostAttack(this, player, this.Skills));
        }

        if (player.Health <= 0)
        {
            message.Append(" [dark]You die ... [/]");
        }
        return message.ToString();
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