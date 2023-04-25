using Hybrid.Core.Dungeon;

namespace Hybrid.Core.Character.Skills;

class FourArmsSkill : BaseSkill
{
    private const int NumberOfExtraArms = 2;
    private readonly Player _player;

    public FourArmsSkill(Player player)
    {
        _player = player;
    }
    
    public override string AfterAttack(Monster target)
    {
        if (target.Health > 0)
        {
            var damage = _player.CalculateDamage(target) * NumberOfExtraArms;
            return $"You hit the [dark]{target.Name}[/] with your extra arms for [highlight]{damage}[/] damage.";
            target.Health -= damage;
        }

        return string.Empty;
    }
}