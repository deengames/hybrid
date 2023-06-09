using Hybrid.Core.Skills.Actors;

namespace Hybrid.Core.Data.Skills;

public static class PlayerSkillsData
{
    public const int CarapaceToughnessPerLevel = 1;

    public static SkillData[] AllSkills = new SkillData[]
    {
        // Common
        new SkillData
        {
            Name = "Carapace",
            Species = "Arachanid",
            Effect = $"Increases defense by {CarapaceToughnessPerLevel} per level",
            Description = "Grow a tough exterior carapace like the giant spiders of Burkaan",
            LearningCost = 2,
        },
        new SkillData
        {
            Name = "Regeneration",
            Species = "Blattodea", // cockroach
            Effect = $"Regenerate {(int)(RegenerationSkill.HealthPerRound * 100)}% of your total health per round",
            Description = "With the genes of the  \"unkillable\" Red Spotted Cockroach, your body recovers in battle",
            LearningCost = 3,
        },
        new SkillData
        {
            Name = "Stinger",
            Species = "Vespidae", // wasp
            Effect = "Each attack inflicts venom",
            Description = "Tiny stringers from the Lunartata wasps cover your arms and hands",
            LearningCost = 3,
        },
        new SkillData
        {
            Name = "Blood Horn",
            Species = "Culicidae", // mosquito,
            Effect = "Adds an extra attack that heals you",
            Description = "Sprout a long, thin proboscis from your forehad",
            LearningCost = 1,
        },
        // Player-only
        new SkillData
        {
            Name = "Four Arms",
            Species = "Mantidae", // mantis
            Effect = "Adds two additional attacks per round",
            Description = "Grow two additional powerful mantis-like forearms",
            LearningCost = 2,
        },
        new SkillData
        {
            Name = "Slow Spores",
            Species = "Fungi",
            Effect = "Decreases a monster's speed every time it attacks you",
            Description = "Release a cloud of Sriped Mycota spores around you",
            LearningCost = 2,
        },
        new SkillData
        {
            Name = "Ommatidia Eyes",
            Species = "Vespidae",
            Effect = $"{(int)(OmmatidiaEyesSkill.CounterAttackProbability * 100)}% chance of counter-attacking when attacked",
            Description = "Adds several extra compound eyes to your head",
            LearningCost = 3,
        },
        new SkillData
        {
            Name = "Rage",
            Species = "Mantidae",
            Effect = $"Increases strength by {RageSkill.StrengthGainPerHurt} when hit",
            Description = "Your pores secrete hormones that drive you into a rage",
            LearningCost = 3,
        },
        new SkillData
        {
            Name = "Amotoxic Flesh",
            Species = "Fungi",
            Effect = "Inflict poison when attacked",
            Description = "Secrete amotoxins from your skin like the Death Cap mushrooms of Terra",
            LearningCost = 2,
        },
        new SkillData
        {
            Name = "Raptor Legs",
            Species = "Dromaeosaurid",
            Effect = $"Deals {RaptorLegsSkill.DamagePerLevel} damage, per level, to all monsters, each round of battle",
            Description = "Your legs bulge and extend with three long, sharp claws.",
            LearningCost = 2,
        },
        new SkillData
        {
            Name = "Honeycomb Cells",
            Species = "Bombini", // honey bees, mostly
            Effect = $"Deals {HoneycombCellsSkill.DamageOnCharge} damage to all monsters after you get hit {HoneycombCellsSkill.HitsToCharge} times",
            Description = "Your cells reshape themselves into honeycombs and absorb kinetic energy",
            LearningCost = 1,
        },
        new SkillData
        {
            Name = "Shadow Affinity",
            Species = "Grue",
            Effect = "Heals 50% of your health when you descend to the next floor",
            Description = "You blend into passing shadows, the darkness repairing your damaged organs",
            LearningCost = 2
        },
        new SkillData
        {
            Name = "Ink",
            Species = "Cephalopod", // squid
            Effect = "Each round, one enemy is inked, increasing the  chance of missing an attack by 20%",
            Description = "You develop ink sacs that can spray jet-blank ink at high pressures",
            LearningCost = 2
        }
        // Monster-only: none so far (that need to be here)
    };
    
    public static SkillData Get(string name) => AllSkills.Single(s => s.Name == name);

    public static IEnumerable<SkillData> GetUnlearnedSkills(string[] skills)
    {
        return PlayerSkillsData.AllSkills.ExceptBy(skills, s => s.Name).OrderBy(s => s.Species).ThenBy(s => s.Name);
    }
}

public struct SkillData
{
    public string Name { get; set; }
    public string Species { get; set; } // Family, class, species, whatever
    public string Effect { get; set; }
    public string Description { get; set; }
    public int LearningCost { get; set; }
}