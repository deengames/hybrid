using Hybrid.Core.Character;

namespace Hybrid.Core.Skills.Actors;

class BloodHornSkill : BaseSkill
{
    private const float DamageAndHealPercent = 0.60f;

    override public string AfterAttack(Actor attacker, Actor target, string[] skills)
    {
        var result = attacker.Attack(target, skills, DamageAndHealPercent);
        var damage = result.Item1;

        var pokeDamage = Math.Min(target.TotalHealth - target.Health, damage);
        target.Health -= pokeDamage;
        attacker.Heal(pokeDamage);

        var toReturn = $"[highlight]{attacker.Name}[/] stab(s) [dark]{target.Name}[/] with a probiscous, damaging and healing for [highlight]{pokeDamage}[/] health.";
        if (!string.IsNullOrWhiteSpace(result.Item2))
        {
            toReturn += $"\n{result.Item2}";
        }
        return toReturn;
    }
}
