namespace AdventOfCode22.Solutions
{
    internal class Day7
    {
        static List<int> dirSizes = new();
        static int capacity = 70000000;
        static int neededSpace = 30000000;
        
        public static void Compute(string[] inputLines)
        {
            var index = 0;
            var rootSize = ComputeSize(inputLines, ref index);
            var remainingSpace = capacity - rootSize;

            Console.WriteLine(dirSizes.Order()
                .First(s => remainingSpace + s >= neededSpace));
        }

        public static int ComputeSize(string[] inputLines, ref int index)
        {
            var rootSize = 0;

            while (index < inputLines.Length)
            {
                var line = inputLines[index];
                var cli = line.Split(' ');
                if (int.TryParse(cli[0], out int size))
                {
                    rootSize += size;
                }
                else if (inputLines[index] == "$ cd ..")
                {
                    return rootSize;
                }
                else if (cli[1] == "cd")
                {
                    index++;
                    var subTreeSize = ComputeSize(inputLines, ref index);
                    rootSize += subTreeSize;

                    dirSizes.Add(subTreeSize);
                }

                index++;
            }
            return rootSize;
        }
    }
}
