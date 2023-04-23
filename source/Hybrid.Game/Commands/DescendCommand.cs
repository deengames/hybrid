using Hybrid.Core.Dungeon;
using Hybrid.Core.Dungeon.Generators;
using Hybrid.Game.Commands;
using Hybrid.Game.IO;
using Spectre.Console;

class DescendCommand : ICommand
{
    private Floor _currentFloor;

    public DescendCommand(Floor currentFloor)
    {
        _currentFloor = currentFloor;
    }

    public void Run()
    {
        if (_currentFloor.CurrentRoom == _currentFloor.StairsRoom)
        {
            // How do we update _currentFloor?
            AnsiConsole.MarkupLine($"You descend to [{Colours.ThemeHighlight}]{_currentFloor.FloorNumber}[/]. Well, not really.");
            return;
        }

        AnsiConsole.MarkupLine("You don't see any way deeper into the planetoid from here.");
    }
}
