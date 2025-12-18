using Project1;
using System;
using System.Security.Cryptography.X509Certificates;


public class Program
{
    /// <summary>
    /// Main entry point for the console-based maze game.
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        /// <summary>
        /// Sets up game state, including maze generation, player placement, item and enemy placement.
        /// </summary>

        Console.CursorVisible = false;
        var maze = new Maze(41, 21);
        var mazeGen = maze.Generate();
        var gameState = new GameState(mazeGen);

        //set exit
        var exitPos = FindRandomOpenSpot(mazeGen, new Random());
        mazeGen[exitPos.x, exitPos.y] = Maze.CellType.Path;
        gameState.SetExit(exitPos.x, exitPos.y);

        // place player
        var playerStart = FindRandomOpenSpot(mazeGen, new Random());
        var player = new Player("Player1", 100) { X = playerStart.x, Y = playerStart.y };
        gameState.SetPlayer(player);

        //place weapon 
        for (int i = 0; i < 5; i++)
        {
            var itemPos = FindRandomOpenSpot(mazeGen, new Random());
            gameState.AddItem(new Weapon(
                "axe",
                "A ridiculously large axe. It's amazing you can even pick it up.",
                20,
                80,
                itemPos.x,
                itemPos.y,
                'W'
            ));


        }

        //place health potions
        for (int i = 0; i < 4; i++)
        {
            var itemPos = FindRandomOpenSpot(mazeGen, new Random());
            var healthPot = new HealthPot(
                "Potion",
                itemPos.x,
                itemPos.y,
                'H'
            );
            gameState.AddItem(healthPot);
        }

        //place enemies
        for (int i = 0; i < 3; i++)
        {
            var enemyPos = FindRandomOpenSpot(mazeGen, new Random());
            gameState.AddMonster(new Monster("Ogre", 80, "Standard") { X = enemyPos.x, Y = enemyPos.y });

        }

        while (!gameState.IsGameOver)
        {
            if (Console.KeyAvailable)
            {
                var keyInfo = Console.ReadKey(true);
                HandleInput(gameState, keyInfo);
            }


            RenderGame(gameState);
            System.Threading.Thread.Sleep(1000);
        }

    }
    /// <summary>
    /// Fins a random open spot in the maze that is not a wall.
    /// </summary>
    /// <param name="maze"></param>
    /// <param name="random"></param>
    /// <returns></returns>
    public static (int x, int y) FindRandomOpenSpot(Maze.CellType[,] maze, Random random)
    {
        int width = maze.GetLength(0);
        int height = maze.GetLength(1);

        while (true)
        {
            int x = random.Next(1, width - 1);
            int y = random.Next(1, height - 1);

            if (maze[x, y] == Maze.CellType.Path)
            {
                return (x, y);
            }
        }
    }

    /// <summary>
    /// Handles player input for movement and interactions.
    /// </summary>
    /// <param name="gameState"></param>
    /// <param name="keyInfo"></param>
    private static void HandleInput(GameState gameState, ConsoleKeyInfo keyInfo)
    {
        int newX = gameState.Player.X;
        int newY = gameState.Player.Y;

        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
            case ConsoleKey.W:
                newY--;
                break;
            case ConsoleKey.DownArrow:
            case ConsoleKey.S:
                newY++;
                break;
            case ConsoleKey.LeftArrow:
            case ConsoleKey.A:
                newX--;
                break;
            case ConsoleKey.RightArrow:
            case ConsoleKey.D:
                newX++;
                break;

        }

        ///<summary>
        ///check for items
        /// </summary>
        var item = gameState.Items.FirstOrDefault(i => i.X == newX && i.Y == newY);
        if (item != null)
        {
            if (item is Weapon weapon)
            {
                gameState.Message = $"You picked up a {weapon.Name}!";
                gameState.Items.Remove(item);
            }
            else if (item is HealthPot healthPot)
            {
                gameState.Message = "You drink the health potion and feel rejuvenated!";
                gameState.Player.Health += 20;
                if (gameState.Player.Health > 100) gameState.Player.Health = 100;
                gameState.Items.Remove(item);
            }
        }

        //check for enemies
        var enemy = gameState.Enemies.FirstOrDefault(e => e.X == newX && e.Y == newY);
        if (enemy != null)
        {
            if (enemy is Monster monster)
            {
                gameState.Message = $"You encountered a {monster.Name}!";
                gameState.Player.Attack(monster);
                if (monster.Health > 0)
                {
                    monster.Attack(gameState.Player);
                    if (gameState.Player.Health <= 0)
                    {
                        gameState.Message = "You have been defeated! Game Over.";
                        gameState.EndGame();
                        return;
                    }
                }
                else
                {
                    gameState.Message = $"You defeated the {monster.Name}!";
                    gameState.Enemies.Remove(enemy);
                    gameState.AddScore(10);
                }
            }
        }

        ///<summary>
        ///checks for walls
        ///</summary>
        if (gameState.Maze[newX, newY] != Maze.CellType.Wall)
        {
            gameState.Player.X = newX;
            gameState.Player.Y = newY;
        }

        ///<summary>
        ///check for exit
        ///</summary>
        if (newX == gameState.ExitX && newY == gameState.ExitY)
        {
            Console.Clear();
            gameState.Message = "Congratulations! You've reached the exit and won the game!";
            gameState.EndGame();
        }
    }

    /// <summary>
    /// Renders the game state to the console.
    /// </summary>
    private static void RenderGame(GameState gameState)
    {
        Console.SetCursorPosition(0, 0);
        int width = gameState.Maze.GetLength(0);
        int height = gameState.Maze.GetLength(1);

        MazeDisplay.printMaze(gameState.Maze);


        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                bool objectDrawn = false;

                // Draw player
                if (x == gameState.Player.X && y == gameState.Player.Y)
                {
                    Console.SetCursorPosition(gameState.Player.X, gameState.Player.Y);
                    Console.Write(gameState.Player.Symbol + " ");
                    objectDrawn = true;
                }

                // Draw items
                // Use .Any() for efficiency and check for the first match
                if (!objectDrawn && gameState.Items.Any(i => i.X == x && i.Y == y))
                {
                    var itemToDraw = gameState.Items.First(i => i.X == x && i.Y == y);
                    Console.SetCursorPosition(itemToDraw.X, itemToDraw.Y);
                    Console.Write(itemToDraw.Symbol + " ");
                    objectDrawn = true;
                }

                // Draw enemies
                if (!objectDrawn && gameState.Enemies.Any(e => e.X == x && e.Y == y))
                {
                    var enemyToDraw = gameState.Enemies.First(e => e.X == x && e.Y == y);
                    Console.SetCursorPosition(enemyToDraw.X, enemyToDraw.Y);
                    Console.Write(gameState.Enemies.First(e => e.X == x && e.Y == y).Symbol + " ");
                    objectDrawn = true;
                }

                // Draw exit
                if (!objectDrawn !& x == gameState.ExitX && y == gameState.ExitY)
                {
                    Console.Write("><"); // Exit point
                    objectDrawn = true;
                }


            }
        }
        ///<summary>
        ///"https://learn.microsoft.com/en-us/dotnet/api/system.console.setcursorposition?view=net-9.0"
        ///"how to move consols.Writline down the console" This is the search I used to find out how to move the console writeline down
        ///moves the status lines down so they dont overlap with the maze
        ///</summary>
        int statusY = height + 1;
        if (statusY < Console.WindowHeight)
        {
            Console.SetCursorPosition(0, statusY);
        }

        Console.WriteLine();
        Console.Write($"Score: {gameState.Score}".PadRight(Console.WindowWidth));
        Console.WriteLine();

        Console.Write($"Health: {gameState.Player.Health}".PadRight(Console.WindowWidth));
        Console.WriteLine();

        Console.WriteLine(gameState.Message.PadRight(Console.WindowWidth));
    }
}
