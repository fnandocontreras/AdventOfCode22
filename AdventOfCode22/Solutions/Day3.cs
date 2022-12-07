using System.Linq;

namespace AdventOfCode22.Solutions
{
    internal class Day3
    {
        public static void Compute(string[] inputLines)
        {
            var sum = inputLines
                .Chunk(3)
                .Select(g => 
                    g[0].Intersect(g[1]).Intersect(g[2])
                    .Single())
                .Sum(b => b switch
            {
                >= 'a' and <= 'z' => b - 'a' + 1,
                >= 'A' and <= 'Z' => b - 'A' + 27
            });
            
            Console.WriteLine(sum);
        }

        public static void ComputeV1(string[] inputLines)
        {
            var sum = inputLines
                .Select(rucksack => rucksack.Substring(0, rucksack.Length / 2)
                    .Intersect(rucksack.Substring(rucksack.Length / 2)).Single())
                .Sum(c => c switch
                {
                    >= 'a' and <= 'z' => c - 'a' + 1,
                    >= 'A' and <= 'Z' => c - 'A' + 27
                });

            Console.WriteLine(sum);
        }

    }
}
