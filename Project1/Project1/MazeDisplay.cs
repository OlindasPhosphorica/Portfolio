using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    /// <summary>
    /// Method to display the maze in the console.
    /// </summary>
    public class MazeDisplay
    {
        /// <summary>
        /// Uses '#' for walls and ' ' for paths to print the maze to the console. Set in a for loop to iterate through the 2D array.
        /// </summary>
        /// <param name="maze"></param>
        public static void printMaze(Maze.CellType[,] maze)
        {
            int width = maze.GetLength(0);
            int height = maze.GetLength(1);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (maze[x, y] == Maze.CellType.Wall)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
