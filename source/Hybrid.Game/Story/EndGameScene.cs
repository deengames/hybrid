using Hybrid.Game.Character;
using Hybrid.Game.Interfaces;
using Hybrid.Game.IO;
using Spectre.Console;

namespace Hybrid.Game.Story;

class EndGameScene : IScene
{
    public void Show()
    {
        AnsiConsole.MarkupLine($"[{Colours.ThemeHighlight}]Thanks for playing![/] If you have any feedback, send it to NightBlade (NightBlade99 on Twitter, NightBlade#3155 on Discord). I would love to hear from you!");
        AnsiConsole.WriteLine("Press any key to return to the title screen.");
        System.Console.ReadKey(true);

        Game.Instance.ChangeScene(new TitleScene());
    }
}