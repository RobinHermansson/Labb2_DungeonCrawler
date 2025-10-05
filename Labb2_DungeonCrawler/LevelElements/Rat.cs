using Labb2_DungeonCrawler.Core;
using Labb2_DungeonCrawler.Utilities;
namespace Labb2_DungeonCrawler.LevelElements;

public class Rat : Enemy 
{
    public Rat(Position pos, char representation, ConsoleColor color) : base(pos, representation, color)
    {
         
    }

    public override void Update()
    {
        Random ratRandom = new Random();
        
        
        while (true)
        {
            int stepInCardinalDirection = ratRandom.Next(0, 4);
            Position attempt = DirectionTransformer.GetPositionDelta((Direction)stepInCardinalDirection) + Position;
            if (AttemptMove(attempt, GameState))
            {
                Console.SetCursorPosition(Position.XPos, Position.YPos);
                Console.Write(' ');
                MoveMe((Direction)stepInCardinalDirection);
                Draw();
                break;
            }
            else if (attempt == GameState.Player.Position)
            {
                Console.WriteLine("A FIGHT WILL BREAK LOOSE!");
            }
        }


        
    }
}
