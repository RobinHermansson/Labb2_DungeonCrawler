using DungeonCrawler.Domain.Interfaces;
namespace DungeonCrawler.Domain.Entities;
public class PlayerClass : IHasId<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty; // "Warrior", "Wizard", "Thief", "Priest"
    public string Description { get; set; } = string.Empty;
    
    public int BaseHitPoints { get; set; } = 100;
    public int BaseAttackDiceCount { get; set; } = 2;
    public int BaseDefenceDiceCount { get; set; } = 2;
    public int BaseAttackModifier { get; set; } = 3;
    public int BaseDefenceModifier { get; set; } = 3;
    public int BaseVisionRange { get; set; } = 5;
    
    public ConsoleColor ClassColor { get; set; } = ConsoleColor.Yellow;
}