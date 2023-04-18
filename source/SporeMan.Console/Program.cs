using Spectre.Console;
using SporeMan.Console.IO;

namespace SporeMan.Console;

class Program {
    public static void Main(string[] args) {
        AnsiConsole.MarkupLine("[#0f8]Spore[/]Man");
        AnsiConsole.MarkupLine("Press any key to start a new game.");
        System.Console.ReadKey();
    }
}