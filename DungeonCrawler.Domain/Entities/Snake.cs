using DungeonCrawler.Domain.ValueObjects;
using DungeonCrawler.Domain.Utilities;

namespace DungeonCrawler.Domain.Entities;

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
            Math.Abs(element.Position.XPos - Position.XPos) <= VisionRange &&
            Math.Abs(element.Position.YPos - Position.YPos) <= VisionRange
        );

        foreach (LevelElement element in nearbyElements)
        {
            var distance = CalculateDistance.Between(Position, element.Position);
            if (distance < VisionRange)
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
        if (IsVisible)
        {
            Color = ConsoleColor.Green;
        }
        else
        {
            Color = ConsoleColor.Black;
        }
    }


    public override void Update()
    {
        // TODO: FIX BELOW AFTER MONGODB ADDITION
        //CheckSurrounding(GameState.LevelData.LevelElementsList);
        if (IsScared)
        {
            Position[] possibleMoves = new Position[4] {
                DirectionTransformer.GetPositionDelta(Direction.Up) + Position ,
                DirectionTransformer.GetPositionDelta(Direction.Left) + Position ,
                DirectionTransformer.GetPositionDelta(Direction.Right) + Position ,
                DirectionTransformer.GetPositionDelta(Direction.Down) + Position 
            };

            Position bestMove = new Position();
            double highestDistance = 0;
            foreach (var possibleMove in possibleMoves)
            {
                Console.WriteLine("SNAKE UPDATE(): REMOVE COMMENTED OUT CODE AFTER MONGODB ADDITION.");

                /*double currentCheck = CalculateDistance.Between(possibleMove, GameState.Player.Position);
                if ( currentCheck > highestDistance )
                {
                    if (AttemptMove(possibleMove, GameState))
                    {
                        highestDistance = currentCheck;
                        bestMove = possibleMove;
                    }
                }
                */
            }

            /* RE-ADD ME AFTER MONGODB ADDITION
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
            */
        }
    }
}
