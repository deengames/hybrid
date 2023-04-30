using Hybrid.Game.IO;
using Spectre.Console;

namespace Hybrid.Game.Commands;

class HelpCommand : ICommand
{
    public void Run()
    {
        AnsiConsole.MarkupLine($"Type [{Colours.ThemeHighlight}]n, e, s, or w[/] to move north, east, south, or west; type [{Colours.ThemeHighlight}]l[/] to look around, and [{Colours.ThemeHighlight}]f[/] to fight automatically. To check what skills you can learn, type [{Colours.ThemeHighlight}]c[/] or [{Colours.ThemeHighlight}]skill[/]. When you find the crater down to the next floor, type [{Colours.ThemeHighlight}]d[/] to descend.");

        AnsiConsole.WriteLine();

        AnsiConsole.MarkupLine($"[{Colours.ThemeHighlight}]Tips:[/]");
        AnsiConsole.WriteLine("  1) You return to full health before each battle.");
        AnsiConsole.WriteLine("  2) Choose skills carefully, they make a big difference.");
        AnsiConsole.WriteLine("  3) You gain experience points and level up when you kill enough monsters.");
        AnsiConsole.WriteLine("  4) You also gain a level every time you descend down a floor.");
    }
}