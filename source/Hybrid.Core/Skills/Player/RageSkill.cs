using Hybrid.Core.Character;

namespace Hybrid.Core.Skills.Actors;

class RageSkill : BaseSkill
{
    internal const int StrengthGainPerHurt = 1;
    private readonly List<Actor> _ragers = new();

    public override string OnAttacked(Actor attacker, Actor target, string[] skills)
    {
        if (!_ragers.Contains(target))
        {
            _ragers.Add(target);
        }

        target.StrengthModifier += StrengthGainPerHurt;
        return $"{target.Name} [highlight]roars[/] in rage! Strength up by [highlight]{StrengthGainPerHurt}[/]!";
    }

    public override string OnBattleEnd()
    {
        var toReturn = "";

        foreach (var rager in _ragers)
        {
            rager.StrengthModifier = 0;
            toReturn += $"\n{rager.Name} returns to normal.";
        }

        return toReturn;
    }
}