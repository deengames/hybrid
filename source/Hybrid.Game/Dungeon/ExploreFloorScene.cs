using Hybrid.Core.Dungeon;
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

        if (!currentRoom.Monsters.Any())
        {
            AnsiConsole.MarkupLine($"There are [{Colours.ThemeDark}]no monsters[/] in this room.");
        }
        else
        {
            AnsiConsole.MarkupLine($"There are [{Colours.ThemeHighlight}]{currentRoom.Monsters.Count} monsters[/] in this room: {String.Join(", ", currentRoom.Monsters.Select(m => m.Name))}");
        }

        Game.Instance.End();
    }
}
