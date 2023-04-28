namespace Hybrid.Core.Character.Skills;

abstract class BaseSkill
{
    protected readonly Player _player;

    public BaseSkill(Player player)
    {
        _player = player;
    }

    /// <summary>
    /// Do something before an actor starts his turn. Return a message if ya want.
    /// </summary>
    public virtual string PreTurn(Actor self)
    {
        return string.Empty;
    }

    /// <summary>
    /// Do stuff after an actor's melee attack. Only called if target is still alive.
    /// </summary>
    public virtual string AfterAttack(Actor attacker, Actor target, string[] skills)
    {
        return string.Empty;
    }

    // Called on each and every single attack
    public virtual string OnAttack(Actor target)
    {
        return string.Empty;
    }

    public virtual string OnRoundEnd()
    {
        return string.Empty;
    }
}