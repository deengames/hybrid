using Hybrid.Console.IO;
using Spectre.Console;

namespace Hybrid.Console.Story
{
    class TitleScene
    {
        public void Show()
        {
            AnsiConsole.MarkupLine($"[{Colours.ThemeHighlight}]Hyb[/]rid");
            AnsiConsole.MarkupLine("Press any key to start a new game.");
            System.Console.ReadKey(true);
        }
    }
}