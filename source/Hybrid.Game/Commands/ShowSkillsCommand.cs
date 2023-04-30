using Hybrid.Game;
using Hybrid.Game.Character;
using Hybrid.Game.Commands;

class ShowSkillsCommand : ICommand
{
    public void Run()
    {
        Game.Instance.ChangeScene(new SkillSelectScene(Game.Instance.Player.Skills));
    }
}
