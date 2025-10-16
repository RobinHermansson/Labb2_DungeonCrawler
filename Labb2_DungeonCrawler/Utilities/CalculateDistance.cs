using Labb2_DungeonCrawler.LevelElements;

namespace Labb2_DungeonCrawler.Utilities;

public static class CalculateDistance
{
    public static double Between(Position pos1, Position pos2)
    {
        double ypos = Math.Pow((pos2.YPos - pos1.YPos), 2);
        double xpos = Math.Pow((pos2.XPos - pos1.XPos), 2);

        double sum = xpos + ypos;
        double result = Math.Sqrt(sum);
        return result;
    }
}
