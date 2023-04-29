using Hybrid.Core.Character;

namespace Hybrid.Core.Skills.Actors;

class RegenerationSkill : BaseSkill
{
    internal const float HealthPerRound = 0.1f;
    
    override public string PreTurn(Actor self)
    {
        var regenAmount = GetRegenAmount(self);
        self.Heal(regenAmount);
        return $"{self.Name} regenerate(s) [highlight]{regenAmount}[/] health!\n";
    }

    private int GetRegenAmount(Actor self)
    {
        return (int)Math.Round(HealthPerRound * self.TotalHealth);
    }
}