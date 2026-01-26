using DungeonCrawler.Domain.Entities;
using DungeonCrawler.Domain.Interfaces;

namespace DungeonCrawler.App.Services;
public class GameService
{
    private readonly ILevelTemplateRepository _levelTemplateRepository;
    private ISaveGameRepository _saveGameRepository;
    public GameService(ILevelTemplateRepository levelTemplateRepo, ISaveGameRepository saveGameRepository)
    {
        _levelTemplateRepository = levelTemplateRepo;
        _saveGameRepository = saveGameRepository;
    }
    
    public async Task<GameState> CreateNewGameAsync(string playerName, PlayerClass playerClass)
    {
        
        var levelTemplate = await _levelTemplateRepository.GetByLevelNumberAsync(1);

        var gameState = levelTemplate.CreateInitialGameState();
        
        gameState.Player.ApplyPlayerClassStats(playerClass);
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