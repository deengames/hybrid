using Hybrid.Core.Dungeon;

namespace Hybrid.Core.Character.Skills;

class FourArmsSkill : BaseSkill
{
    private const int NumberOfExtraArms = 2;

    public FourArmsSkill(Player player) : base(player)
    {
    }
    
    public override string AfterAttack(Monster target)
    {
        var damage = _player.CalculateDamage(target) * NumberOfExtraArms;
        return $"You hit the [dark]{target.Name}[/] with your extra arms for [highlight]{damage}[/] damage.";
        target.Health -= damage;
    }
}