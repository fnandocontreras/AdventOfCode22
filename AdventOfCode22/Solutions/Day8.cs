namespace AdventOfCode22.Solutions
{
    internal class Day8
    {
        public static void Compute(string[] inputLines)
        {
            var forestLeftToRight = inputLines.Select((l, i) => 
                l.Select((c, j) => new Tree(c - '0', i,j)).ToArray()).ToArray();
            var forestRightToLeft = forestLeftToRight.Select(l => l.Reverse().ToArray()).ToArray();
            var forestTopDown = Pivot(forestLeftToRight).ToArray();
            var forestBottomUp = forestTopDown.Select(l => l.Reverse().ToArray()).ToArray();

            var visibleTrees = new HashSet<(int, int)>();

            ExploreForest(forestLeftToRight, visibleTrees);
            ExploreForest(forestRightToLeft, visibleTrees);
            ExploreForest(forestTopDown, visibleTrees);
            ExploreForest(forestBottomUp, visibleTrees);

            Console.WriteLine(visibleTrees.Count());
        }

        static void ExploreForest(Tree[][] inputLines, HashSet<(int, int)> visibleTrees) 
        {
            for (int i = 0; i < inputLines.Length; i++)
            {
                Tree highest = new(-1,0,0);
                for (int j = 0; j < inputLines[i].Length; j++)
                {
                    var current = inputLines[i][j];
                    if (current.height > highest.height)
                    {
                        visibleTrees.Add((current.x, current.y));
                        highest = current;
                    }
                }
            }
        }

        static IEnumerable<Tree[]> Pivot(Tree[][] forest)
        {
            for (int i = forest[0].Length - 1; i >= 0; i--)
            {
                var list = new Tree[forest.Length];
                for (int j = 0; j < forest.Length; j++)
                {
                    list[j] = forest[j][i];
                }
                yield return list;
            }
        }

        record Tree(int height, int x, int y);


        public static void ComputeV2(string[] inputLines)
        {
            var forest = inputLines.SelectMany((l, i) =>
                l.Select((c, j) => new Tree(c - '0', i, j)).ToArray()).ToArray();

            int maxScenicScore = 0;

            foreach (var a in forest)
            {
                var row = forest.Where(t => t.x == a.x && t.y != a.y).ToList();
                var column = forest.Where(t => t.y == a.y && t.x != a.x).ToList();

                var visibleatRight = row.Count(b => a.y < b.y && !row.Any(c => a.y < c.y && c.y < b.y && c.height >= a.height));
                var visibleatLeft = row.Count(b => b.y < a.y && !row.Any(c => b.y < c.y && c.y < a.y && c.height >= a.height));
                var visibleAtDown = column.Count(b => a.x < b.x && !column.Any(c => a.x < c.x && c.x < b.x && c.height >= a.height));
                var visibleAtUp = column.Count(b => b.x < a.x && !column.Any(c => b.x < c.x && c.x < a.x && c.height >= a.height));
                var currentScore = visibleatLeft * visibleatRight * visibleAtUp * visibleAtDown;

                maxScenicScore = Math.Max(maxScenicScore, currentScore);
            }
            Console.WriteLine(maxScenicScore);
        }


    }
}
