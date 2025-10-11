using Labb2_DungeonCrawler.Core;
using Labb2_DungeonCrawler.Utilities;
using Labb2_DungeonCrawler.Features;
namespace Labb2_DungeonCrawler.LevelElements;

public class Rat : Enemy
{
    private static Random _random = new Random();
    private int _maxMoveAttempts = 10;
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
            for (int i = 1; i < AttackDiceCount; i++)
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
            for (int i = 1; i < DefenceDiceCount; i++)
            {
                DefenceDice.Add(new Dice());
            }
        }
    }

    public override void Update()
    {
        Random ratRandom = new Random();

        for (int attempts = 0; attempts < _maxMoveAttempts; attempts++)
        {
            int stepInCardinalDirection = _random.Next(0, 4);
            Position attempt = DirectionTransformer.GetPositionDelta((Direction)stepInCardinalDirection) + Position;
            
            if (attempt.XPos == GameState.Player.Position.XPos && 
                attempt.YPos == GameState.Player.Position.YPos)
            {
                Combat combat = new Combat(this, GameState.Player);
                combat.StartCombat();
                return;             
            }
            else if (AttemptMove(attempt, GameState))
            {
                MoveMe((Direction)stepInCardinalDirection);
                return;            
            }
        }
        // Rat stays in place if none of the above were possible.

    }

}
