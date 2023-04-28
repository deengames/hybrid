using Hybrid.Core.Dungeon;

namespace Hybrid.Core.Data;

public static class MonstersData
{
    public static Monster[] All = new Monster[]
    {
        new Monster
        {
            Name = "Dipple", // weak
            TotalHealth = 20,
            Health = 20,
            Strength = 10,
            Toughness = 3,
            Speed = 1,
            Cost = 1,
        },
        new Monster
        {
            Name = "Splame",
            TotalHealth = 30,
            Health = 30,
            Strength = 12,
            Toughness = 1,
            Speed = 1,
            Cost = 1,    
        },
        new Monster
        {
            Name = "Ragki", // fast
            TotalHealth = 25,
            Health = 25,
            Strength = 7,
            Toughness = 2,
            Speed = 10,
            Cost = 3,
        },
        new Monster
        {
            Name = "Hydrapace", // tank
            TotalHealth = 40,
            Health = 40,
            Strength = 16,
            Toughness = 5,
            Speed = 4,
            Cost = 5
        },
        new Monster
        {
            Name = "Culiwing", // mosquito
            TotalHealth = 100,
            Health = 100,
            Strength = 10,
            Toughness = 4,
            Speed = 12,
            Cost = 4,
        }
    };
}