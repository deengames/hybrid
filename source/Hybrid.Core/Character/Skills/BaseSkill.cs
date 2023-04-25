using Hybrid.Core.Dungeon;

namespace Hybrid.Core.Character.Skills;

abstract class BaseSkill
{
    protected readonly Player _player;

    public BaseSkill(Player player)
    {
        _player = player;
    }

    /// <summary>
    /// Do something before the player starts his turn. Return a message if ya want.
    /// </summary>
    public virtual string PreTurn()
    {
        return string.Empty;
    }

    /// <summary>
    /// Do stuff after the player's melee attack. Only called if target is still alive.
    /// </summary>
    public virtual string AfterAttack(Monster target)
    {
        return string.Empty;
    }
}