using Hybrid.Core.Dungeon;
using Hybrid.Game.Commands;
using Hybrid.Game.Interfaces;
using Hybrid.Game.IO;
using Spectre.Console;

namespace Hybrid.Game.Dungeon;

class ExploreFloorScene : IScene
{
    private Floor _currentFloor;
    private Room _previousRoom;

    public ExploreFloorScene(Floor currentFloor)
    {
        _currentFloor = currentFloor;
    }

    public void Show()
    {
        AnsiConsole.MarkupLine($"[{Colours.ThemeHighlight}]You are on floor {_currentFloor.FloorNumber}[/].");
        new LookCommand(_currentFloor).Run();
        _previousRoom = _currentFloor.CurrentRoom;

        AnsiConsole.MarkupLine($"What now? (Type [{Colours.ThemeHighlight}]help[/] for help, or [{Colours.ThemeHighlight}]quit[/] to quit.)");

        while (true) // "quit" quits
        {
            if (_previousRoom != _currentFloor.CurrentRoom)
            {
                new LookCommand(_currentFloor).Run();
                _previousRoom = _currentFloor.CurrentRoom;
            }

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
        name = name.Trim().ToLower();
        switch (name)
        {
            case "north":
            case "east":
            case "south":
            case "west":
            case "n":
            case "e":
            case "s":
            case "w":
                return new NavigateCommand(name, _currentFloor);
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
}
