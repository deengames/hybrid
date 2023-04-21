using Hybrid.Core.Character;
using Hybrid.Game.Interfaces;
using Hybrid.Game.Story;

namespace Hybrid.Game;

class Game
{
    public static Game Instance { get; private set; } = new Game();

    internal Player Player = new Player();
    internal IScene? currentScene = new TitleScene();

    private Game()
    {
        Game.Instance = this;
    }

    public void Run()
    {
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
        currentScene = null;
        Console.WriteLine("Bye!");
    }

}