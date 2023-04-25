using Hybrid.Core.Dungeon;

namespace Hybrid.Core.Character.Skills;

class BloodHornSkill : BaseSkill
{
    private const float DamageAndHealPercent = 0.5f;
    private readonly Player _player;

    public BloodHornSkill(Player player)
    {
        _player = player;
    }

    override public string AfterAttack(Monster target)
    {
        var damage = (int)Math.Ceiling(_player.CalculateDamage(target) * DamageAndHealPercent);

        if (target.Health > 0)
        {
            var pokeDamage = Math.Min(target.TotalHealth - target.Health, damage);
            target.Health -= pokeDamage;
            _player.Heal(pokeDamage);

            return $"[highlight]You[/] stab the [dark]{target.Name}[/] with your probiscous, damaging it and healing yourself for [highlight]{pokeDamage}[/] health.\n";
        }

        return string.Empty;
    }
}
