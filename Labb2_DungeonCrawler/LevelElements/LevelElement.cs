namespace Labb2_DungeonCrawler.LevelElements;

public abstract class LevelElement
{

    private Position _position;
    public Position Position 
    { 
        get => _position;
        set 
        {
            PreviousPosition = Position;
            _position = value;
            // Raise event when position changes
            OnPositionChange(_position);
        }
    }
    public Position PreviousPosition { get; set; } 
    public char RepresentationAsChar { get; set; }
    public int VisionRange { get; set; }
    public bool isVisible { get; set; }

    public bool hasBeenSeen { get; set; }
    public ConsoleColor Color { get; set; }

    public EventHandler<Position> PositionChanged;

    public virtual void OnPositionChange(Position newPosition)
    {
        PositionChanged?.Invoke(this, newPosition);
    }

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
