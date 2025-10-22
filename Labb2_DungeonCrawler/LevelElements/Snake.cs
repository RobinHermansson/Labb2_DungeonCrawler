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
    public override void CheckSurrounding(List<LevelElement> surroundingElements)
    {
        var nearbyElements = surroundingElements.Where(element => 
            Math.Abs(element.Position.XPos - this.Position.XPos) <= VisionRange &&
            Math.Abs(element.Position.YPos - this.Position.YPos) <= VisionRange
        );

        foreach (LevelElement element in nearbyElements)
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

    public override void UpdateColor()
    {
        if (this.IsVisible)
        {
            this.Color = ConsoleColor.Green;
        }
        else if (this.HasBeenSeen && !this.IsVisible)
        {
            this.Color = ConsoleColor.Black;
        }
        else
        {
            this.Color = ConsoleColor.Black; 
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
                // Sometimes this must fail, because otherwise the snakes can NOT be caught.
                var failChance = new Random().Next(0, 4);
                if (failChance == 3)
                {
                    return;                 
                }
                else
                {
                    MoveTo(bestMove);

                }
            }
        }
    }
}
