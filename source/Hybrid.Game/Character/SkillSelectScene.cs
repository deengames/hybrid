using Hybrid.Game.Interfaces;
using Hybrid.Game.IO;
using Spectre.Console;

namespace Hybrid.Game.Character;

class SkillSelectScene : IScene
{
    public void Show()
    {
        var player = Game.Instance.Player;

        AnsiConsole.MarkupLine($"[{Colours.ThemeHighlight}]Choose your skills.[/] You have [{Colours.ThemeHighlight}]{player.SkillPoints}[/] skill points.");
        System.Console.ReadKey(true);
        Game.Instance.End();
    }
}