using Hybrid.Game.IO;
using Spectre.Console;

namespace Hybrid.Game.Commands;

class HelpCommand : ICommand
{
    public void Run()
    {
        AnsiConsole.MarkupLine($"Type [{Colours.ThemeHighlight}]n, e, s, or w[/] to move north, east, south, or west; type [{Colours.ThemeHighlight}]l[/] to look around, and [{Colours.ThemeHighlight}]f[/] to fight automatically. To check what skills you can learn, type [{Colours.ThemeHighlight}]c[/] or [{Colours.ThemeHighlight}]skill[/]. When you find the crater down to the next floor, you can type [{Colours.ThemeHighlight}]d[/] to descend. Type [{Colours.ThemeHighlight}]ex or examine[/] followed by a monster name, to take a look at that monster's stats.");

        AnsiConsole.WriteLine();

        AnsiConsole.MarkupLine($"[{Colours.ThemeHighlight}]Tips:[/]");
        if (FeatureToggles.FullHealAfterBattles)
        {
            AnsiConsole.WriteLine("  You return to full health before each battle.");
        }
        AnsiConsole.WriteLine("  Choose skills carefully, they make a big difference.");
        AnsiConsole.WriteLine("  You gain experience points and level up when you kill enough monsters.");
        if (FeatureToggles.LevelUpOnDescend)
        {
            AnsiConsole.WriteLine("  You also gain a level every time you descend down a floor.");
        }
    }
}