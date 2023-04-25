namespace Hybrid.Core.Character.Skills;

interface ISkill
{
    /// <summary>
    /// Do something before the player starts his turn. Return a message if ya want.
    /// </summary>
    
    string PreTurn();
}