using Labb2_DungeonCrawler.CharacterClasses;
using Labb2_DungeonCrawler.Core;
using Labb2_DungeonCrawler.Features;
using Labb2_DungeonCrawler.Utilities;
namespace Labb2_DungeonCrawler.LevelElements;


public class Player : Character
{
    public Player(string name, Position pos, char representation, ConsoleColor color, ICharacterClass characterClass) : base(pos, representation, color)
    {
        Name = name;
        Class = characterClass;
        IsPlayer = true;
        VisionRange = 5;

        BaseHitPoints = 80; 
        HitPoints = TotalHitPoints;

        BaseAttackModifier = 1;
        BaseDefenseModifier = 1;
        AttackModifier = TotalAttackModifier;
        DefenceModifier = TotalDefenseModifier;
        
        AttackDiceCount = 2;
        DefenceDiceCount = 2;
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
