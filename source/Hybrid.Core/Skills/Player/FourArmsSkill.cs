using Hybrid.Core.Character;
using Hybrid.Core.Skills.Actors;

namespace Hybrid.Core.Skills.Player;

class FourArmsSkill : BaseSkill
{
    private const float DamagePerExtraArm = 0.3f;

    public override string AfterAttack(Actor attacker, Actor target, string[] skills)
    {
        var multiplier = 1f;
        if (attacker is Hybrid.Core.Character.Player)
        {
            // e.g. did 10 damage? Now you get en extra (10 * .6) = 6 damage.
            multiplier = DamagePerExtraArm * 2;
        }

        var result = attacker.Attack(target, skills, multiplier);
        var damage = result.Item1;
        return $"You hit the [dark]{target.Name}[/] with your extra arms for [highlight]{damage}[/] damage.\n{result.Item2}";
    }
}