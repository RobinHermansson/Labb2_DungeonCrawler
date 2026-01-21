namespace DungeonCrawler.Domain.Entities;

public class Dice
{
    public int Sides { get; set; } = 6;

    public int Roll()
    {
        return new Random().Next(1, Sides + 1);
    }
}
