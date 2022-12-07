namespace AdventOfCode22.Solutions
{
    internal class Day2
    {
        public static void Compute(string[] inputLines)
        {
            var plays = new[] { 1, 2, 3, 1 }.ToList();
            int score = 0;

            foreach (var line in inputLines)
            {
                var round = line.Split(' ');
                var player1 = round[0][0] - 'A' + 1;
                var player2 = round[1][0] - 'X' + 1;

                score += (player1, player2) switch
                {
                    (int a, 1) => 0 + plays[plays.LastIndexOf(a)-1],
                    (int a, 2) => 3 + a,
                    (int a, 3) => 6 + plays[plays.IndexOf(a) + 1],
                };
            }
            Console.WriteLine(score);
        }

    }
}
