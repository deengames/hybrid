using System.Text;

namespace Hybrid.Core.Character.Skills;

public class SkillManager
{
    public static SkillManager Instance { get; private set; }

    private Dictionary<string, BaseSkill> _skillImplementations = new Dictionary<string, BaseSkill>();

    private static Dictionary<string, Type> SkillToType = new()
    {
        { "Regeneration", typeof(RegenerationSkill) },
        { "Blood Horn", typeof(BloodHornSkill) },
        { "Four Arms", typeof(FourArmsSkill) },
        { "Slow Spores", typeof(SlowSporesSkill ) },
        { "Stinger", typeof(StingerSkill ) },
    };

    public SkillManager(Player player)
    {
        SkillManager.Instance = this;
        foreach (var skillName in SkillToType.Keys)
        {
            var skillType = SkillToType[skillName];
            var skill = Activator.CreateInstance(skillType, player) as BaseSkill;
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
    public string OnAttack(Actor target, string[] skills)
    {
        var message = new StringBuilder();
        foreach (var skillName in skills)
        {
            var skill = GetSkillImplementation(skillName);
            if (target.Health > 0)
            {
                message.Append(skill.OnAttack(target));
            }
        }

        return message.ToString();
    }

    private BaseSkill GetSkillImplementation(string skillName)
    {
        return _skillImplementations[skillName];
    }
}