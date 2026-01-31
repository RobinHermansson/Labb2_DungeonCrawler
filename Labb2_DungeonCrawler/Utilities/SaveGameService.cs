using DungeonCrawler.Domain.Entities;
using DungeonCrawler.Domain.Interfaces;

namespace DungeonCrawler.App.Services;

public class SaveGameService
{
    private readonly ISaveGameRepository _saveGameRepository;
    private readonly IPlayerClassRepository _playerClassRepository;
    
    public SaveGameService(
        ISaveGameRepository saveGameRepository,
        IPlayerClassRepository playerClassRepository)
    {
        _saveGameRepository = saveGameRepository;
        _playerClassRepository = playerClassRepository;
    }
    
    public async Task<GameState> LoadGameAsync(SaveGame saveGame)
    {
        var gameState = saveGame.ToGameState();
        
        // Restore PlayerClass if it exists
        if (saveGame.PlayerClassId.HasValue)
        {
            var playerClass = await _playerClassRepository.GetByIdAsync(saveGame.PlayerClassId.Value);
            if (playerClass != null)
            {
                gameState.Player.ApplyPlayerClassStatsPreserveHP(playerClass);
            }
        }
        
        return gameState;
    }
    
    public async Task SaveGameAsync(GameState gameState, int slotNumber)
    {
        var existingSave = await _saveGameRepository.GetBySlotNumberAsync(slotNumber);
        
        var saveGame = SaveGame.FromGameState(gameState, slotNumber);
        
        if (existingSave != null)
        {
            // Update existing save
            saveGame.Id = existingSave.Id;
            saveGame.CreatedAt = existingSave.CreatedAt;
        }
        
        saveGame.LastPlayedAt = DateTime.UtcNow;
        await _saveGameRepository.SaveToSlotNumberAsync(saveGame, slotNumber);
    }
    
    public async Task DeleteSaveAsync(int slotNumber)
    {
        var save = await _saveGameRepository.GetBySlotNumberAsync(slotNumber);
        if (save != null)
        {
            await _saveGameRepository.RemoveAsync(save);
        }
    }
}