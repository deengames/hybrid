using Spectre.Console;

namespace SporeMan;

class Program {
    public static void Main(string[] args) {
        AnsiConsole.MarkupLine("[#080]Spore[/]Man");
        var input = Console.ReadKey(true);
        Console.WriteLine($"Bye, {input.KeyChar}!");
        Thread.Sleep(1000);
    }
}