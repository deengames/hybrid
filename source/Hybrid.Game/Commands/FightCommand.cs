using Hybrid.Core.Character;
using Hybrid.Core.Character.Skills;
using Hybrid.Game.IO;
using Spectre.Console;

namespace Hybrid.Game.Commands;

class FightCommand : ICommand
{
    public void Run()
    {
        var currentRoom = Game.Instance.CurrentFloor.CurrentRoom;
        var player = Game.Instance.Player;
        if (!currentRoom.Monsters.Any())
        {
            AnsiConsole.MarkupLine("There's nothing to fight here.");
            return;
        }

        var actors = new List<Actor>() { player };
        actors.AddRange(currentRoom.Monsters);
        actors = actors.OrderByDescending(a => a.Speed).ToList();

        while (player.Health > 0 && actors.Any(a => a != player))
        {
            // AnsiConsole.MarkupLine($"\nHealth: {player.Health}/{player.TotalHealth}");
            foreach (var actor in actors)
            {
                if (actor.Health <= 0)
                {
                    continue;
                }

                var result = actor.TakeTurn(actors);

                result = FormatMarkup(result);
                AnsiConsole.MarkupLine(result);
            }

            // Apply round-end skill effects
            foreach (var actor in actors)
            {
                AnsiConsole.Markup(FormatMarkup(SkillManager.Instance.OnRoundEnd(actor.Skills)));
            }

            // Died? Sayounara.
            actors.RemoveAll(a => a.Health <= 0);

            // Speed can change mid-round
            actors = actors.OrderByDescending(a => a.Speed).ToList();
        }

        if (player.Health > 0)
        {
            AnsiConsole.MarkupLine($"[{Colours.ThemeHighlight}]You vanquish[/] the monsters, with [{Colours.ThemeDark}]{player.Health}/{player.TotalHealth}[/] health. Your wounds knit close.\n");

            player.Heal(player.TotalHealth);
            Game.Instance.CurrentFloor.CurrentRoom.Monsters.Clear();
            new LookCommand().Run();
        }
        else
        {
            AnsiConsole.MarkupLine($"[{Colours.ThemeDark}]Your body disintegrates.[/] [{Colours.ThemeHighlight}]Game over[/] ...");
            new QuitCommand().Run();
        }
    }

    private string FormatMarkup(string input)
    {
        return input
            .Replace("[highlight]", $"[{Colours.ThemeHighlight}]")
            .Replace("[dark]", $"[{Colours.ThemeDark}]");
    }
}