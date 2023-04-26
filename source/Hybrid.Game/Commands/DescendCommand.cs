using Hybrid.Core.Dungeon.Generators;
using Hybrid.Game;
using Hybrid.Game.Commands;
using Hybrid.Game.IO;
using Spectre.Console;

class DescendCommand : ICommand
{
    public void Run()
    {
        var currentFloor = Game.Instance.CurrentFloor;
        if (currentFloor.CurrentRoom == currentFloor.StairsRoom)
        {
            Game.Instance.CurrentFloor = FloorGenerator.Generate(Game.Instance.CurrentFloor.FloorNumber + 1);
            AnsiConsole.MarkupLine($"You descend to [{Colours.ThemeHighlight}]{Game.Instance.CurrentFloor.FloorNumber}B[/]. You gain [{Colours.ThemeHighlight}]1[/] skill point.");
            Game.Instance.Player.LevelUp();
            return;
        }

        AnsiConsole.MarkupLine("You don't see any way deeper into the planetoid from here.");
    }
}
