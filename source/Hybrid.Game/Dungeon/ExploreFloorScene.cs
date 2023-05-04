using Hybrid.Core.Dungeon;
using Hybrid.Game.Commands;
using Hybrid.Game.Interfaces;
using Hybrid.Game.IO;
using Spectre.Console;

namespace Hybrid.Game.Dungeon;

class ExploreFloorScene : IScene
{
    private Room _previousRoom;

    public void Show()
    {
        AnsiConsole.MarkupLine($"[{Colours.ThemeHighlight}]You are on floor {Game.Instance.CurrentFloor.FloorNumber}B[/].");
        new LookCommand().Run();
        _previousRoom = Game.Instance.CurrentFloor.CurrentRoom;

        AnsiConsole.MarkupLine($"What now? (Type [{Colours.ThemeHighlight}]help[/] for help, or [{Colours.ThemeHighlight}]quit[/] to quit.)");

        while (true) // "quit" quits
        {
            if (_previousRoom != Game.Instance.CurrentFloor.CurrentRoom)
            {
                new LookCommand().Run();
                _previousRoom = Game.Instance.CurrentFloor.CurrentRoom;
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
                AnsiConsole.WriteLine();
                command.Run();
                continue;
            }

            AnsiConsole.MarkupLine($"Not sure how to \"[{Colours.ThemeHighlight}]{input}[/].\" (Type [{Colours.ThemeHighlight}]help[/] for help.)");
        }
    }

    private ICommand? GetCommand(string name)
    {
        name = name.Trim().ToLower();
        var endIndex = name.Contains(' ') ? name.IndexOf(' ') : name.Length;
        var command = name.Substring(0, endIndex);
        switch (command)
        {
            case "l":
            case "look":
                return new LookCommand();
            case "north":
            case "east":
            case "south":
            case "west":
            case "n":
            case "e":
            case "s":
            case "w":
                return new NavigateCommand(name);
            case "d":
            case "des":
            case "descend":
                return new DescendCommand();
            case "f":
            case "fight":
            case "a":
            case "attack":
                return new FightCommand();
            case "skills":
            case "c":
            case "character":
                return new ShowSkillsCommand();
            case "ex":
            case "examine":
                return new ExamineCommand(name);
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
