using Hybrid.Core.Character;

namespace Hybrid.Core.Skills.Actors;

class OmmatidiaEyesSkill : BaseSkill
{
    internal static readonly float CounterAttackProbability = 0.5f;
    private static Random _random = new Random();
    
    public override string OnAttacked(Actor attacker, Actor target, string[] skills)
    {
        if (_random.NextDouble() >= CounterAttackProbability)
        {
            return "";
        }

        var result = attacker.Attack(target, skills);
        var damage = result.Item1;
        return $"[highlight]{attacker.Name}[/] counter-attacks [dark]{target.Name}[/] for [highlight]{damage}[/] damage.\n{result.Item2}";
    }
}
