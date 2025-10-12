using Labb2_DungeonCrawler.LevelElements;
using Labb2_DungeonCrawler.LevelHandling;

namespace Labb2_DungeonCrawler.Core;

public class GameState
{
    public bool Debug { get; private set; } = false;

    public LevelData LevelData = new LevelData();
    public List<Enemy> Enemies = new List<Enemy>();
    public List<Wall> Walls = new List<Wall>();
    public Dictionary<Position, LevelElement> ElementsDict = new Dictionary<Position, LevelElement>();

    public Player Player = null;

    public bool FightIsHappening = false;

    public GameState()
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string path = Path.Combine(baseDirectory, "Levels", "Level1.txt");
        LevelData.LoadElementsFromFile(path);
        foreach (var element in LevelData.LevelElementsList)
        {
            ElementsDict.Add(element.Position, element);
            if (element is Character character)
            {
                character.GameState = this;
            }
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
