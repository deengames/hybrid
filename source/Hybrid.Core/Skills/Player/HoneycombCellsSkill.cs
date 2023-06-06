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
            if (_hitsSoFar >= HitsToCharge)
            {
                _hitsSoFar -= HitsToCharge;
                return $"Your cells explode, hitting all monsters for {DamageOnCharge} damage!";
            }
        }

        return "";
    }
}