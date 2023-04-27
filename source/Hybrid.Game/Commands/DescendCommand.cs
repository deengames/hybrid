using Hybrid.Core.Dungeon;
using Hybrid.Core.Dungeon.Generators;
using Hybrid.Game;
using Hybrid.Game.Commands;
using Hybrid.Game.IO;
using Hybrid.Game.Story;
using Spectre.Console;

class DescendCommand : ICommand
{
    public void Run()
    {
        var currentFloor = Game.Instance.CurrentFloor;
        if (currentFloor.CurrentRoom == currentFloor.StairsRoom)
        {
            if (currentFloor.FloorNumber == Floor.FinalFloorNumber)
            {
                Game.Instance.ChangeScene(new EndGameScene());
                return;
            }

            if (currentFloor.CurrentRoom.Monsters.Any())
            {
                AnsiConsole.MarkupLine($"You can't descend while [{Colours.ThemeDark}]there are monsters[/] around.");
                return;
            }

            Game.Instance.CurrentFloor = FloorGenerator.Generate(currentFloor.FloorNumber + 1);
            AnsiConsole.MarkupLine($"You descend to [{Colours.ThemeHighlight}]{Game.Instance.CurrentFloor.FloorNumber}B[/].");
            AnsiConsole.MarkupLine($"You gained a level! Your strength, toughness, and agility increased by [{Colours.ThemeHighlight}]1 each[/]. You gained [{Colours.ThemeHighlight}]1[/] skill point.");
            Game.Instance.Player.LevelUp();
            return;
        }

        AnsiConsole.MarkupLine("You don't see any way deeper into the planetoid from here.");
    }
}
