using Hybrid.Core.Character;

namespace Hybrid.Core.Skills.Actors;

class AmotoxicFleshSkill : BaseSkill
{
    private const int VenomPerHit = 2;
    
    public override string OnAttacked(Actor attacker, Actor target, string[] skills)
    {
        // Reverse because: venom applies to attacker
        VenomManager.OnAttack(target, attacker, VenomPerHit);
        var targetName = target.Name == "You" ? "Your" : target.Name;
        return $"{targetName} skin secretes [highlight]{VenomPerHit}[/] venom onto {attacker.Name}!\n";
    }
}