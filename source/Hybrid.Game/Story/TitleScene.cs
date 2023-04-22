using Hybrid.Game.Interfaces;
using Hybrid.Game.IO;
using Spectre.Console;

namespace Hybrid.Game.Story
{
    class TitleScene : IScene
    {
        public void Show()
        {
            AnsiConsole.MarkupLine($"[{Colours.ThemeHighlight}]Hyb[/][{Colours.ThemeDark}]rid[/]");
            AnsiConsole.MarkupLine("Press any key to start a new game.");
            System.Console.ReadKey(true);

            Game.Instance.ChangeScene(new StoryScene());
        }
    }
}