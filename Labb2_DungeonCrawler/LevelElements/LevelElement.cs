using Labb2_DungeonCrawler.Events;
namespace Labb2_DungeonCrawler.LevelElements;

public abstract class LevelElement
{
    // The event that will notify subscribers when this element changes
    public event EventHandler<LevelElementChangedEventArgs> ElementChanged;
    
    // Protected method to raise the event
    protected virtual void OnElementChanged(LevelElementChangedEventArgs e)
    {
        // Make a local copy to avoid race conditions
        EventHandler<LevelElementChangedEventArgs> handler = ElementChanged;
        handler?.Invoke(this, e);
    }
    private Position _position;
    public Position Position 
    {
        get
        { return _position; }
        set
        {
            if (_position != value)
            {
                Position oldPosition = _position;
                _position = value;
                //OnElementChanged(new LevelElementChangedEventArgs(oldPosition, value));

            }
        } 
    }
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

    public void Draw()
    {
        Console.SetCursorPosition(this.Position.XPos, this.Position.YPos);
        Console.ForegroundColor = this.Color;
        Console.Write(this.RepresentationAsChar);
        Console.ResetColor();
    }

    public override string ToString()
    {
        return $"PreviousPosition: X:{this.PreviousPosition.XPos}, Y:{this.PreviousPosition.YPos}\nPosition: X:{this.Position.XPos},Y:{this.Position.YPos}";
    }
}
