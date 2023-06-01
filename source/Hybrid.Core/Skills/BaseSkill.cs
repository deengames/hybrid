using Hybrid.Core.Character;

namespace Hybrid.Core;

abstract class BaseSkill
{
    // Do something before an actor starts his turn. Return a message if ya want.
    public virtual string PreTurn(Actor self)
    {
        return string.Empty;
    }

    // Do stuff after an actor's melee attack. Only called if target is still alive.
    public virtual string AfterAttack(Actor attacker, Actor target, string[] skills)
    {
        return string.Empty;
    }

    // Called on each and every single attack
    public virtual string OnAttack(Actor attacker, Actor target)
    {
        return string.Empty;
    }

    // Reverse of OnAttack: called when you're under attack.
    public virtual string OnAttacked(Actor attacker, Actor target, string[] skills)
    {
        return string.Empty;
    }

    // Called after a round of combat. Used for, like, regen.
    public virtual string OnRoundEnd()
    {
        return string.Empty;
    }

    // Called when the battle ends. Victory only.
    public virtual string OnBattleEnd()
    {
        return string.Empty;
    }
}