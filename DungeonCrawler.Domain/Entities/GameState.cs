using DungeonCrawler.Domain.ValueObjects;

namespace DungeonCrawler.Domain.Entities;

public class GameState
{
    public bool Debug { get; set; } = false;

    public List<LevelElement> AllElements { get; set; } = new();
    public List<Enemy> Enemies { get; set; } = new List<Enemy>();
    public List<Wall> Walls { get; set; } = new List<Wall>();
    public Dictionary<Position, LevelElement> EntitiesDict { get; set; } = new();

    public Player? Player { get; set; } = null;
    public string PlayerName { get; set; }
    public int Turn { get; set; }
    public int LevelNumber { get; set; } = 1; // defaulting to first level.
    public int SlotNumber { get; set; }

    public bool FightIsHappening { get; set; } = false;
    public bool FightHappened { get; set; } = false;
    public MessageLog MessageLog { get; set; } = new();


    public GameState()
    {
    }

    public bool IsPositionWalkable(Position destination)
    {
        foreach (var element in AllElements)
        {
            if (element.Position.XPos == destination.XPos && element.Position.YPos == destination.YPos)
            {
                return false;
            }
        }
        return true;
    }

    public void AddElement(LevelElement element)
    {
        AllElements.Add(element);
        EntitiesDict[element.Position] = element;

        // Populate type-specific collections
        switch (element)
        {
            case Player player:
                Player = player;
                break;
            case Enemy enemy:
                Enemies.Add(enemy);
                break;
            case Wall wall:
                Walls.Add(wall);
                break;
        }
    }
}
