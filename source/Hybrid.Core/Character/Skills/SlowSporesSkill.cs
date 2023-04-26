using Hybrid.Core.Dungeon;

namespace Hybrid.Core.Character.Skills;

class SlowSporesSkill : BaseSkill
{
    public SlowSporesSkill(Player player) : base(player)
    {

    }

    public override string AfterAttack(Monster target)
    {
        if (target.Speed > 1)
        {
            target.Speed -= 1;
            return $"You release a cloud of spores that [dark]reduce {target.Name}'s speed[/] by [highlight]1[/].\n";
        }

        return string.Empty;
    }
}