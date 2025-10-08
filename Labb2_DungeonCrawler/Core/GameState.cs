using Labb2_DungeonCrawler.LevelElements;
using Labb2_DungeonCrawler.LevelHandling;

namespace Labb2_DungeonCrawler.Core;

public class GameState
{

    public LevelData LevelData = new LevelData();

    public Player Player = null;

    public bool FightIsHappening = false;

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
