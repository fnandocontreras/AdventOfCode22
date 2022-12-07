namespace AdventOfCode22.Solutions
{
    internal class Day6
    {
        public static void Compute(string[] inputLines)
        {
            var input = inputLines[0];

            for (int i = 14; i < input.Length; i++)
            {
                if(input.Substring(i-14,14).Distinct().Count() == 14)
                {
                    Console.WriteLine(i);
                    break;
                }
            }
        }

        public static void ComputeOptimized(string[] inputLines)
        {
            var input = inputLines[0];
            int k = 4;

            int unique = 0;
            var counters = new int['z' - 'a' + 1];

            foreach (var c in input.Take(k))
            {
                var index = c - 'a';
                counters[index]++;
                if (counters[index] == 1)
                    unique++;
            }

            for (int i = k; i < input.Length; i++)
            {
                if (unique == k)
                {
                    Console.WriteLine(i);
                    break;
                }

                //add
                var head = input[i];
                var headIndex = head - 'a';

                counters[headIndex]++;
                if (counters[headIndex] == 1)
                    unique++;

                //remove
                var tale = input[i-k];
                var taleIndex = tale - 'a';

                counters[taleIndex]--;
                if (counters[taleIndex] == 0)
                    unique--;
            }
        }



    }
}
