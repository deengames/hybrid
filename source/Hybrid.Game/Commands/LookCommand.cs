using Hybrid.Core.Dungeon;
using Hybrid.Game.IO;
using Spectre.Console;

namespace Hybrid.Game.Commands;

class LookCommand : ICommand
{
    private readonly Floor _currentFloor;
    
    public LookCommand(Floor currentFloor)
    {
        _currentFloor = currentFloor;
    }

    public void Run()
    {
        AnsiConsole.MarkupLine($"You stand [{Colours.ThemeDark}]in room ({_currentFloor.CurrentRoom.X}, {_currentFloor.CurrentRoom.Y}).[/]");
        ShowExits(_currentFloor.CurrentRoom);
        ShowMonsters(_currentFloor.CurrentRoom);
    }

    private static void ShowMonsters(Room currentRoom)
    {
        if (!currentRoom.Monsters.Any())
        {
            AnsiConsole.MarkupLine($"There are [{Colours.ThemeDark}]no monsters[/] in this room.");
        }
        else
        {
            AnsiConsole.MarkupLine($"There are [{Colours.ThemeHighlight}]{currentRoom.Monsters.Count} monsters[/] in this room: {String.Join(", ", currentRoom.Monsters.OrderBy(m => m.Cost).ThenBy(m => m.Name).Select(m => m.Name))}");
        }
    }

    private static void ShowExits(Room currentRoom)
    {
        AnsiConsole.MarkupLine($"Exits:");

        if (currentRoom.North != null)
        {
            AnsiConsole.MarkupLine($"  [{Colours.ThemeDark}]North[/]");
        }
        if (currentRoom.East != null)
        {
            AnsiConsole.MarkupLine($"  [{Colours.ThemeDark}]East[/]");
        }
        if (currentRoom.South != null)
        {
            AnsiConsole.MarkupLine($"  [{Colours.ThemeDark}]South[/]");
        }
        if (currentRoom.West != null)
        {
            AnsiConsole.MarkupLine($"  [{Colours.ThemeDark}]West[/]");
        }
    }
}
