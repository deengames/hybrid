using Hybrid.Core.Character;
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
        SkillData? toLearn = null;

        do
        {
            toLearn = AskWhichSkillToLearn();
            if (toLearn.HasValue) {
                player.Learn(toLearn.Value);
                AnsiConsole.MarkupLine($"Your genes reconfigure themselves. [{Colours.ThemeHighlight}]You learn {toLearn.Value.Name}.[/]");
            }
        } while (toLearn.HasValue);

        AnsiConsole.WriteLine("You descend past the cusp of NIM-3 and into the first subterranean layer.");
        System.Console.ReadKey(true);
        Game.Instance.End();
    }

    private SkillData? AskWhichSkillToLearn()
    {
        int selection = 0;
        var allData = PlayerSkillsData.AllSkills;

        while (selection <= 0 || selection > unlearnedSkills.Count())
        {
            selection = AnsiConsole.Ask<int>("Enter the number of the skill to learn, or 0 to start the game: ");
            
            if (selection == 0)
            {
                return null;
            }

            if (selection > 0 && selection <= unlearnedSkills.Count()) {
                var selected = allData.Single(s => s.Name == unlearnedSkills.ElementAt(selection - 1).Name);
                if (selected.LearningCost > Game.Instance.Player.SkillPoints)
                {
                    AnsiConsole.WriteLine($"You don't have enough points to learn {selected.Name}.");
                    selection = 0;
                } else if (Game.Instance.Player.Skills.Any(s => s == selected.Name))
                {
                    AnsiConsole.WriteLine($"You already assimilated {selected.Name}.");
                    selection = 0;
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Invalid input[/]");
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