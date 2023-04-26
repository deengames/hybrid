using Hybrid.Game.IO;
using Spectre.Console;

namespace Hybrid.Game.Commands;

class HelpCommand : ICommand
{
    public void Run()
    {
        AnsiConsole.MarkupLine($"Type [{Colours.ThemeHighlight}]n, e, s, or w[/] to move north, east, south, or west; type [{Colours.ThemeHighlight}]l[/] to look around, and [{Colours.ThemeHighlight}]f[/] to fight automatically. To check what skills you can learn, type [{Colours.ThemeHighlight}]c[/] or [{Colours.ThemeHighlight}]skill[/]. When you find the crater down to the next floor, type [{Colours.ThemeHighlight}]d[/] to descend.");
    }
}