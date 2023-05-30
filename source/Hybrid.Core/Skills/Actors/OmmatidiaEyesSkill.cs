using Hybrid.Core.Character;

namespace Hybrid.Core.Skills.Actors;

class OmmatidiaEyesSkill : BaseSkill
{
    public static float CounterAttackProbability = 0.5f;

    public override string OnAttacked(Actor attacker, Actor target)
    {
        return $"{target.Name}: WE'RE BEING ATTACKED!";
    }
}
