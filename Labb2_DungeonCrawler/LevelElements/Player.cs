using Labb2_DungeonCrawler.Core;
using Labb2_DungeonCrawler.Features;
using Labb2_DungeonCrawler.Utilities;
namespace Labb2_DungeonCrawler.LevelElements;


public class Player : Character
{
    public Player(string name, Position pos, char representation, ConsoleColor color) : base(pos, representation, color)
    {
        Name = name;
        IsPlayer = true;
        VisionRange = 5;
        HitPoints = 100;
        AttackDiceCount = 2;
        DefenceDiceCount = 2;
        AttackModifier = 3;
        DefenceModifier = 3;
        AttackDice = new List<Dice>();
        DefenceDice = new List<Dice>();

        for (int i = 0; i < AttackDiceCount; i++)
        {
            AttackDice.Add(new Dice());
        }
        for (int i = 0; i < DefenceDiceCount; i++)
        {
            DefenceDice.Add(new Dice());
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
