using Labb2_DungeonCrawler.Core;
using Labb2_DungeonCrawler.LevelElements;
namespace Labb2_DungeonCrawler.Utilities;

public class DebugAssistant
{
    public char CurrentRepresentation { get; set; }
    public ConsoleColor CurrentObjectColor { get; set; }

    public ConsoleColor Color = ConsoleColor.Cyan;
    public Position Position { get; set; }
    public char PreviousObjectPositionRepresentation { get; set; }
    public ConsoleColor PreviousObjectPositionColor { get; set; }
    public Position PreviousObjectPosition { get; set; } = new Position(0, 0);

    public Dictionary<Position, LevelElement> AllElementsDict { get; set; }

    public DebugAssistant(Position startingPosition, Dictionary<Position, LevelElement> allLevelElements)
    {

        AllElementsDict = allLevelElements;
        Position = startingPosition;
        
        // Instantiate the debugger with the current object but also with a color and repr. to work from.
        this.CurrentObjectColor = AllElementsDict[startingPosition].Color;
        this.CurrentRepresentation = AllElementsDict[startingPosition].RepresentationAsChar;
    }

    public void Update()
    {

        //this.PreviousObjectPosition = Position;
        this.PreviousObjectPositionRepresentation = CurrentRepresentation;
        this.PreviousObjectPositionColor = CurrentObjectColor;

        var currentObject = GetObjectDataWhereStanding();
        this.CurrentRepresentation = currentObject.RepresentationAsChar;
        this.CurrentObjectColor = currentObject.Color;

    }

    public void MoveTo(Position position)
    {
        this.PreviousObjectPosition = new Position(Position.XPos, Position.YPos);
        this.Position = position;
        Update();
    }

    public bool IsObjectOnPosition()
    {
        if (AllElementsDict.TryGetValue(Position, out LevelElement p))
        {
            return true;
        }
        return false;
    }

    public LevelElement GetObjectDataWhereStanding()
    {
        if (IsObjectOnPosition()) {
            return AllElementsDict[Position];
        }
        else
        {
            return new EmptySpace(Position, 'O', ConsoleColor.Cyan);
        }
    }

    public Position? MovementHandler(ConsoleKeyInfo input)
    {
        switch (input.Key)
        {
            case ConsoleKey.W:
                return DirectionTransformer.GetPositionDelta(Direction.Up) + this.Position;
            case ConsoleKey.S:
                return DirectionTransformer.GetPositionDelta(Direction.Down) + this.Position;
            case ConsoleKey.A:
                return DirectionTransformer.GetPositionDelta(Direction.Left) + this.Position;
            case ConsoleKey.D:
                return DirectionTransformer.GetPositionDelta(Direction.Right) + this.Position;
            case ConsoleKey.UpArrow:
                return DirectionTransformer.GetPositionDelta(Direction.Up) + this.Position;
            case ConsoleKey.DownArrow:
                return DirectionTransformer.GetPositionDelta(Direction.Down) + this.Position;
            case ConsoleKey.LeftArrow:
                return DirectionTransformer.GetPositionDelta(Direction.Left) + this.Position;
            case ConsoleKey.RightArrow:
                return DirectionTransformer.GetPositionDelta(Direction.Right) + this.Position;
            default:
                return null;
        }

    }
}
