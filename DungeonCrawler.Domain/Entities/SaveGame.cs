using DungeonCrawler.Domain.Interfaces;

namespace DungeonCrawler.Domain.Entities;

public class SaveGame : IHasId<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int SlotNumber { get; set; }
    public int LevelNumber { get; set; }
    public int Turn { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public DateTime LastPlayedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public MessageLog MessageLog { get; set; }

    public bool Debug { get; set; }
    

    public List<LevelElement> AllElements { get; set; } = new List<LevelElement>();

    public static SaveGame FromGameState(GameState gameState, int slotNumber)
    {
        return new SaveGame
        {
            SlotNumber = slotNumber,
            LevelNumber = gameState.LevelNumber,
            PlayerName = gameState.PlayerName,
            CreatedAt = DateTime.UtcNow,
            LastPlayedAt = DateTime.UtcNow,
            Turn = gameState.Turn,
            Debug = gameState.Debug,
            AllElements = new List<LevelElement>(gameState.AllElements),
            MessageLog = gameState.MessageLog
        };
    }
    
    public GameState ToGameState()
    {
        var gameState = new GameState
        {
            Debug = false
        };

        gameState.Turn = Turn;
        gameState.PlayerName = PlayerName;
        gameState.MessageLog = MessageLog;
        
        foreach (var element in AllElements)
        {
            gameState.AddElement(element);
            
            if (element is Character character)
            {
                character.GameState = gameState;
            }
        }
        
        return gameState;
    }
}
