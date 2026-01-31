using DungeonCrawler.Domain.Interfaces;
using DungeonCrawler.Domain.ValueObjects;

namespace DungeonCrawler.Domain.Entities;

public class LevelTemplate : IHasId<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "Level";
    public int LevelNumber { get; set; }

    public List<LevelElement> InitialElements { get; set; } = new();

    public GameState CreateInitialGameState()
    {
        var gameState = new GameState();
        
        foreach (var element in InitialElements)
        {
            // Create a fresh copy of each element (new IDs, reset state)
            LevelElement? newElement = element switch
            {
                Wall wall => new Wall(
                    new Position(wall.Position.XPos, wall.Position.YPos),
                    wall.RepresentationAsChar,
                    wall.Color),
                    
                Rat rat => new Rat(
                    new Position(rat.Position.XPos, rat.Position.YPos),
                    rat.RepresentationAsChar,
                    rat.Color),
                    
                Snake snake => new Snake(
                    new Position(snake.Position.XPos, snake.Position.YPos),
                    snake.RepresentationAsChar,
                    snake.Color),
                    
                Player player => new Player(
                    player.Name,
                    new Position(player.Position.XPos, player.Position.YPos),
                    player.RepresentationAsChar,
                    player.Color),
                    
                _ => null
            };
            
            if (newElement != null)
            {
                gameState.AddElement(newElement);
                
                // Set GameState reference for characters
                if (newElement is Character character)
                {
                    character.GameState = gameState;
                }
            }
        }
        
        return gameState;
    }

}
