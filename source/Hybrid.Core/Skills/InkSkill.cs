using Hybrid.Core.Character;

namespace Hybrid.Core.Skills.Player;

class SquidInkSkill : BaseSkill
{
    private Dictionary<Actor, int> _inkAmount = new();
    private Random random = new Random();

    public const float MissProbabilityPerInk = 0.2f;

    public override string OnRoundEnd(IEnumerable<Actor> actors)
    {
        var player = actors.Single(a => a is Character.Player);

        var target = actors.Where(t => t != player && t.Health > 0).OrderBy(r => random.Next()).FirstOrDefault();
        if (target == null)
        {
            return string.Empty; // nobody left to ink
        }
        
        if (!_inkAmount.ContainsKey(target))
        {
            _inkAmount[target] = 0;
        }

        _inkAmount[target]++;
        return $"You [dark]spray dark ink[/] at {target.Name}!\n";
    }

    override public string OnAttacked(Actor attacker, Actor target, string[] skills)
    {
        if (_inkAmount.ContainsKey(attacker))
        {
            var missProbability = MissProbabilityPerInk * _inkAmount[attacker];
            if (random.NextDouble() <= missProbability)
            {
                return $"{attacker.Name} [dark]misses[/]!";
            }
        }

        return string.Empty;
    }
}