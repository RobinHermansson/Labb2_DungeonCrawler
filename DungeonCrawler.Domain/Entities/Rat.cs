using DungeonCrawler.Domain.Utilities;
using DungeonCrawler.Domain.ValueObjects;

namespace DungeonCrawler.Domain.Entities;

public class Rat : Enemy
{
    private static Random _random = new Random();
    public int MaxMoveAttempts = 10;
    public Rat(Position pos, char representation, ConsoleColor color) : base(pos, representation, color)
    {
        Name = "Ratty";
        HitPoints = 20;
        AttackDiceCount = 2;
        DefenceDiceCount = 2;
        AttackModifier = 1;
        DefenceModifier = 0;
        AttackDice = new List<Dice>();
        DefenceDice = new List<Dice>();

        if (AttackDiceCount == 1)
        {
            AttackDice.Add(new Dice());
        }
        else
        {
            for (int i = 0; i < AttackDiceCount; i++)
            {
                AttackDice.Add(new Dice());
            }
        }
        if (DefenceDiceCount == 1)
        {
            DefenceDice.Add(new Dice());
        }
        else
        {
            for (int i = 0; i < DefenceDiceCount; i++)
            {
                DefenceDice.Add(new Dice());
            }
        }
    }

    public override void Update()
    {
        Random ratRandom = new Random();

        for (int attempts = 0; attempts < MaxMoveAttempts; attempts++)
        {
            int stepInCardinalDirection = _random.Next(0, 4);
            Position attempt = DirectionTransformer.GetPositionDelta((Direction)stepInCardinalDirection) + Position;

            if (attempt.XPos == GameState.Player.Position.XPos &&
                attempt.YPos == GameState.Player.Position.YPos)
            {
                GameState.FightHappened = true;
                return;
            }
            else if (AttemptMove(attempt, GameState))
            {
                MoveTo(attempt);
                return;
            }
        }
        // Rat stays in place if none of the above were possible.

    }
    public override void UpdateColor()
    {
        if (IsVisible)
        {
            Color = ConsoleColor.Magenta;
        }
        else if (HasBeenSeen && !IsVisible)
        {
            Color = ConsoleColor.Black;
        }
        else
        {
            Color = ConsoleColor.Black;
        }
    }


}
