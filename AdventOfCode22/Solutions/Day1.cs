namespace AdventOfCode22.Solutions
{
    internal class Day1
    {
        public static void Compute(string[] inputLines)
        {
            var list = new List<int>();

            int current = 0;
            foreach (var line in inputLines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    list.Add(current);
                    current = 0;
                }
                else current += int.Parse(line);
            }

            var sum = list.OrderDescending().Take(3).Sum();

            Console.WriteLine(sum);
        }

    }
}
