namespace Hybrid.Core.Character.Skills;

class SlowSporesSkill : BaseSkill
{
    public SlowSporesSkill(Player player) : base(player)
    {

    }

    // WARNING: DESTRUCTIVE AGAINST PLAYER. Speed does not reset after battle!
    public override string AfterAttack(Actor attacker, Actor target, string[] skills)
    {
        if (target.Speed > 1)
        {
            target.Speed -= 1;
            return $"You release a cloud of spores that [dark]reduce {target.Name}'s speed[/] by [highlight]1[/].\n";
        }

        return string.Empty;
    }
}