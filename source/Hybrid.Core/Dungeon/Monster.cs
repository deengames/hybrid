namespace Hybrid.Core.Dungeon;

public class Monster
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int TotalHealth { get; set; }
    public int Strength { get; set; }
    public int Toughness { get; set; }
    public int Speed { get; set; }
    public int Cost { get; set; }

    // Shallow clone.
    public Monster Clone()
    {
        return this.MemberwiseClone() as Monster;
    }

    public override string ToString()
    {
        return this.Name;
    }
}