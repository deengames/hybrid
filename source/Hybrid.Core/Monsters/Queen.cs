namespace Hybrid.Core.Monsters;

class Queen : Monster
{
    public Queen()
    {
        Name = $"The Queen";
        TotalHealth = 250;
        Health = TotalHealth;
        Strength = 23;
        Toughness = 10;
        Speed = 10;
        Skills = new string[] { "Blood Horn", "Regeneration" };
    }
}