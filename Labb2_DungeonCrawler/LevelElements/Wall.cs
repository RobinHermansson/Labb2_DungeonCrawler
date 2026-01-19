namespace Labb2_DungeonCrawler.App.LevelElements;

public class Wall : LevelElement
{
    public Wall(Position pos, char representation, ConsoleColor color) : base(pos, representation, color)
    {
    }
    
    // Update color based on current visibility state
    public override void UpdateColor()
    {
        if (IsVisible)
        {
            Color = ConsoleColor.DarkYellow;
        }
        else if (HasBeenSeen && !IsVisible)
        {
            Color = ConsoleColor.DarkGray;
        }
    }
}
