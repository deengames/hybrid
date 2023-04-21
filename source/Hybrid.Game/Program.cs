using Hybrid.Game.Interfaces;
using Hybrid.Game.Story;
using Spectre.Console;

namespace Hybrid.Game;

class Program {

    internal static IScene currentScene = new TitleScene();

    public static void ChangeScene(IScene scene)
    {
        currentScene = scene;
        if (currentScene != null)
        {
            currentScene.Show();
        }
    }

    public static void EndGame()
    {
        currentScene = null;
    }

    public static void Main(string[] args) {

        while (currentScene != null)
        {
            currentScene.Show();
        }
    }
}