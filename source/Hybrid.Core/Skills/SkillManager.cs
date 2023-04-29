using System.Text;
using Hybrid.Core.Character;
using Hybrid.Core.Skills.Actors;
using Hybrid.Core.Skills.Monsters;
using Hybrid.Core.Skills.Player;

namespace Hybrid.Core.Skills;

public class SkillManager
{
    public static SkillManager Instance { get; private set; } = new SkillManager();

    private Dictionary<string, BaseSkill> _skillImplementations = new Dictionary<string, BaseSkill>();

    private Dictionary<string, Type> SkillToType = new()
    {
        { "Regeneration", typeof(RegenerationSkill) },
        { "Blood Horn", typeof(BloodHornSkill) },
        { "Four Arms", typeof(FourArmsSkill) },
        { "Slow Spores", typeof(SlowSporesSkill) },
        { "Stinger", typeof(StingerSkill) },
        { "Burn", typeof(BurnSkill) },
    };

    private SkillManager()
    {
        foreach (var skillName in SkillToType.Keys)
        {
            var skillType = SkillToType[skillName];
            var skill = Activator.CreateInstance(skillType) as BaseSkill;
            _skillImplementations[skillName] = skill;
        }
    }

    public string OnPreAttack(Actor self, string[] skills)
    {
        var message = new StringBuilder();
        foreach (var skillName in skills)
        {
            var skill = GetSkillImplementation(skillName);
            message.Append(skill.PreTurn(self));
        }
        return message.ToString();
    }

    public string OnPostAttack(Actor attacker, Actor target, string[] skills)
    {
        var message = new StringBuilder();
        foreach (var skillName in skills)
        {
            var skill = GetSkillImplementation(skillName);
            message.Append(skill.AfterAttack(attacker, target, skills));
        }
        return message.ToString();
    }

    // Applied once at the end of a round
    public string OnRoundEnd(string[] skills)
    {
        var message = new StringBuilder();
        foreach (var skillName in skills)
        {
            var skill = GetSkillImplementation(skillName);
            message.Append(skill.OnRoundEnd()); // No Actor context needed yet
        }
        return message.ToString();
    }

    // Apply skills that take effect for EVERY ATTACK
    public string OnAttack(Actor attacker, Actor target, string[] skills)
    {
        var message = new StringBuilder();
        foreach (var skillName in skills)
        {
            var skill = GetSkillImplementation(skillName);
            if (target.Health > 0)
            {
                message.Append(skill.OnAttack(attacker, target));
            }
        }

        return message.ToString();
    }

    private BaseSkill GetSkillImplementation(string skillName)
    {
        return _skillImplementations[skillName];
    }
}