using System.Text;
using Hybrid.Core.Character;
using Hybrid.Core.Monsters;

namespace Hybrid.Core.Skills.Player;

// Shared venom for Stinger and Amotoxic Flesh
public static class VenomManager
{
    private const int DamagePerVenom = 2;

    private static Dictionary<Actor, int> _venomAmount = new();

    public static void OnAttack(Actor attacker, Actor target, int venomAdded)
    {
        if (!_venomAmount.ContainsKey(target))
        {
            _venomAmount[target] = 0;
        }

        _venomAmount[target] += venomAdded;
    }

    public static string OnRoundEnd()
    {
        var message = new StringBuilder();
        foreach (var actor in _venomAmount.Keys)
        {
            if (actor.Health > 0)
            {
                var damage = DamagePerVenom * _venomAmount[actor];
                actor.Health = Math.Max(0, actor.Health - damage);
                message.Append($"Venom courses through [dark]{actor.Name}[/]! [highlight]{damage}[/] damage!");
                
                if (actor.Health <= 0)
                {
                    message.Append($" [dark]{actor.Name} succumbs to poison[/] and [highlight]DIES![/]");
                    if (actor is Monster)
                    {
                        var monster = actor as Monster;
                        message.Append($" You gain [highlight]{monster.Cost}[/] experience points!");
                    }
                }

                message.AppendLine("");

                _venomAmount[actor] -= 1;
                if (_venomAmount[actor] <= 0)
                {
                    _venomAmount.Remove(actor);
                }
            }
        }
        return message.ToString();
    }

    public static void OnBattleEnd()
    {
        _venomAmount.Clear();
    }
}