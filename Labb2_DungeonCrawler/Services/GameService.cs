using DungeonCrawler.Domain.Entities;
using DungeonCrawler.Infrastructure.Disc;

namespace DungeonCrawler.App.Services;
public class GameService
{
    private readonly DiscRepository _discRepository;
    
    public GameService(DiscRepository diskRepository)
    {
        _discRepository = diskRepository;
    }
    
    public async Task<GameState> CreateNewGameAsync()
    {
        var gameState = new GameState();
        
        var levelElements = await _discRepository.LoadLevelElementsAsync(1);
        
        foreach (var element in levelElements)
        {
            gameState.AddElement(element);
        }
        
        return gameState;
    }
}