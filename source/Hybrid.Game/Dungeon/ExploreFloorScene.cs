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
        AnsiConsole.MarkupLine($"[{Colours.ThemeHighlight}]You are on floor {_currentFloor.FloorNumber}[/] [{Colours.ThemeDark}]in room ({_currentFloor.StartRoom.X}, {_currentFloor.StartRoom.Y}).[/]");

        Game.Instance.End();
    }
}
