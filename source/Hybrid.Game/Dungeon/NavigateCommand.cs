using Hybrid.Core.Dungeon;
using Hybrid.Game.Commands;
using Hybrid.Game.IO;
using Spectre.Console;

namespace Hybrid.Game.Dungeon
{
    internal class NavigateCommand : ICommand
    {
        private readonly string _direction;

        public NavigateCommand(string direction)
        {
            _direction = direction;
        }

        public void Run()
        {
            switch (_direction)
            {
                case "n":
                case "north":
                    TryToMove("north");
                    break;
                case "e":
                case "east":
                    TryToMove("east");
                    break;
                case "s":
                case "south":
                    TryToMove("south");
                    break;
                case "w":
                case "west":
                    TryToMove("west");
                    break;
                default:
                    throw new InvalidOperationException($"Invalid direction: {_direction}");
            }
        }

        private void TryToMove(string direction)
        {
            var currentFloor = Game.Instance.CurrentFloor;

            if (direction == "north" && currentFloor.CurrentRoom.North != null)
            {
                currentFloor.CurrentRoom = currentFloor.CurrentRoom.North;
                ShowMove(direction);
            }
            else if (direction == "east" && currentFloor.CurrentRoom.East != null)
            {
                currentFloor.CurrentRoom = currentFloor.CurrentRoom.East;
                ShowMove(direction);
            }
            else if (direction == "south" && currentFloor.CurrentRoom.South != null)
            {
                currentFloor.CurrentRoom = currentFloor.CurrentRoom.South;
                ShowMove(direction);
            }
            else if (direction == "west" && currentFloor.CurrentRoom.West != null)
            {
                currentFloor.CurrentRoom = currentFloor.CurrentRoom.West;
                ShowMove(direction);
            }
            else 
            {
                AnsiConsole.Markup($"You can't go [{Colours.ThemeDark}]{direction}[/] from here.");
            }
        }

        private void ShowMove(string direction)
        {
            AnsiConsole.MarkupLine($"You move [{Colours.ThemeDark}]{direction}[/].\n");
        }
    }
}