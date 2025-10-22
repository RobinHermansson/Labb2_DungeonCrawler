namespace Labb2_DungeonCrawler.LevelElements;

public class Wall : LevelElement
{
    public Wall(Position pos, char representation, ConsoleColor color) : base(pos, representation, color)
    {
    }
    
    // Update color based on current visibility state
    public override void UpdateColor()
    {
        if (this.IsVisible)
        {
            this.Color = ConsoleColor.DarkYellow;
        }
        else if (this.HasBeenSeen && !this.IsVisible)
        {
            this.Color = ConsoleColor.DarkGray;
        }
    }
}
