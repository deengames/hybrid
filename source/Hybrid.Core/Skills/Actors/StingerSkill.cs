using Hybrid.Core.Character;

namespace Hybrid.Core.Skills.Actors;

class StingerSkill : BaseSkill
{
    private const int VenomPerStrike = 3;
    
    public override string OnAttack(Actor attacker, Actor target)
    {
        VenomManager.OnAttack(attacker, target, VenomPerStrike);
        return $"{attacker.Name}'s stingers stab into and [dark]poison {target.Name}[/] [highlight]{VenomPerStrike}[/] times!\n";
    }
}