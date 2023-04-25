using Hybrid.Core.Data.Skills;

namespace Hybrid.Core.Character.Skills;

class RegenerationSkill : BaseSkill
{
    internal const float HealthPerRound = 0.1f;
    private readonly Player _player;
    private int _regenAmount => (int)Math.Round(HealthPerRound * _player.TotalHealth);
    
    public RegenerationSkill(Player player)
    {
        _player = player;
    }

    override public string PreTurn()
    {
        if (_player.Health > 0)
        {
            _player.Health += _regenAmount;
            return $"You regenerate [highlight]{_regenAmount}[/] health!\n";
        }
        
        return "";
    }

}