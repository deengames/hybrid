using System.Text;
using Hybrid.Core.Dungeon;

namespace Hybrid.Core.Character.Skills;

public class SkillManager
{
    public static SkillManager Instance { get; } = new SkillManager();

    private List<BaseSkill> _skillImplementations = new List<BaseSkill>();

    private static Dictionary<string, Type> SkillToImplementation = new()
    {
        { "Regeneration", typeof(RegenerationSkill) },
        { "Blood Horn", typeof(BloodHornSkill) },
        { "Four Arms", typeof(FourArmsSkill) },
        { "Slow Spores", typeof(SlowSporesSkill ) },
        { "Stinger", typeof(StingerSkill ) },
    };

    public void OnPlayerLearn(Player player, string skillName)
    {
        if (SkillToImplementation.ContainsKey(skillName))
        {
            var type = SkillToImplementation[skillName];
            var instance = Activator.CreateInstance(type, player) as BaseSkill;
            _skillImplementations.Add(instance);
        }
    }

    public string OnPlayerPreAttack()
    {
        var message = new StringBuilder();
        foreach (var skill in _skillImplementations)
        {
            message.Append(skill.PreTurn());
        }
        return message.ToString();
    }

    public string OnPlayerPostAttack(Monster target)
    {
        var message = new StringBuilder();
        foreach (var skill in _skillImplementations)
        {
            message.Append(skill.AfterAttack(target));
        }
        return message.ToString();
    }

    // Applied once at the end of a round
    public string OnRoundEnd()
    {
        var message = new StringBuilder();
        foreach (var skill in _skillImplementations)
        {
            message.AppendLine(skill.OnRoundEnd());
        }
        return message.ToString();
    }

    // Apply skills that take effect for EVERY ATTACK
    public string OnPlayerAttack(Monster target)
    {
        var message = new StringBuilder();
        
        foreach (var skill in _skillImplementations)
        {
            if (target.Health > 0)
            {
                message.Append(skill.OnAttack(target));
            }
        }

        return message.ToString();
    }
}