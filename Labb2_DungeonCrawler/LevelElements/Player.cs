using Labb2_DungeonCrawler.Core;
using Labb2_DungeonCrawler.Features;
using Labb2_DungeonCrawler.Utilities;
using System.ComponentModel.Design;
namespace Labb2_DungeonCrawler.LevelElements

{
    public class Player : LevelElement, IMovable, IFighter
    {
        public string Name { get; set; } = "Player";
        public int HitPoints { get; set; } = 100;
        public int AttackDiceCount { get; } = 2;
        public int DefenceDiceCount { get; } = 2;
        public int AttackModifier { get; } = 3;
        public int DefenceModifier { get; } = 3;

        public List<Dice> AttackDice { get; } = new List<Dice>();
        public List<Dice> DefenceDice { get; } = new List<Dice>();
        public GameState GameState { get; set; }
        
        public Player(string name, Position pos, char representation, ConsoleColor color) : base(pos, representation, color)
        {
            Name = name;
            for (int i = 1; i < AttackDiceCount; i++)
            {
                AttackDice.Add(new Dice());
            }
            for (int i = 1; i < DefenceDiceCount; i++)
            {
                DefenceDice.Add(new Dice());
            }
        }
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
        public void MoveMe(Direction direction)
        {

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

        public bool Attack(IFighter target)
        {
            int attackRollTotal = 0;
            int defenceRollTotal = 0;
            foreach(Dice dice in AttackDice)
            {
                attackRollTotal += dice.Roll();
                Console.WriteLine($"{Name} rolls, and the attacktotal is: {attackRollTotal}");
            }
            foreach(Dice dice in target.DefenceDice)
            {
                defenceRollTotal += dice.Roll();
                Console.WriteLine($"{target.Name} rolls, and the defencetotal is: {defenceRollTotal}");
            }
            if ((attackRollTotal + AttackModifier) > (defenceRollTotal + target.DefenceModifier))
            {
                int totalDamageTaken = (attackRollTotal + AttackModifier) - (defenceRollTotal + target.DefenceModifier);
                Console.WriteLine($"{target.Name} is about to take {totalDamageTaken}!");
                target.TakeDamage(totalDamageTaken);
                return true;
            }
            else
            {
                return false;
            }
                
            
        }

        public void TakeDamage(int damage)
        {
            HitPoints -= damage;
        }

        public bool IsAlive()
        {
            return HitPoints > 0;
        }

        
    }
}
