using System.Text;

namespace Hybrid.Core.Character.Skills;

class StingerSkill : BaseSkill
{
    private const int VenomPerStrike = 3;
    private const int DamagePerVenom = 2;
    
    private Dictionary<Actor, int> venomAmount = new();

    public override string OnAttack(Actor target)
    {
        if (!venomAmount.ContainsKey(target))
        {
            venomAmount[target] = 0;
        }

        venomAmount[target] += VenomPerStrike;

        return $"Stingers stab into and [dark]poison {target.Name}[/] [highlight]{VenomPerStrike}[/] times!\n";
    }

    public override string OnRoundEnd()
    {
        var message = new StringBuilder();
        foreach (var actor in venomAmount.Keys)
        {
            if (actor.Health > 0)
            {
                var damage = DamagePerVenom * venomAmount[actor];
                actor.Health = Math.Max(0, actor.Health - damage);
                message.Append($"Venom courses through [dark]{actor.Name}[/]! [highlight]{damage}[/] damage!");
                
                if (actor.Health <= 0)
                {
                    message.Append($" [highlight]{actor.Name} succumbs to poison and DIES![/]");
                }

                message.AppendLine("");

                venomAmount[actor] -= 1;
                if (venomAmount[actor] <= 0)
                {
                    venomAmount.Remove(actor);
                }
            }
        }
        return message.ToString();
    }
}