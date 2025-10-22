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

    public bool PreviouslyVisible { get; set; }
    private bool _isVisible = false;
    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            PreviouslyVisible = _isVisible;
            _isVisible = value;
            if (PreviouslyVisible != _isVisible)
            {
                OnVisibilityChange(_isVisible);
            }
        }
    }


    public bool PreviouslyHasBeenSeen { get; set; }
    private bool _hasBeenSeen = false;
    public bool HasBeenSeen
    {
        get => _hasBeenSeen;
        set 
        {
            PreviouslyHasBeenSeen = _hasBeenSeen;
            _hasBeenSeen = value;
            if (PreviouslyHasBeenSeen != _hasBeenSeen)
            {
                OnHasBeenSeenChange(_hasBeenSeen);
            }
        }
    }
    public ConsoleColor Color { get; set; }

    public EventHandler<Position> PositionChanged;
    public EventHandler<bool> VisibilityChanged;
    public EventHandler<bool> HasBeenSeenChanged;
    
    public virtual void OnVisibilityChange(bool newState)
    {

        VisibilityChanged?.Invoke(this, newState);
        UpdateColor();
    }

    public virtual void OnHasBeenSeenChange(bool newState)
    {

        HasBeenSeenChanged?.Invoke(this, newState);
        UpdateColor();
    }
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

    public abstract void UpdateColor();
        

    public override string ToString()
    {
        return $"PreviousPosition: X:{this.PreviousPosition.XPos}, Y:{this.PreviousPosition.YPos}\nPosition: X:{this.Position.XPos},Y:{this.Position.YPos}";
    }
}
