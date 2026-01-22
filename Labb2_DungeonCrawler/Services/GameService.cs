using DungeonCrawler.Domain.Entities;
using DungeonCrawler.Domain.Interfaces;
using DungeonCrawler.Infrastructure.Disc;
using System.Diagnostics;

namespace DungeonCrawler.App.Services;
public class GameService
{
    private readonly DiscRepository _discRepository;
    private readonly IEnemyRepository _enemyRepository;    
    public GameService(DiscRepository diskRepository, IEnemyRepository enemyRepository)
    {
        _discRepository = diskRepository;
        _enemyRepository = enemyRepository;
    }
    
    public async Task<GameState> CreateNewGameAsync()
    {
        var gameState = new GameState();
        
        var levelElements = await _discRepository.LoadLevelElementsAsync(1);




        
        // Just as a test, attempting to load in stuff to the DB...
        /*
        foreach (var item in levelElements)
        {

            //_characterRepository.AddAsync
            if (item is Enemy enemy)
            {
                Debug.WriteLine(enemy.Name);
                await  _enemyRepository.AddAsync(enemy);
            }
        }
        */
        
        foreach (var element in levelElements)
        {
            gameState.AddElement(element);
        }
        
        return gameState;
    }
}