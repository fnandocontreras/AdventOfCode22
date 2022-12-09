using System.Drawing;

namespace AdventOfCode22.Solutions
{
    internal class Day9
    {
        public static void Compute(string[] inputLines)
        {
            var talePositions = new HashSet<Point>();

            var knots = new LinkedList<Point>(Enumerable.Range(0, 10).Select(_ => new Point(0, 0)));
            talePositions.Add(knots.First());

            foreach (var line in inputLines)
            {
                var split = line.Split(' ');
                var dir = split[0];
                var steps = int.Parse(split[1]);

                

                while(steps > 0)
                {
                    var head = knots.First;
                    var tail = head.Next;

                    var headMove = GetHeadMove(dir);
                    head.Value = new Point(
                        head.Value.X + headMove.X,
                        head.Value.Y + headMove.Y);

                    while (tail is not null)
                    {
                        var taleMove = GetTaleMove(head.Value, tail.Value);
                        tail.Value = new Point(tail.Value.X + taleMove.X, tail.Value.Y + taleMove.Y);

                        head = tail;
                        tail = tail.Next;
                    }

                    talePositions.Add(knots.Last());
                    steps--;
                }
            }

            Console.WriteLine(talePositions.Count());
        }

        private static Point GetHeadMove(string direccion) => direccion switch
        {
            "U" => new Point(1, 0),
            "D" => new Point(-1, 0),
            "L" => new Point(0, -1),
            "R" => new Point(0, 1),
        };

        private static Point GetTaleMove(Point head, Point tale) =>
            Math.Abs(head.X - tale.X) > 1 ||
            Math.Abs(head.Y - tale.Y) > 1 ?
            new Point(head.X.CompareTo(tale.X), head.Y.CompareTo(tale.Y)):
            new Point(0,0);

    }
}
