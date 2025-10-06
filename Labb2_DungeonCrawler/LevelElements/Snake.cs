using Labb2_DungeonCrawler.Core;
using Labb2_DungeonCrawler.Features;

namespace Labb2_DungeonCrawler.LevelElements;

public class Snake: Enemy, IMovable
{
    public Snake(Position pos, char representation, ConsoleColor color) : base(pos, representation, color)
    {
        
        Name = "Snakk";
        HitPoints = 20;
        AttackDiceCount = 3;
        DefenceDiceCount = 1;
        AttackModifier = 1;
        DefenceModifier = 3;
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

    public bool AttemptMove()
    {
        return false;
    }

    public override void Update()
    {
        AttemptMove(Position, GameState);
    }
}
