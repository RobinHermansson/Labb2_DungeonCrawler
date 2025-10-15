namespace Labb2_DungeonCrawler.LevelElements;

public abstract class LevelElement
{

    public Position Position { get; set; }
    public Position PreviousPosition { get; set; }
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

    public override string ToString()
    {
        return $"PreviousPosition: X:{this.PreviousPosition.XPos}, Y:{this.PreviousPosition.YPos}\nPosition: X:{this.Position.XPos},Y:{this.Position.YPos}";
    }
}
