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
            AnsiConsole.MarkupLine("Press any key to start a new game, or q to quit.");
            var key = System.Console.ReadKey(true);

            if (key.KeyChar == 'q' || key.KeyChar == 'Q')
            {
                Console.WriteLine("Bye!");
                System.Environment.Exit(0);
            }
            
            Game.Instance.ChangeScene(new StoryScene());
        }
    }
}