using Hybrid.Core.Character;
using Hybrid.Core.Data.Skills;
using Hybrid.Core.Dungeon.Generators;
using Hybrid.Game.Dungeon;
using Hybrid.Game.Interfaces;
using Hybrid.Game.IO;
using Spectre.Console;

namespace Hybrid.Game.Character;

class ShowCharacterScene : IScene
{
    public ShowCharacterScene()
    {
    }

    public void Show()
    {
        var player = Game.Instance.Player;
        ShowStats(player);
        Game.Instance.ChangeScene(new ExploreFloorScene());
    }

    private void ShowStats(Player player)
    {
        var health = $"[{Colours.ThemeHighlight}]{player.TotalHealth}";
        if (!FeatureToggles.FullHealAfterBattles)
        {
            health = $"[{Colours.ThemeHighlight}]{player.Health}/{player.TotalHealth}";
        }

        AnsiConsole.MarkupLine($"You are on level [{Colours.ThemeHighlight}]{player.Level}[/]. You have [{Colours.ThemeHighlight}]{player.Xp}[/] experience points, and will level up with [{Colours.ThemeHighlight}]{player.XpToLevelUp()}[/] more experience points.");
        
        AnsiConsole.MarkupLine($"You have {health}[/] health, [{Colours.ThemeHighlight}]{player.Strength}[/] strength, [{Colours.ThemeHighlight}]{player.GetToughness()}[/] toughness, and [{Colours.ThemeHighlight}]{player.Speed}[/] speed.");
    }
}