using Hybrid.Core.Data.Skills;

namespace Hybrid.Core.Character;

class RegenerationSkill
{
    private readonly Player _player;
    
    public RegenerationSkill(Player player)
    {
        _player = player;
    }

    public void OnTakeTurn()
    {
        if (_player.Health > 0)
        {
            _player.Health += this.RegenAmount;
        }
    }

    public int RegenAmount => (int)Math.Round(PlayerSkillsData.RegenTotalHealthPerRound * _player.TotalHealth);
}