using Hybrid.Core.Character;
using Hybrid.Core.Data.Skills;
using Hybrid.Game.Interfaces;
using Hybrid.Game.IO;
using Spectre.Console;

namespace Hybrid.Game.Character;

class SkillSelectScene : IScene
{
    public void Show()
    {
        var player = Game.Instance.Player;

        ShowIntro(player.SkillPoints);
        ShowCurrentSkills(player.Skills);
        ListUnlearnedSkills(player.Skills);
        System.Console.ReadKey(true);
        Game.Instance.End();
    }

    private void ListUnlearnedSkills(string[] skills)
    {
        var unlearnedSkills = PlayerSkillsData.AllSkills.ExceptBy(skills, s => s.Name);
        foreach (var unlearned in unlearnedSkills)
        {
            AnsiConsole.MarkupLine($"[{Colours.ThemeHighlight}]{unlearned.Name}[/] ({unlearned.LearningCost} points): {unlearned.Description}. [{Colours.ThemeHighlight}]{unlearned.Effect}[/].");
        }
    }

    private void ShowIntro(int skillPoints)
    {
        AnsiConsole.MarkupLine($"[{Colours.ThemeHighlight}]Choose your skills.[/] You have [{Colours.ThemeHighlight}]{skillPoints}[/] skill points.\n");
    }

    private void ShowCurrentSkills(string[] skills)
    {
        AnsiConsole.MarkupLine($"[{Colours.ThemeHighlight}]Current skills[/] ({skills.Length}): {string.Join(", ", skills)}");
    }
}