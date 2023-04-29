namespace Hybrid.Core.Character.Skills;

class FourArmsSkill : BaseSkill
{
    private const int NumberOfExtraArms = 2;

    public override string AfterAttack(Actor attacker, Actor target, string[] skills)
    {
        var numAttacks = attacker is Player ? NumberOfExtraArms : 1;
        var result = attacker.Attack(target, skills, numAttacks);
        var damage = result.Item1;
        return $"You hit the [dark]{target.Name}[/] with your extra arms for [highlight]{damage}[/] damage.\n{result.Item2}";
    }
}