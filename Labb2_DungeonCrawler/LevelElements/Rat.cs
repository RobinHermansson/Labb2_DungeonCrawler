using Labb2_DungeonCrawler.Core;
using Labb2_DungeonCrawler.Utilities;
using Labb2_DungeonCrawler.Features;
namespace Labb2_DungeonCrawler.LevelElements;

public class Rat : Enemy
{
    public Rat(Position pos, char representation, ConsoleColor color) : base(pos, representation, color)
    {
        Name = "Ratty";
        HitPoints = 20;
        AttackDiceCount = 1;
        DefenceDiceCount = 1;
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


        while (true && IsAlive())
        {
            int stepInCardinalDirection = ratRandom.Next(0, 4);
            Position attempt = DirectionTransformer.GetPositionDelta((Direction)stepInCardinalDirection) + Position;
            if (AttemptMove(attempt, GameState))
            {
                Console.SetCursorPosition(Position.XPos, Position.YPos);
                Console.Write(' ');
                MoveMe((Direction)stepInCardinalDirection);
                //Draw();
                break;
            }
            else if (attempt == GameState.Player.Position)
            {
                Combat combat = new Combat(this, GameState.Player);
                combat.StartCombat();
            }
        }
    }



}
