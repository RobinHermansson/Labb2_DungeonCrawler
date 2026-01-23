using DungeonCrawler.Domain.Entities;
using DungeonCrawler.Domain.Interfaces;
using DungeonCrawler.Infrastructure.Disc;
using System.Diagnostics;

namespace DungeonCrawler.App.Services;
public class GameService
{
    private readonly DiscRepository _discRepository;
    private readonly ILevelTemplateRepository _levelTemplateRepository;
    private readonly IEnemyRepository _enemyRepository;    
    public GameService(DiscRepository diskRepository, IEnemyRepository enemyRepository, ILevelTemplateRepository levelTemplateRepo)
    {
        _discRepository = diskRepository;
        _enemyRepository = enemyRepository;
        _levelTemplateRepository = levelTemplateRepo;
    }
    
    public async Task<GameState> CreateNewGameAsync()
    {
        //var gameState = new GameState();
        
        var levelElements = await _discRepository.LoadLevelElementsAsync(1);

        var levelTemplate = await _levelTemplateRepository.GetByLevelNumberAsync(1);

        var gameState = levelTemplate.CreateInitialGameState();
                
        return gameState;


      
                
    }
}