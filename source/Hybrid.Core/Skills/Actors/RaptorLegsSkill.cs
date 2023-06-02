using Hybrid.Core.Character;

namespace Hybrid.Core.Skills.Actors;

class RaptorLegsSkill : BaseSkill
{
    internal const int DamagePerLevel = 5;

    public override string OnRoundEnd(IEnumerable<Actor> actors)
    {
        var player = actors.Single(a => a.GetType() == typeof(Character.Player)) as Character.Player;
        var monsters = actors.Where(a => a != player);
        var damage = DamagePerLevel * player.Level;

        foreach (var monster in monsters)
        {
            monster.TakeDamage(damage);
        }
        return $"Your claw-like legs strike out for [highlight]{damage}[/] damage!\n";
    }
}