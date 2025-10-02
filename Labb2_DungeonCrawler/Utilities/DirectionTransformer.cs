using Labb2_DungeonCrawler.Core;
using Labb2_DungeonCrawler.LevelElements;
namespace Labb2_DungeonCrawler.Utilities
{
    public static class DirectionTransformer
    {
        public static Position GetPositionDelta(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Position(0, -1);
                case Direction.Down:
                    return new Position(0, 1);
                case Direction.Left:
                    return new Position(-1, 0);
                case Direction.Right:
                    return new Position(1, 0);
                default:
                    throw new ArgumentException($"Unhandled direction: {direction}");
            }

        }
    }
}
