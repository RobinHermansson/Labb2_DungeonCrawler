using Labb2_DungeonCrawler.LevelElements;
namespace Labb2_DungeonCrawler.Core
{
    public class Gameloop
    {
        public GameState GameState{ get; set; }
        public Player Player { get; set; }
        public Gameloop(GameState gameState)
        {

            GameState = gameState;
            Player = gameState.Player;

        }

        public void PlayGame()
        {
            foreach (var element in GameState.LevelData.LevelElementsList)
            {
                if (element is Enemy enemy)
                {
                    enemy.GameState = this.GameState; // TODO: Hope to fix this later on and now set the entire GameState on each character...
                }
                element.Draw();
            }

            Console.CursorVisible = false;
            while (true)
            {
                
                ConsoleKeyInfo input = Console.ReadKey();
                Position oldPosition = Player.Position;
                Console.SetCursorPosition(oldPosition.XPos, oldPosition.YPos);
                Console.Write(' ');

                Position attempt = Player.MovementHandler(input);
                if (Player.AttemptMove(attempt, GameState))
                {
                    Player.MoveTo(attempt);
                }
                Player.Draw();


                Enemy enemyWhoDied = null;
                foreach (var element in GameState.LevelData.LevelElementsList)
                {
                    if (element is Enemy enemyObject)
                    {
                        enemyObject.Update();
                        // After a combat, attempt to remove the enemy. Need to redraw everything as the Combat clears the screen and also the enemy is gone. FIX THIS IN A MORE PROPER WAY!
                        if (!enemyObject.IsAlive())
                        {
                            enemyWhoDied = enemyObject;
                           
                        }
                    }
                }
                if (enemyWhoDied is not null)
                {
                    GameState.LevelData.LevelElementsList.Remove(enemyWhoDied);
                    foreach (var elementRedraw in GameState.LevelData.LevelElementsList)
                    {
                        elementRedraw.Draw();
                    }
                    
                }

            }
        }
    }
}
