namespace Hybrid.Core.Character.Skills;

class RegenerationSkill : BaseSkill
{
    internal const float HealthPerRound = 0.1f;
    private int _regenAmount => (int)Math.Round(HealthPerRound * _player.TotalHealth);
    
    public RegenerationSkill(Player player) : base(player)
    {
    }

    override public string PreTurn()
    {
        _player.Heal(_regenAmount);
        return $"You regenerate [highlight]{_regenAmount}[/] health!\n";
    }

}