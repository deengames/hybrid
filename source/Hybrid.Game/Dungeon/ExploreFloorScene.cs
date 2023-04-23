using Hybrid.Core.Dungeon;
using Hybrid.Game.Commands;
using Hybrid.Game.Interfaces;
using Hybrid.Game.IO;
using Spectre.Console;

namespace Hybrid.Game.Dungeon;

class ExploreFloorScene : IScene
{
    private Floor _currentFloor;

    public ExploreFloorScene(Floor currentFloor)
    {
        _currentFloor = currentFloor;
    }

    public void Show()
    {
        var currentRoom = _currentFloor.StartRoom;

        AnsiConsole.MarkupLine($"[{Colours.ThemeHighlight}]You are on floor {_currentFloor.FloorNumber}[/] [{Colours.ThemeDark}]in room ({currentRoom.X}, {currentRoom.Y}).[/]");
        ListExits(currentRoom);
        ListMonsters(currentRoom);

        AnsiConsole.MarkupLine($"What now? (Type [{Colours.ThemeHighlight}]help[/] for help, or [{Colours.ThemeHighlight}]quit[/] to quit.)");

        while (true) // "quit" quits
        {
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                AnsiConsole.WriteLine("Please enter a command!");
                continue;
            }

            var command = GetCommand(input);
            if (command != null)
            {
                command.Run();
                continue;
            }

            AnsiConsole.MarkupLine($"Not sure how to \"[{Colours.ThemeHighlight}]{input}[/].\" (Type [{Colours.ThemeHighlight}]help[/] for help.)");
        }
    }

    private ICommand? GetCommand(string name)
    {
        switch (name.Trim().ToLower())
        {
            case "h":
            case "help":
                return new HelpCommand();
            case "q":
            case "quit":
                return new QuitCommand();
            default:
                return null;
        }
    }

    private static void ListMonsters(Room currentRoom)
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

    private static void ListExits(Room currentRoom)
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
