using Labb2_DungeonCrawler.App.Core;
using Labb2_DungeonCrawler.App.LevelElements;
namespace Labb2_DungeonCrawler.App.Utilities;

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
        CurrentObjectColor = AllElementsDict[startingPosition].Color;
        CurrentRepresentation = AllElementsDict[startingPosition].RepresentationAsChar;
    }

    public void Update()
    {

        //this.PreviousObjectPosition = Position;
        PreviousObjectPositionRepresentation = CurrentRepresentation;
        PreviousObjectPositionColor = CurrentObjectColor;

        var currentObject = GetObjectDataWhereStanding();
        CurrentRepresentation = currentObject.RepresentationAsChar;
        CurrentObjectColor = currentObject.Color;

    }

    public void MoveTo(Position position)
    {
        PreviousObjectPosition = new Position(Position.XPos, Position.YPos);
        Position = position;
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
                return DirectionTransformer.GetPositionDelta(Direction.Up) + Position;
            case ConsoleKey.S:
                return DirectionTransformer.GetPositionDelta(Direction.Down) + Position;
            case ConsoleKey.A:
                return DirectionTransformer.GetPositionDelta(Direction.Left) + Position;
            case ConsoleKey.D:
                return DirectionTransformer.GetPositionDelta(Direction.Right) + Position;
            case ConsoleKey.UpArrow:
                return DirectionTransformer.GetPositionDelta(Direction.Up) + Position;
            case ConsoleKey.DownArrow:
                return DirectionTransformer.GetPositionDelta(Direction.Down) + Position;
            case ConsoleKey.LeftArrow:
                return DirectionTransformer.GetPositionDelta(Direction.Left) + Position;
            case ConsoleKey.RightArrow:
                return DirectionTransformer.GetPositionDelta(Direction.Right) + Position;
            default:
                return null;
        }

    }
}
