using Labb2_DungeonCrawler.LevelElements;
namespace Labb2_DungeonCrawler.Events;

public class LevelElementChangedEventArgs : EventArgs
{
    public Position OldPosition { get; }
    public Position NewPosition { get; }
    public bool VisibilityChanged { get; }
    public bool IsAliveChanged { get; }
    
    // Constructor for position changes
    public LevelElementChangedEventArgs(Position oldPosition, Position newPosition)
    {
        OldPosition = oldPosition;
        NewPosition = newPosition;
        VisibilityChanged = false;
        IsAliveChanged = false;
    }
    
    // Constructor for visibility or state changes
    public LevelElementChangedEventArgs(bool visibilityChanged, bool isAliveChanged)
    {
        VisibilityChanged = visibilityChanged;
        IsAliveChanged = isAliveChanged;
        // Same position for non-movement changes
        //OldPosition = null;
        //NewPosition = null;
    }
}
