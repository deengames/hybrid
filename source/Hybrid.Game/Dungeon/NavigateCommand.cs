using Hybrid.Core.Dungeon;
using Hybrid.Game.Commands;
using Hybrid.Game.IO;
using Spectre.Console;

namespace Hybrid.Game.Dungeon
{
    internal class NavigateCommand : ICommand
    {
        private readonly string _direction;
        private readonly Floor _currentFloor;

        public NavigateCommand(string direction, Floor currentFloor)
        {
            _direction = direction;
            _currentFloor = currentFloor;
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
            if (direction == "north" && _currentFloor.CurrentRoom.North != null)
            {
                _currentFloor.CurrentRoom = _currentFloor.CurrentRoom.North;
                ShowMove(direction);
            }
            else if (direction == "east" && _currentFloor.CurrentRoom.East != null)
            {
                _currentFloor.CurrentRoom = _currentFloor.CurrentRoom.East;
                ShowMove(direction);
            }
            else if (direction == "south" && _currentFloor.CurrentRoom.South != null)
            {
                _currentFloor.CurrentRoom = _currentFloor.CurrentRoom.South;
                ShowMove(direction);
            }
            else if (direction == "west" && _currentFloor.CurrentRoom.West != null)
            {
                _currentFloor.CurrentRoom = _currentFloor.CurrentRoom.West;
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