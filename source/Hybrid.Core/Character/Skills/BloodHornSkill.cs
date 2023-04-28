using Hybrid.Core.Dungeon;

namespace Hybrid.Core.Character.Skills;

class BloodHornSkill : BaseSkill
{
    private const float DamageAndHealPercent = 0.5f;

    public BloodHornSkill(Player player) : base(player)
    {
    }

    override public string AfterAttack(Actor attacker, Actor target, string[] skills)
    {
        var result = attacker.Attack(target, skills, DamageAndHealPercent);
        var damage = result.Item1;

        var pokeDamage = Math.Min(target.TotalHealth - target.Health, damage);
        target.Health -= pokeDamage;
        attacker.Heal(pokeDamage);

        return $"[highlight]{attacker.Name}[/] stab(s) [dark]{target.Name}[/] with a probiscous, damaging and healing for [highlight]{pokeDamage}[/] health.\n{result.Item2}\n";
    }
}
