using Labb2_DungeonCrawler.Core;
namespace Labb2_DungeonCrawler.LevelElements;

public class Rat : Enemy 
{
    public Rat(Position pos, char representation, ConsoleColor color) : base(pos, representation, color)
    {
         
    }

    public override void Update()
    {
        Random ratRandom = new Random();
        int stepInCardinalDirection = ratRandom.Next(0, 4);
        Console.SetCursorPosition(Position.XPos, Position.YPos);
        Console.Write(' ');
        MoveMe((Direction)stepInCardinalDirection);
        Draw();

        
    }
}
