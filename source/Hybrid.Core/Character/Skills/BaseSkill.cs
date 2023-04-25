using Hybrid.Core.Dungeon;

namespace Hybrid.Core.Character.Skills;

abstract class BaseSkill
{
    /// <summary>
    /// Do something before the player starts his turn. Return a message if ya want.
    /// </summary>

    public virtual string PreTurn()
    {
        return string.Empty;
    }

    public virtual string AfterAttack(Monster target)
    {
        return string.Empty;
    }
}