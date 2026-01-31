using DungeonCrawler.Domain.Entities;
using DungeonCrawler.Domain.Interfaces;

namespace DungeonCrawler.Infrastructure.Utilities;

public class PlayerClassSeeder
{
    private readonly IPlayerClassRepository _repository;
    
    public PlayerClassSeeder(IPlayerClassRepository repository)
    {
        _repository = repository;
    }
    
    public async Task SeedDefaultClassesAsync()
    {
        var existingClasses = await _repository.GetAllAsync();
        if (existingClasses.Any()) return; // Already seeded
        
        var defaultClasses = new List<PlayerClass>
        {
            new PlayerClass
            {
                Name = "Warrior",
                Description = "A strong fighter with high HP and attack",
                BaseHitPoints = 120,
                BaseAttackDiceCount = 3,
                BaseDefenceDiceCount = 2,
                BaseAttackModifier = 5,
                BaseDefenceModifier = 2,
                BaseVisionRange = 4,
                ClassColor = ConsoleColor.Red
            },
            new PlayerClass
            {
                Name = "Wizard",
                Description = "A magic user with powerful attacks but low defense",
                BaseHitPoints = 80,
                BaseAttackDiceCount = 2,
                BaseDefenceDiceCount = 1,
                BaseAttackModifier = 6,
                BaseDefenceModifier = 1,
                BaseVisionRange = 6,
                ClassColor = ConsoleColor.Blue
            },
            new PlayerClass
            {
                Name = "Thief",
                Description = "A nimble rogue with high vision and defense",
                BaseHitPoints = 90,
                BaseAttackDiceCount = 2,
                BaseDefenceDiceCount = 3,
                BaseAttackModifier = 3,
                BaseDefenceModifier = 4,
                BaseVisionRange = 7,
                ClassColor = ConsoleColor.Green
            },
            new PlayerClass
            {
                Name = "Priest",
                Description = "A healer with balanced stats",
                BaseHitPoints = 100,
                BaseAttackDiceCount = 2,
                BaseDefenceDiceCount = 2,
                BaseAttackModifier = 3,
                BaseDefenceModifier = 3,
                BaseVisionRange = 5,
                ClassColor = ConsoleColor.White
            }
        };
        
        foreach (var playerClass in defaultClasses)
        {
            await _repository.AddAsync(playerClass);
        }
    }
}