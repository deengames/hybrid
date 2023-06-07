using Hybrid.Core.Character;

namespace Hybrid.Core.Skills.Actors;

class HoneycombCellsSkill : BaseSkill
{
    internal const int DamageOnCharge = 50;
    internal const int HitsToCharge = 5;
    
    private int _hitsSoFar = 0;

    public override string OnAttacked(Actor attacker, Actor target, string[] skills)
    {
        if (target is Hybrid.Core.Character.Player)
        {
            _hitsSoFar++;
        }

        return "";
    }

    public override string OnRoundEnd(IEnumerable<Actor> actors)
    {
        if (_hitsSoFar < HitsToCharge)
        {
            return string.Empty;
        }

        _hitsSoFar -= HitsToCharge;

        var player = actors.Single(a => a.GetType() == typeof(Character.Player)) as Character.Player;
        var monsters = actors.Where(a => a != player);

        foreach (var monster in monsters)
        {
            monster.TakeDamage(DamageOnCharge);
        }

        return $"Your cells explode, hitting all monsters for {DamageOnCharge} damage!\n";
    }
}