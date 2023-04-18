using Spectre.Console;
using SporeMan.Console.IO;

namespace SporeMan.Console;

class Program {
    public static void Main(string[] args) {
        AudioPlayer.Play(Path.Join("assets", "audio", "sfx", "random.wav"));
        AudioPlayer.OnPlayDone += (file) => System.Console.WriteLine($"Done {file}");
        AnsiConsole.MarkupLine("[#080]Spore[/]Man");
        var input = System.Console.ReadKey(true);
        System.Console.WriteLine($"Bye, {input.KeyChar}!");
        Thread.Sleep(1000);
    }
}