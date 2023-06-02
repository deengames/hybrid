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
        // Common
        { "Regeneration", typeof(RegenerationSkill) },
        { "Blood Horn", typeof(BloodHornSkill) },
        { "Stinger", typeof(StingerSkill) },
        { "Ommatidia Eyes", typeof(OmmatidiaEyesSkill) },
        // Player-only
        { "Four Arms", typeof(FourArmsSkill) },
        { "Slow Spores", typeof(SlowSporesSkill) },
        { "Rage", typeof(RageSkill) },
        { "Amotoxic Flesh", typeof(AmotoxicFleshSkill )},
        { "Raptor Legs", typeof(RaptorLegsSkill )},
        // Monster-only
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
            // Null skills are things like pierce, which are hard-coded in monster.attack
            if (skill != null)
            {
                message.Append(skill.PreTurn(self));
            }
        }
        return message.ToString();
    }

    public string OnPostAttack(Actor attacker, Actor target, string[] skills)
    {
        var message = new StringBuilder();
        foreach (var skillName in skills)
        {
            var skill = GetSkillImplementation(skillName);
            if (skill != null)
            {
                message.Append(skill.AfterAttack(attacker, target, skills));
            }
        }
        return message.ToString();
    }

    // Applied once at the end of a round
    public string OnRoundEnd(string[] skills, IEnumerable<Actor> actors)
    {
        var message = new StringBuilder();
        foreach (var skillName in skills)
        {
            var skill = GetSkillImplementation(skillName);
            if (skill != null)
            {
                message.Append(skill.OnRoundEnd(actors));
            }
        }
        return message.ToString();
    }

    // Applied once at the end of battle - victory only.
    public string OnBattleEnd(string[] skills)
    {
        var message = new StringBuilder();
        foreach (var skillName in skills)
        {
            var skill = GetSkillImplementation(skillName);
            if (skill != null)
            {
                message.Append(skill.OnBattleEnd()); // No Actor context needed yet
            }
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
            if (skill != null && target.Health > 0)
            {
                message.Append(skill.OnAttack(attacker, target));
            }
        }

        return message.ToString();
    }

    // Apply skills that take effect for EVERY TIME YOU ARE ATTACKED
    public string OnAttacked(Actor attacker, Actor target, string[] skills)
    {
        var message = new StringBuilder();
        foreach (var skillName in skills)
        {
            var skill = GetSkillImplementation(skillName);
            if (skill != null && attacker.Health > 0 && target.Health > 0)
            {
                message.Append(skill.OnAttacked(attacker, target, skills));
            }
        }

        return message.ToString();
    }

    private BaseSkill GetSkillImplementation(string skillName)
    {
        if (_skillImplementations.ContainsKey(skillName))
        {
            return _skillImplementations[skillName];
        }

        // Skills like pierce have no implementation; they're hard-coded instead.
        // See: Monster.Attack
        return null;
    }
}