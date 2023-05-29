using Hybrid.Game;
using Hybrid.Game.Character;
using Hybrid.Game.Commands;

class ShowCharacterCommand : ICommand
{
    public void Run()
    {
        Game.Instance.ChangeScene(new ShowCharacterScene());
    }
}
