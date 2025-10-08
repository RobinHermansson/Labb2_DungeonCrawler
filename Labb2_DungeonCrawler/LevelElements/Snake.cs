using Labb2_DungeonCrawler.Core;
using Labb2_DungeonCrawler.Utilities;
using Labb2_DungeonCrawler.Features;

namespace Labb2_DungeonCrawler.LevelElements;

public class Snake : Enemy
{
    public bool IsScared { get; set; } = false;

    public Snake(Position pos, char representation, ConsoleColor color) : base(pos, representation, color)
    {

        Name = "Snekk";
        HitPoints = 20;
        AttackDiceCount = 3;
        DefenceDiceCount = 1;
        AttackModifier = 1;
        DefenceModifier = 2;
        AttackDice = new List<Dice>();
        DefenceDice = new List<Dice>();
        VisionRange = 3;
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
    public void CheckSurrounding(List<LevelElement> surroundingElements)
    {
        foreach (LevelElement element in surroundingElements)
        {
            var distance = CalculateDistance.Between(this.Position, element.Position);
            if (distance < this.VisionRange)
            {
                if (element is Player)
                {
                    IsScared = true;
                    break;
                }
                else
                {
                    IsScared = false;
                }
            }
        }
    }


    public override void Update()
    {
        CheckSurrounding(GameState.LevelData.LevelElementsList);
        if (IsScared)
        {
            Position[] possibleMoves = new Position[4] {
                (DirectionTransformer.GetPositionDelta(Direction.Up) + Position ),
                (DirectionTransformer.GetPositionDelta(Direction.Left) + Position ),
                (DirectionTransformer.GetPositionDelta(Direction.Right) + Position ),
                (DirectionTransformer.GetPositionDelta(Direction.Down) + Position )
            };

            Position bestMove = new Position();
            double highestDistance = 0;
            foreach (var possibleMove in possibleMoves)
            {
                double currentCheck = CalculateDistance.Between(possibleMove, GameState.Player.Position);
                if ( currentCheck > highestDistance )
                {
                    if (AttemptMove(possibleMove, GameState))
                    {
                        highestDistance = currentCheck;
                        bestMove = possibleMove;
                    }
                }
            }

            if (AttemptMove(bestMove, GameState))
            {
                MoveTo(bestMove);
            }
        }
    }
}
