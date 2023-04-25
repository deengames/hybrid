using Hybrid.Core.Dungeon;
using Hybrid.Game.IO;
using Spectre.Console;

namespace Hybrid.Game.Commands;

class FightCommand : ICommand
{
    public void Run()
    {
        var currentRoom = Game.Instance.CurrentFloor.CurrentRoom;
        var player = Game.Instance.Player;

        var actors = new List<Actor>() { player };
        actors.AddRange(currentRoom.Monsters);
        actors = actors.OrderByDescending(a => a.Speed).ToList();

        while (player.Health > 0 && actors.Any(a => a != player))
        {
            AnsiConsole.MarkupLine($"\nHealth: {player.Health}/{player.TotalHealth}");
            foreach (var actor in actors)
            {
                if (actor.Health <= 0)
                {
                    continue;
                }

                var result = actor.TakeTurn(actors);
                result = result
                    .Replace("[highlight]", $"[{Colours.ThemeHighlight}]")
                    .Replace("[dark]", $"[{Colours.ThemeDark}]");
                AnsiConsole.MarkupLine(result);
            }

            // Died? Sayounara.
            actors.RemoveAll(a => a.Health <= 0);
        }

        if (player.Health > 0)
        {
            AnsiConsole.MarkupLine($"[{Colours.ThemeHighlight}]VICTORY![/]\n");
            Game.Instance.CurrentFloor.CurrentRoom.Monsters.Clear();
            new LookCommand().Run();
        }
        else
        {
            AnsiConsole.MarkupLine($"[{Colours.ThemeDark}]Defeated! Game over ...[/]");
            new QuitCommand().Run();
        }
    }
}