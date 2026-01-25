using DungeonCrawler.Domain.Utilities;
using DungeonCrawler.Domain.ValueObjects;

namespace DungeonCrawler.Domain.Entities;

public class Player : Character
{
    public PlayerClass? Class { get; set; } // Reference to the class

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

        InitializeDice();    
    }

    // Constructor that uses the new PlayerClass
    public Player(string name, Position pos, char representation, PlayerClass playerClass)
        : base(pos, representation, playerClass.ClassColor)
    {
        Name = name;
        IsPlayer = true;
        Class = playerClass;

        VisionRange = playerClass.BaseVisionRange;
        HitPoints = playerClass.BaseHitPoints;
        AttackDiceCount = playerClass.BaseAttackDiceCount;
        DefenceDiceCount = playerClass.BaseDefenceDiceCount;
        AttackModifier = playerClass.BaseAttackModifier;
        DefenceModifier = playerClass.BaseDefenceModifier;

        InitializeDice();
    }
    public void ApplyPlayerClassStats(PlayerClass playerClass)
    {
        Class = playerClass;
        VisionRange = playerClass.BaseVisionRange;
        HitPoints = playerClass.BaseHitPoints;
        AttackDiceCount = playerClass.BaseAttackDiceCount;
        DefenceDiceCount = playerClass.BaseDefenceDiceCount;
        AttackModifier = playerClass.BaseAttackModifier;
        DefenceModifier = playerClass.BaseDefenceModifier;
        
        // Reinitialize dice with new counts
        InitializeDice();
    }
    public void ApplyPlayerClassStatsPreserveHP(PlayerClass playerClass)
    {
        Class = playerClass;
        VisionRange = playerClass.BaseVisionRange;
        // Don't overwrite HitPoints - it's already loaded from save
        AttackDiceCount = playerClass.BaseAttackDiceCount;
        DefenceDiceCount = playerClass.BaseDefenceDiceCount;
        AttackModifier = playerClass.BaseAttackModifier;
        DefenceModifier = playerClass.BaseDefenceModifier;
        
        InitializeDice();
    }

    private void InitializeDice()
    {
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
