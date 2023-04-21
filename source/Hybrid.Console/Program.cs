using Hybrid.Console.Interfaces;
using Hybrid.Console.Story;
using Spectre.Console;

namespace Hybrid.Console;

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