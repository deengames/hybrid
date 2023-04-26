using Hybrid.Core.Dungeon.Generators;
using Hybrid.Game;
using Hybrid.Game.Character;
using Hybrid.Game.Commands;
using Hybrid.Game.IO;
using Spectre.Console;

class ShowSkillsCommand : ICommand
{
    public void Run()
    {
        Game.Instance.ChangeScene(new SkillSelectScene(Game.Instance.Player.Skills));
    }
}
