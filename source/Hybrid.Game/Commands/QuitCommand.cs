using Spectre.Console;

namespace Hybrid.Game.Commands;

class QuitCommand : ICommand
{
    public void Run()
    {
        Game.Instance.End();
        System.Environment.Exit(0);
    }
}