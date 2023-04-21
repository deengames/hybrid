using Hybrid.Core.Data.Skills;
using Hybrid.Game.Interfaces;
using Hybrid.Game.IO;
using Spectre.Console;

namespace Hybrid.Game.Character;

class SkillSelectScene : IScene
{
    // Order matters
    private readonly IEnumerable<SkillData> unlearnedSkills;

    public SkillSelectScene(string[] learnedSkills)
    {
        unlearnedSkills = PlayerSkillsData.GetUnlearnedSkills(learnedSkills);
    }

    public void Show()
    {
        var player = Game.Instance.Player;

        ShowIntro(player.SkillPoints);
        ShowCurrentSkills(player.Skills);
        ListUnlearnedSkills();

        var toLearn = AskWhichSkillToLearn();
        player.Learn(toLearn);
        AnsiConsole.MarkupLine($"Your genes reconfigure themselves. [{Colours.ThemeHighlight}]You learn {toLearn.Name}.[/]");
        
        System.Console.ReadKey(true);
        Game.Instance.End();
    }

    private SkillData AskWhichSkillToLearn()
    {
        var input = "";
        int selection = 0;
        var allData = PlayerSkillsData.AllSkills;

        while (selection <= 0 || selection > unlearnedSkills.Count())
        {
            AnsiConsole.Write("Enter the number of the skill to learn: ");
            input = Console.ReadLine();
            int.TryParse(input, System.Globalization.NumberStyles.Integer, null, out selection);

            if (selection > 0 && selection <= unlearnedSkills.Count()) {
                var selected = allData.Single(s => s.Name == unlearnedSkills.ElementAt(selection - 1).Name);
                if (selected.LearningCost > Game.Instance.Player.SkillPoints)
                {
                    AnsiConsole.WriteLine($"You don't have enough points to learn {selected.Name}.");
                    selection = 0;
                }
            }
        }

        return unlearnedSkills.ElementAt(selection - 1);
    }

    private void ListUnlearnedSkills()
    {
        AnsiConsole.MarkupLine($"[{Colours.ThemeHighlight}]Unlearned skills:[/]\n");
        var previousSpecies = "";

        for (var i = 0; i < unlearnedSkills.Count(); i++)
        {
            var unlearned = unlearnedSkills.ElementAt(i);
            if (unlearned.Species != previousSpecies)
            {
                previousSpecies = unlearned.Species;
                AnsiConsole.MarkupLine($"Species: [{Colours.ThemeHighlight}]{unlearned.Species}[/]");
            }
            AnsiConsole.MarkupLine($"  {i + 1}: [{Colours.ThemeHighlight}]{unlearned.Name}[/] ({unlearned.LearningCost} points): {unlearned.Description}. [{Colours.ThemeHighlight}]{unlearned.Effect}[/].");
        }
    }

    private void ShowIntro(int skillPoints)
    {
        AnsiConsole.MarkupLine($"[{Colours.ThemeHighlight}]Choose your skills.[/] You have [{Colours.ThemeHighlight}]{skillPoints}[/] skill points.\n");
    }

    private void ShowCurrentSkills(string[] skills)
    {
        AnsiConsole.MarkupLine($"[{Colours.ThemeHighlight}]Current skills[/] ({skills.Length}): {string.Join(", ", skills)}\n");
    }
}