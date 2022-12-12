using System.Drawing;

namespace AdventOfCode22.Solutions
{
    internal class Day12
    {
        public static void Compute(string[] inputLines)
        {
            var startPositions = GetPositions('S', inputLines)
                .Concat(GetPositions('a', inputLines)).ToList();
            var E = GetPositions('E', inputLines).Single();

            var grid = inputLines.Select(x => x.ToCharArray()).ToArray();

            int shortestPath = int.MaxValue;

            foreach (var S in startPositions)
            {
                var visited = new int[inputLines.Length, inputLines[0].Length];
                for (int i = 0; i < inputLines.Length; i++)
                    for (int j = 0; j < inputLines[0].Length; j++)
                        visited[i, j] = -1;

                var queue = new Queue<Point>();
                visited[S.X, S.Y] = 0;
                grid[S.X][S.Y] = 'a';
                grid[E.X][E.Y] = 'z';
                queue.Enqueue(S);

                while (queue.Any())
                {
                    var current = queue.Dequeue();
                    var height = grid[current.X][current.Y];

                    if (current.Equals(E))
                    {
                        shortestPath = Math.Min(visited[current.X, current.Y], shortestPath);
                        break;
                    }

                    foreach (var ady in GetPosibleMoves(grid, current).Where(p => visited[p.X, p.Y] < 0))
                    {
                        if (grid[ady.X][ady.Y] - height <= 1)
                        {
                            queue.Enqueue(ady);
                            visited[ady.X, ady.Y] = visited[current.X, current.Y] + 1;
                        }
                    }
                }
            }

            Console.WriteLine(shortestPath);
            
        }

        static IEnumerable<Point> GetPositions(char c, string[] inputLines)
        {
            for (int i = 0; i < inputLines.Length; i++)
            {
                int j = inputLines[i].IndexOf(c);
                if (j >= 0) yield return new(i, j);
            }
        }

        static IEnumerable<Point> GetPosibleMoves(char[][] grid, Point c)
        {
            var h = grid[c.X][c.Y];

            //UP
            if (c.X > 0)
                yield return new(c.X - 1, c.Y);

            //DOWN
            if (c.X < grid.Length - 1)
                yield return new(c.X + 1, c.Y);

            //LEFT
            if (c.Y > 0 )
                yield return new(c.X, c.Y - 1);

            //DOWN
            if (c.Y < grid[0].Length - 1)
                yield return new(c.X, c.Y + 1);
        }

    }
}
 