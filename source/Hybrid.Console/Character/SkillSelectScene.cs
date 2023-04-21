using Hybrid.Console.Interfaces;

namespace Hybrid.Console.Character;

class SkillSelectScene : IScene
{
    public void Show()
    {
        System.Console.WriteLine("Hello, world.");
        System.Console.ReadKey(true);
        Program.EndGame();
    }
}