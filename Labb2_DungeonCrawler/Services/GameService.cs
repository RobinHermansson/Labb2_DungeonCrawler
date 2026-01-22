using DungeonCrawler.Domain.Entities;
using DungeonCrawler.Domain.Interfaces;
using DungeonCrawler.Infrastructure.Disc;

namespace DungeonCrawler.App.Services;
public class GameService
{
    private readonly DiscRepository _discRepository;
    private readonly ICharacterRepository _characterRepository;
    
    public GameService(DiscRepository diskRepository, ICharacterRepository characterRepository)
    {
        _discRepository = diskRepository;
        _characterRepository = characterRepository;
    }
    
    public async Task<GameState> CreateNewGameAsync()
    {
        var gameState = new GameState();
        
        var levelElements = await _discRepository.LoadLevelElementsAsync(1);
        
        // Just as a test, attempting to load in stuff to the DB...


        
        
        foreach (var element in levelElements)
        {
            gameState.AddElement(element);
        }
        
        return gameState;
    }
}