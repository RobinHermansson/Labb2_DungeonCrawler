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
            Player.CheckSurrounding(GameState.LevelData.LevelElementsList);


        }

        public void PlayGame()
        {
            
            Console.CursorVisible = false;
                        foreach (var element in GameState.LevelData.LevelElementsList)
            {
                if (element is Enemy enemy)
                {
                    enemy.GameState = this.GameState; // TODO: Hope to fix this later on and now set the entire GameState on each character...
                }
                if (element.isVisible)
                {
                    element.Draw();
                }
            }
            while (true)
            {
                
                ConsoleKeyInfo input = Console.ReadKey();
                Position oldPosition = Player.Position;
                Console.SetCursorPosition(oldPosition.XPos, oldPosition.YPos);
                Console.Write(' ');

                Position attempt = Player.MovementHandler(input);
                Enemy enemyAtPosition = GameState.LevelData.LevelElementsList
                    .OfType<Enemy>() // Filter to only Enemy types
                    .FirstOrDefault(enemy => enemy.Position.XPos == attempt.XPos && 
                                            enemy.Position.YPos == attempt.YPos);
                
                if (enemyAtPosition != null)
                {
                    // Found an enemy at this position - initiate combat!
                    Combat combat = new Combat(aggressor: Player, defender: enemyAtPosition);
                    combat.StartCombat();
                }
                else if (Player.AttemptMove(attempt, GameState))
                {
                    Player.MoveTo(attempt);
                    Player.CheckSurrounding(GameState.LevelData.LevelElementsList);
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
                GameState.LevelData.LevelElementsList.Remove(enemyWhoDied);
                foreach (var elementRedraw in GameState.LevelData.LevelElementsList)
                {
                    if (elementRedraw.isVisible)
                    {
                        elementRedraw.Draw();
                    }
                }  
                foreach (var element in GameState.LevelData.LevelElementsList)
                {
                    if (element.isVisible && element is not Wall)
                    {
                        element.Draw();
                    }
                    else if (element is Wall && element.isVisible)
                    {
                        element.Color = ConsoleColor.DarkYellow;
                        element.Draw();
                    }
                    else if (element is Wall && element.hasBeenSeen && !element.isVisible)
                    {
                        element.Color = ConsoleColor.DarkGray;
                        element.Draw();
                    }
                }

            }
        }
    }
}
