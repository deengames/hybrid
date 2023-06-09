using Hybrid.Core.Character;
using Hybrid.Core.Skills.Actors;

namespace Hybrid.Core.Skills.Monsters;

class BurnSkill : BaseSkill
{
    private const int BurnsPerHit = 1;
    private const int ToughnessPerBurn = 1;
    
    private readonly Dictionary<Actor, int> _burns = new();

    public override string OnAttack(Actor attacker, Actor target)
    {
        if (!_burns.ContainsKey(target))
        {
            _burns[target] = 0;
        }

        _burns[target] += BurnsPerHit;
        target.ToughnessModifier -= ToughnessPerBurn;
        return $"{attacker.Name} [dark]burns {target.Name}[/]! Toughness down by [highlight]{ToughnessPerBurn}[/]!\n";
    }
}