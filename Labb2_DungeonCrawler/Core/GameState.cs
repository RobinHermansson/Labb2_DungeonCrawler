using Labb2_DungeonCrawler.LevelElements;
using Labb2_DungeonCrawler.LevelHandling;

namespace Labb2_DungeonCrawler.Core;

public class GameState
{
    public bool Debug { get; private set; } = false;

    public LevelData LevelData = new LevelData();
    public List<Enemy> Enemies = new List<Enemy>();
    public List<Wall> Walls = new List<Wall>();
    public Dictionary<Position, LevelElement> EntitiesDict = new();

    public Player Player = null;

    public bool FightIsHappening = false;
    public bool FightHappened = false;


    public GameState()
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string path = Path.Combine(baseDirectory, "Levels", "Level1.txt");
        LevelData.LoadElementsFromFile(path);
        foreach (var element in LevelData.LevelElementsList)
        {
            if (element is Player player)
            {
                Player = player;
            }
            if (element is Enemy enemy)
            {
                Enemies.Add(enemy);
            }
            if (element is Wall wall) {
                Walls.Add(wall);
            }
            EntitiesDict[element.Position] = element;
        }
    }

    public bool IsPositionWalkable(Position destination)
    {
        foreach (var element in LevelData.LevelElementsList)
        {
            if (element.Position.XPos == destination.XPos && element.Position.YPos == destination.YPos)
            {
                return false;
            }
        }
        return true;
    }

}
