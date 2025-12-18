using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{

    public class Maze
    {
        // I sourced an example of a recursive backtracker for this. This is the link: https://www.google.com/search?q=how+to+make+a+randomly+generated+2d+maze+in+c%23&sca_esv=d2ab6361f8823ac8&rlz=1C1RXQR_enUS1178US1178&sxsrf=AE3TifM7CSWdtwNZnv7Utx-ODGhg0aOq0Q%3A1758815717037&ei=5WXVaIqIAs67kPIP7Ka52Q4&ved=2ahUKEwinm7vRo_SPAxURJ0QIHQoxA3QQ0NsOegQIUBAA&uact=5&sclient=gws-wiz-serp&udm=50&fbs=AIIjpHxU7SXXniUZfeShr2fp4giZud1z6kQpMfoEdCJxnpm_3YlUqOpj4OTU_HmqxOd8LCYAmZcz3xp4-s3ijYzIP40LlddfBAhJDuHsBzPcairVH6jEyLRYOBQgKx39vFebUA6gMRyOjUtKr2tAgLt8-riYCxo7cqYvgVIxY_03doEIFjWWiF6brNIzAObqF7XNPBoa6nWqDYwiLKQb2ooNcABsdF3WMg&aep=10&ntc=1&mtid=5mfVaJ7OFKvFkPIPt9fXuQs&mstk=AUtExfD0OIrj8CmOTcG89FYm-0oZt-cP9QKfEZ5wY7Z0o_SkF8Gt5n5K754GecqvUOKy_C4TddRXLs3VzPayS90W4MytC9TaJJdAGWeVoEGG8tHStL_3mYPQcMFClzm0oiIry2q5nuYftiPDM8bXwLE9o1TYqNbwAYW8nEizDiaZqsnKSZUH46uJNXuFgm2cmKyESKwQf25PHknx1IxsHTjzAwT4ZYMgcBcpge3c1fU7g_xjJph7A6zzkK04gu2_ISXM0Mn2dGyn0Nw_G9XxW-kll6iRGdyzxDFY3RxGnL3DV4nMyHsgp_TnIPsi4IogFOOON1Ew7reZgGvVng&csuir=1
        //"https://www.geeksforgeeks.org/dsa/shuffle-a-given-array-using-fisher-yates-shuffle-algorithm/"
        //This igthub link also helped me understand the recursive backtracker algorithm
        /// <summary>
        /// Stores the dimensions of the maze and the maze grid itself.
        /// </summary>
        private readonly int _width;
        private readonly int _height;
        private readonly int width = 21;
        private readonly int height = 11;
        private readonly CellType[,] _maze;
        private readonly Random _random;

        /// <summary>
        /// Lists the possible cell types in the maze.
        /// </summary>
        public enum CellType
        {
            Wall,
            Path,
        }

        /// <summary>
        /// Constructor for the Maze class, ensuring dimensions are odd and initializing the maze grid and random number generator.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <exception cref="ArgumentException"></exception>
        public Maze(int width, int height)
        {
            if (width % 2 == 0 || height % 2 == 0)
            {
                throw new ArgumentException("Dimensions msut be odd.");
            }
            _width = width;
            _height = height;
            _maze = new CellType[_width, _height];
            _random = new Random();
        }
        /// <summary>
        /// Generates the maze using a recursive backtracking algorithm.
        /// I needed a lot of help with this part, so I used the link above to help me understand how to do it. And I also used ChatGPT to help me understand how to implement it in C#.
        /// </summary>
        /// <returns></returns>
        public CellType[,] Generate() 
        {
            //intializes maze walls
            for (int x = 0; x < _width; x++) 
            {
                for (int y = 0; y < _height; y++) 
                {
                    _maze[x, y] = CellType.Wall;
                }
            }

            //random cell chosen to start making a path
            int startX = _random.Next(0, _width / 2) * 2 + 1;
            int startY = _random.Next(0, _height / 2) * 2 + 1;

            CarvePath(startX, startY);

            return _maze;
        }

        /// <summary>
        /// Carves a path in the maze starting from the given coordinates, using recursion to explore and create paths in random directions.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void CarvePath(int x, int y) 
        {
            _maze[x, y] = CellType.Path;

            //shuffles directions for it to remain random
            // the tuple keyword is new to me, but I learned the meaning of it in my math for comp Sci class, so I assuming it works off of the description I was given in that class.
            var directions = new List<Tuple<int, int>>
            {
             Tuple.Create(0,-2),
             Tuple.Create(0,2),
             Tuple.Create(2,0),
             Tuple.Create(-2,0)
            };

            Shuffle(directions);

            foreach (var direction in directions) 
            {
                int nextX = x + direction.Item1;
                int nextY = y + direction.Item2;

                if (nextX > 0 && nextX < _width && nextY > 0 && nextY < _height && _maze[nextX, nextY] == CellType.Wall) 
                {
                    _maze[nextX, nextY] = CellType.Path;
                    _maze[x + direction.Item1 / 2, y + direction.Item2 / 2] = CellType.Path;
                    CarvePath(nextX, nextY);
                }
            }
        }

        /// <summary>
        /// Shuffles a list in place using the Fisher-Yates algorithm.
        /// "https://www.geeksforgeeks.org/dsa/shuffle-a-given-array-using-fisher-yates-shuffle-algorithm/"
        /// This is another part I needed help with, so I used the link above to help me understand how to do it. And I also used ChatGPT to help me understand how to implement it in C#.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        private void Shuffle<T>(IList<T> list) 
        {
            int n = list.Count;
            while (n > 1) 
            {
                n--;
                int k = _random.Next(n +1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

       
    }
}
