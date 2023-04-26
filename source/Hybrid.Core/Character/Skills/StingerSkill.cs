using System.Text;
using Hybrid.Core.Dungeon;

namespace Hybrid.Core.Character.Skills;

class StingerSkill : BaseSkill
{
    private const int VenomPerStrike = 3;
    private const int DamagePerVenom = 2;
    
    private Dictionary<Monster, int> venomAmount = new();

    public StingerSkill(Player player) : base(player)
    {
    }

    public override string OnAttack(Monster target)
    {
        if (!venomAmount.ContainsKey(target))
        {
            venomAmount[target] = 0;
        }

        venomAmount[target] += VenomPerStrike;

        return $"The stingers on your arm [dark]poison {target.Name}[/] [highlight]{VenomPerStrike}[/] times!";
    }

    public override string OnRoundEnd()
    {
        var message = new StringBuilder();
        foreach (var monster in venomAmount.Keys)
        {
            if (monster.Health > 0)
            {
                var damage = DamagePerVenom * venomAmount[monster];
                monster.Health = Math.Max(0, monster.Health - damage);
                message.Append($"Venom courses through [dark]{monster.Name}[/]! [highlight]{damage}[/] damage!");
                
                if (monster.Health == 0)
                {
                    message.Append($" [highlight]{monster.Name} DIES![/]");
                }

                message.Append("\n");

                venomAmount[monster] -= 1;
                if (venomAmount[monster] == 0)
                {
                    venomAmount.Remove(monster);
                }
            }
        }
        return message.ToString();
    }
}