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
            // Calculate direction vector away from player
            int xDirection = this.Position.XPos - GameState.Player.Position.XPos;
            int yDirection = this.Position.YPos - GameState.Player.Position.YPos;

            // Determine best move to flee
            Position newPosition = new Position(this.Position.XPos, this.Position.YPos);

            // If player is more to the right, try to move left
            if (xDirection < 0)
                newPosition.XPos -= 1;
            // If player is more to the left, try to move right
            else if (xDirection > 0)
                newPosition.XPos += 1;

            // If player is more below, try to move up
            if (yDirection < 0)
                newPosition.YPos -= 1;
            // If player is more above, try to move down
            else if (yDirection > 0)
                newPosition.YPos += 1;

            if (AttemptMove(newPosition, GameState))
            {
                Console.SetCursorPosition(Position.XPos, Position.YPos);
                Console.Write(' ');
                MoveTo(newPosition);
            }
        }
    }
}
