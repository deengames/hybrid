using Hybrid.Core.Character;
using Hybrid.Core.Dungeon;
using Hybrid.Game.Interfaces;
using Hybrid.Game.Story;
using Spectre.Console;

namespace Hybrid.Game;

class Game
{
    public static Game Instance { get; private set; } = new Game();

    internal Player Player = new Player();
    internal Floor CurrentFloor;
    internal IScene? currentScene = new TitleScene();

    private Game()
    {
        Game.Instance = this;
    }

    public void Run()
    {
        // Can we change the default, pretty please?
        // Best practice of Spectre: assume background is not black
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        
        while (currentScene != null)
        {
            currentScene.Show();
        }
    }

    public void ChangeScene(IScene scene)
    {
        currentScene = scene;
        if (currentScene != null)
        {
            currentScene.Show();
        }
    }

    public void End()
    {
        AnsiConsole.ResetColors();
        currentScene = null;
        Console.WriteLine("Bye!");
    }
}