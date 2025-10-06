using Labb2_DungeonCrawler.LevelElements;

namespace Labb2_DungeonCrawler.Utilities;

public static class CalculateDistance
{

    public static int Between(Position pos1, Position pos2) 
    { 
        // https://wumbo.net/formulas/distance-between-two-points-2d/ 
        (pos2.YPos - pos1.YPos) + (pos2.XPos - pos1.XPos);
    }
}
