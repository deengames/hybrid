using Hybrid.Game.Interfaces;

namespace Hybrid.Game.Character;

class SkillSelectScene : IScene
{
    public void Show()
    {
        System.Console.WriteLine("Hello, world.");
        System.Console.ReadKey(true);
        Program.EndGame();
    }
}