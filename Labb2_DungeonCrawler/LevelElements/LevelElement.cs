namespace Labb2_DungeonCrawler.LevelElements;

public abstract class LevelElement
{

    public Position Position { get; set; }
    public char RepresentationAsChar { get; set; }
    public int VisionRange { get; set; }
    public bool isVisible { get; set; } = false;

    public bool hasBeenSeen { get; set; } = false;
    public ConsoleColor Color { get; set; }

    public LevelElement(Position pos, char representation, ConsoleColor color)
    {
        Position = pos;
        RepresentationAsChar = representation;
        Color = color;
    }

    public void Draw()
    {
        Console.SetCursorPosition(this.Position.XPos, this.Position.YPos);
        Console.ForegroundColor = this.Color;
        Console.Write(this.RepresentationAsChar);
        Console.ResetColor();
    }
}
