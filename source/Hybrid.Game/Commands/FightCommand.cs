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

        while (player.Health > 0 && currentRoom.Monsters.Any())
        {
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
    }
}