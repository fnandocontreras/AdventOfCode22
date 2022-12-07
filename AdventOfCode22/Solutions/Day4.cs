using System.Linq;

namespace AdventOfCode22.Solutions
{
    internal class Day4
    {
        public static void Compute(string[] inputLines)
        {
            int count = 0;
            foreach (var line in inputLines)
            {
                var nums = line.Split(new char []{ ',', '-' }).Select(int.Parse).ToArray();

                if (Enumerable.Range(nums[0], nums[1] - nums[0] + 1)
                    .Intersect(Enumerable.Range(nums[2], nums[3] - nums[2] + 1)).Any())
                    count++;
            }
            Console.WriteLine(count);
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
