using Hybrid.Console.Character;
using Hybrid.Console.Interfaces;
using Hybrid.Console.IO;
using Spectre.Console;

namespace Hybrid.Console.Story;

class StoryScene : IScene
{
    public void Show()
    {
        AnsiConsole.MarkupLine($"You [{Colours.ThemeHighlight}]emerge from the pod[/] into the darkness of the lab. A robotic voice speaks from the darkness: \"[{Colours.ThemeHighlight}]Hybridization protocol[/] complete.\"");
        AnsiConsole.MarkupLine($"\"He's battle ready?\" a gruff human voice says. \"Put him on the next transporter to Nemosa-7.\" Hands grab you and pull you into the darkness.");
        AnsiConsole.MarkupLine($"Hours later, you awaken in a jumpship oribiting above Nemosa-7, strapped into the seat of an ejection pod. The desolate face of the asteroid fills the windows.");
        AnsiConsole.MarkupLine($"\"You shouldn't have any problems breathing, thanks to the infusion,\" the pilot says, eyeing you from the corner of his eyes. He mutters something under his breath, and you catch the words [{Colours.ThemeHighlight}]\"freak\" and \"genes.\"[/]");
        AnsiConsole.MarkupLine("\"Report back when you've found and killed the queen,\" he says. Without waiting for a reply, he stabs the ejection button, and you hurtle toward the planetoid.");
        AnsiConsole.MarkupLine($"A short while later, you clamber out of the pod, stretching for the first time. You feel ... [{Colours.ThemeHighlight}]strange. Stronger. Faster.[/] You remember your briefing, and quickly identify and climb down the NIM-3 crater and into the first layer beneath the surface.");
        System.Console.ReadKey(true);

        Program.ChangeScene(new SkillSelectScene());
    }
}