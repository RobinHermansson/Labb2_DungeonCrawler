namespace Labb2_DungeonCrawler.App.LevelElements;

public class EmptySpace : LevelElement
{
    public EmptySpace(Position pos, char representation, ConsoleColor color) : base(pos, representation, color)
    {
    }

    public override void UpdateColor()
    {
        throw new NotImplementedException();
    }
}
