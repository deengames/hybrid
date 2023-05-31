using Hybrid.Core.Character;
using Hybrid.Core.Skills;
using Hybrid.Game.IO;
using Hybrid.Game.Story;
using Spectre.Console;

namespace Hybrid.Game.Commands;

class FightCommand : ICommand
{
    public void Run()
    {
        var currentRoom = Game.Instance.CurrentFloor.CurrentRoom;
        var player = Game.Instance.Player;
        if (!currentRoom.Monsters.Any())
        {
            AnsiConsole.MarkupLine("There's nothing to fight here.");
            return;
        }

        var actors = new List<Actor>() { player };
        actors.AddRange(currentRoom.Monsters);
        actors = actors.OrderByDescending(a => a.Speed).ToList();

        while (player.Health > 0 && actors.Any(a => a != player))
        {
            // AnsiConsole.MarkupLine($"\nHealth: {player.Health}/{player.TotalHealth}");
            foreach (var actor in actors)
            {
                if (actor.Health <= 0)
                {
                    continue;
                }

                var result = actor.TakeTurn(actors);

                result = FormatMarkup(result);
                AnsiConsole.MarkupLine(result);
            }

            // Apply round-end skill effects
            foreach (var actor in actors)
            {
                AnsiConsole.Markup(FormatMarkup(SkillManager.Instance.OnRoundEnd(actor.Skills)));
            }

            // Died? Sayounara.
            var dead = actors.Where(a => a.Health <= 0);
            var leveledUp = false;
            foreach (var corpse in dead)
            {
                leveledUp |= player.OnMonsterDied(corpse);
            }
            actors.RemoveAll(a => dead.Contains(a));

            // Speed can change mid-round
            actors = actors.OrderByDescending(a => a.Speed).ToList();

            if (leveledUp)
            {
                AnsiConsole.MarkupLine($"[{Colours.ThemeHighlight}]You leveled up![/] You gained one of each stat point, and ten total and current health.");
            }
        }

        if (player.Health > 0)
        {
            foreach (var actor in actors)
            {
                AnsiConsole.Markup(FormatMarkup(SkillManager.Instance.OnBattleEnd(actor.Skills)));
            }

            if (Game.Instance.CurrentFloor.CurrentRoom.Monsters.Any(m => m.Name.Contains("Queen")))
            {
                Game.Instance.ChangeScene(new EndGameScene());
            }
            else
            {
                AnsiConsole.Markup($"[{Colours.ThemeHighlight}]You vanquish[/] the monsters, with [{Colours.ThemeDark}]{player.Health}/{player.TotalHealth}[/] health.");
                if (FeatureToggles.FullHealAfterBattles && player.Health < player.TotalHealth)
                {
                    AnsiConsole.Write("Your wounds knit close.");
                }
                AnsiConsole.WriteLine();

                if (FeatureToggles.FullHealAfterBattles)
                {
                    player.Heal(player.TotalHealth);
                }
                player.ToughnessModifier = 0; // remove burns, boosts
                Game.Instance.CurrentFloor.CurrentRoom.Monsters.Clear();
                new LookCommand().Run();
            }
        }
        else
        {
            AnsiConsole.MarkupLine($"[{Colours.ThemeDark}]Your body disintegrates.[/] [{Colours.ThemeHighlight}]Game over[/] ...");
            new QuitCommand().Run();
        }
    }

    private string FormatMarkup(string input)
    {
        return input
            .Replace("[highlight]", $"[{Colours.ThemeHighlight}]")
            .Replace("[dark]", $"[{Colours.ThemeDark}]");
    }
}