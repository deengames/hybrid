using Hybrid.Game.Interfaces;
using Hybrid.Game.IO;
using Spectre.Console;

namespace Hybrid.Game.Story;

class EndGameScene : IScene
{
    public void Show()
    {
        AnsiConsole.MarkupLine($"The queen [{Colours.ThemeHighlight}]bursts open[/], revealing a [{Colours.QueenSpeech}]pulsating chrysalis.[/] You step closer raising your arms to strike. A voice speaks directly into your mind: [{Colours.QueenSpeech}]STOP, BROODLING.[/]");
        AnsiConsole.MarkupLine($"You freeze mid-strike. \"What ... who ...?\" you stammer.");
        AnsiConsole.MarkupLine($"\"[{Colours.QueenSpeech}]You cannot destroy us,[/]\" the voice says, \"[{Colours.QueenSpeech}]without destroying yourself.[/]\" You notice the chrysalis pulsating in time with the voice. \"[{Colours.QueenSpeech}]We are hatchlings from the same brood.[/]\"");
        AnsiConsole.MarkupLine($"You open your mouth to argue, but freeze when your gaze falls on your skin. [{Colours.ThemeHighlight}]Mottled green scales[/] cover the flesh of your hand and arms. [{Colours.ThemeHighlight}]Small stingers[/] jab upward from different areas. As you open and close your hand, [{Colours.ThemeHighlight}]sharp blades[/] emerge and retract. \"What ... am I?\" you finally ask.");
        AnsiConsole.MarkupLine($"\"[{Colours.QueenSpeech}]HUMAN,[/]\" the Queen speaks, \"[{Colours.QueenSpeech}]and yet not. Humans cannot hope to defeat us, only our own broodlings posess the strength. Think. Remember.[/]\" And suddenly, you do.");
        AnsiConsole.MarkupLine($"The memories surge forward unexpectedly: volunteering on a [{Colours.ThemeDark}]suicidal mission to end the war[/]. A top-secret [{Colours.ThemeHighlight}]experimental genetic reprogramming program[/]. A syringe full of [{Colours.ThemeHighlight}]unknown, alien fluid.[/] The similarities between you and the very creatures you killed to reach the queen.");
        AnsiConsole.MarkupLine($"Realization floods you. You're a [{Colours.ThemeHighlight}]hybrid[/]. [{Colours.ThemeDark}]Rage follows.[/] Your can never return to the life you had before; forever an outcast, a genetic monstrosity. \"They didn't expect me to survive,\" you muse.");
        AnsiConsole.MarkupLine($"The chrysalis stops moving for a moment, then lights up. [{Colours.QueenSpeech}]Memories flood through you,[/] of ancient civilizations long gone. Of creatures of all different species, living in harmony - [{Colours.ThemeHighlight}]wing and fang, mandible and tail[/], all different but living in harmony. Hundreds of species, thousands of planets, an uncounted number of years.");
        AnsiConsole.MarkupLine($"\"[{Colours.QueenSpeech}]I am the last Egg Bearer,[/]\" the queen says. \"[{Colours.QueenSpeech}]Our great civilization dies with me. Come with us. We can still rebuild. We can still survive.[/]\"");
        AnsiConsole.MarkupLine($"You hesitate, thoughts whirling. What is there to return to? Human civilization offers you nothing further. With a quiet resolution, you nod, and pick up the chrysalis. You place it on your chest. With a splash of fluid, it [{Colours.QueenSpeech}]fuses into your chest.[/]");
        AnsiConsole.MarkupLine($"As you ascend through the layers of the planetoid, memories trickle into your mind from the queen. One in particular fixates itself: a distant planet, scorched permanently on one side by twin suns, but cool and dark on the other, [{Colours.ThemeHighlight}]rife with multi-cellular life[/]. A place to start a new civilization, perhaps.");
        AnsiConsole.MarkupLine($"You plant your feet firmly on the surface of Nemosa-7 and jump. Your [{Colours.ThemeHighlight}]augmented strength propels you[/] off the surface and into space. Your [{Colours.ThemeDark}]VX5 comm unit crackles to life[/], but you throw it behind you, as you soar towards [{Colours.QueenSpeech}]a new beginning.[/]");

        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine();

        AnsiConsole.MarkupLine($"[{Colours.ThemeHighlight}]Thanks for playing![/] If you have any feedback, send it to NightBlade (NightBlade99 on Twitter, NightBlade#3155 on Discord). I would love to hear from you!");
        AnsiConsole.WriteLine("Press any key to return to the title screen.");
        System.Console.ReadKey(true);

        Game.Instance.ChangeScene(new TitleScene());
    }
}