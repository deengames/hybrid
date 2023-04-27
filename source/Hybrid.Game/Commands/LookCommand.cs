using Hybrid.Core.Dungeon;
using Hybrid.Game.IO;
using Spectre.Console;

namespace Hybrid.Game.Commands;

class LookCommand : ICommand
{
    public void Run()
    {
        var currentFloor = Game.Instance.CurrentFloor;

        AnsiConsole.MarkupLine($"You stand in [{Colours.ThemeDark}]in ({currentFloor.CurrentRoom.X}, {currentFloor.CurrentRoom.Y}).[/]");
        if (currentFloor.CurrentRoom == currentFloor.StairsRoom)
        {
            AnsiConsole.MarkupLine($"You spy a [{Colours.ThemeHighlight}]deep chasm[/] leading to the [{Colours.ThemeDark}]next subterranean layer[/]. (Type d to descend.)");
        }

        ShowExits(currentFloor.CurrentRoom);
        ShowMonsters(currentFloor.CurrentRoom);
    }

    private static void ShowMonsters(Room currentRoom)
    {
        if (!currentRoom.Monsters.Any())
        {
            AnsiConsole.MarkupLine($"There are [{Colours.ThemeDark}]no monsters[/] in this room.");
        }
        else
        {
            if (currentRoom.Monsters.Count > 1)
            {
                AnsiConsole.MarkupLine($"There are [{Colours.ThemeHighlight}]{currentRoom.Monsters.Count} monsters[/] in this room: {String.Join(", ", currentRoom.Monsters.OrderBy(m => m.Cost).ThenBy(m => m.Name).Select(m => m.Name))}");
            }
            else
            {
                AnsiConsole.MarkupLine($"There is a [{Colours.ThemeHighlight}]{currentRoom.Monsters.Single().Name}[/] in this room.");
            }
        }
    }

    private static void ShowExits(Room currentRoom)
    {
        AnsiConsole.MarkupLine($"Exits:");

        if (currentRoom.North != null)
        {
            AnsiConsole.MarkupLine($"  [{Colours.ThemeHighlight}]North[/]");
        }
        if (currentRoom.East != null)
        {
            AnsiConsole.MarkupLine($"  [{Colours.ThemeHighlight}]East[/]");
        }
        if (currentRoom.South != null)
        {
            AnsiConsole.MarkupLine($"  [{Colours.ThemeHighlight}]South[/]");
        }
        if (currentRoom.West != null)
        {
            AnsiConsole.MarkupLine($"  [{Colours.ThemeHighlight}]West[/]");
        }
    }
}
