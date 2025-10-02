using Labb2_DungeonCrawler.Core;
using Labb2_DungeonCrawler.Features;
using Labb2_DungeonCrawler.Utilities;
namespace Labb2_DungeonCrawler.LevelElements

{
    public class Player : LevelElement, IMovable
    {
        public string Name { get; } = "Player";
        public int HitPoints { get; } = 100;
        public int AttackDiceCount { get; }
        public int DefenceDiceCount { get; }
        public int AttackModifier { get; }
        public int DefenceModifiier { get; }

        public Dice AttackDice { get; }
        public Dice DefenceDice { get; }

        public void Update()
        {
        }

        public bool AttemptMove(Position attempt, GameState gameState)
        {
            return gameState.IsPositionWalkable(attempt) ? true : false;
        }

        public void MoveTo(Position position)
        {
            this.Position = position;

        }
        public Position MovementHandler(ConsoleKeyInfo input)
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
                    throw new ArgumentException("Could not handle the input key.");
            }

        }
        public Player(string name, Position pos, char representation, ConsoleColor color) : base(pos, representation, color)
        {
            Name = name;
        }
    }
}
