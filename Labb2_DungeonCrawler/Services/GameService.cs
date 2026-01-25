using DungeonCrawler.Domain.Entities;
using DungeonCrawler.Domain.Interfaces;
using DungeonCrawler.Infrastructure.Disc;

namespace DungeonCrawler.App.Services;
public class GameService
{
    private readonly DiscRepository _discRepository;
    private readonly ILevelTemplateRepository _levelTemplateRepository;
    private ISaveGameRepository _saveGameRepository;
    private readonly IEnemyRepository _enemyRepository;    
    public GameService(DiscRepository diskRepository, IEnemyRepository enemyRepository, ILevelTemplateRepository levelTemplateRepo, ISaveGameRepository saveGameRepository)
    {
        _discRepository = diskRepository;
        _enemyRepository = enemyRepository;
        _levelTemplateRepository = levelTemplateRepo;
        _saveGameRepository = saveGameRepository;
    }
    
    public async Task<GameState> CreateNewGameAsync(string playerName, PlayerClass playerClass)
    {
        
        var levelElements = await _discRepository.LoadLevelElementsAsync(1);

        var levelTemplate = await _levelTemplateRepository.GetByLevelNumberAsync(1);

        var gameState = levelTemplate.CreateInitialGameState();
        
        gameState.Player.Class = playerClass;
        gameState.Player.Name = playerName;
        gameState.PlayerName = playerName;
                       
        return gameState;
           
                
    }

    public async Task SaveGameAsync(GameState gameState, int slotNumber)
    {
        var existingSave = await _saveGameRepository.GetBySlotNumberAsync(slotNumber);
        if (existingSave is null)
        {
            await _saveGameRepository.CreateSaveGameAsync(gameState, slotNumber);
            return;
        }
        
        var updatedSave = SaveGame.FromGameState(gameState, slotNumber);
        updatedSave.Id = existingSave.Id;
        updatedSave.CreatedAt = existingSave.CreatedAt;        
        updatedSave.LastPlayedAt = DateTime.UtcNow;        
        await _saveGameRepository.SaveToSlotNumberAsync(updatedSave, slotNumber);
    }
}