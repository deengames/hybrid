namespace Hybrid.Core.Skills.Actors;

class ShadowAffinitySkill : BaseSkill
{
    internal const float HealPercentage = 0.5f;

    public override string OnDescend(Core.Character.Player player)
    {
        var healAmount = (int)Math.Ceiling(HealPercentage * player.TotalHealth);
        player.Heal(healAmount);
        return $"You cells rupture and reform in the shadows, healing [highlight]{healAmount}[/] health.\n";
    }
}