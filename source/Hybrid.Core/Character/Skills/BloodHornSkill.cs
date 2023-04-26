using Hybrid.Core.Dungeon;

namespace Hybrid.Core.Character.Skills;

class BloodHornSkill : BaseSkill
{
    private const float DamageAndHealPercent = 0.5f;

    public BloodHornSkill(Player player) : base(player)
    {
    }

    override public string AfterAttack(Monster target)
    {
        var result = _player.Attack(target, DamageAndHealPercent);
        var damage = result.Item1;

        var pokeDamage = Math.Min(target.TotalHealth - target.Health, damage);
        target.Health -= pokeDamage;
        _player.Heal(pokeDamage);

        return $"[highlight]You[/] stab the [dark]{target.Name}[/] with your probiscous, damaging it and healing yourself for [highlight]{pokeDamage}[/] health.\n{result.Item2}\n";
    }
}
