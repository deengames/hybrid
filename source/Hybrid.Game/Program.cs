namespace Hybrid.Game;

class Program
{

    public static void Main(string[] args)
    {
        var done = Core.Dungeon.FloorGenerator.Generate();
        Game.Instance.Run();
    }
}