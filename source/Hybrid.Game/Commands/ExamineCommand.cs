using System.Text;
using Hybrid.Core.Monsters;
using Hybrid.Game.IO;
using Spectre.Console;

namespace Hybrid.Game.Commands;

class ExamineCommand : ICommand
{
    private readonly string _target = "";

    public ExamineCommand(string input)
    {
        input = input.ToLower();
        Console.WriteLine("What the? " + input);
        var index = 0;
        if (input.StartsWith("examine"))
        {
            index = "examine".Length;
        } else if (input.StartsWith("ex"))
        {
            index = 2;
        }
        _target = input.Substring(index).Trim();
    }

    public void Run()
    {
        var currentRoom = Game.Instance.CurrentFloor.CurrentRoom;

        if (!currentRoom.Monsters.Any())
        {
            Console.WriteLine("There are no monsters here to examine.");
            return;
        }
            
        if (string.IsNullOrWhiteSpace(_target))
        {
            Console.WriteLine("Examine which monster?");
            return;
        }

        var monsters = currentRoom.Monsters;
        var lookAt = monsters.FirstOrDefault(m => m.Name.ToLower().StartsWith(_target));
        
        if (lookAt == null)
        {
            lookAt = monsters.FirstOrDefault(m => m.Name.ToLower().Contains(_target));
        }
        
        if (lookAt == null)
        {
            AnsiConsole.MarkupLine($"Can't see any monster named [{Colours.ThemeDark}]{_target}[/] here.");
            return;
        }

        AnsiConsole.MarkupLine(ShowStats(lookAt));
    }

    private static string ShowStats(Monster monster)
    {
        var toReturn = new StringBuilder();
        
        toReturn.Append($"You stare at the [{Colours.ThemeHighlight}]{monster.Name}[/] with all your eyes. It has [{Colours.ThemeHighlight}]{monster.Health}[/] health, [{Colours.ThemeHighlight}]{monster.Strength}[/] strength, [{Colours.ThemeHighlight}]{monster.GetToughness()}[/] toughness, and [{Colours.ThemeHighlight}]{monster.Speed}[/] speed.");

        if (monster.Skills.Any())
        {
            var skills = string.Join(", ", monster.Skills);
            toReturn.Append($" Your [{Colours.ThemeDark}]extra eyes[/] notice that it also has the ability to use: [{Colours.ThemeHighlight}]{skills}[/].");
        }

        return toReturn.ToString();
    }
}
