using System.Linq;

namespace AdventOfCode22.Solutions
{
    internal class Day5
    {
        public static void Compute(string[] inputLines)
        {
            var stacks = new List<Stack<char>>();

            int index = 0;

            var separators = new[] { ' ', '[', ']' };

            while (!string.IsNullOrEmpty(inputLines[index]))
            {
                var crates = inputLines[index].ToCharArray();

                for (int c = 0; c < crates.Length; c++)
                {
                    if (separators.Contains(crates[c]) || int.TryParse(crates[c].ToString(), out _))
                        continue;

                    var stackOffset = c / 4;

                    while (stacks.Count <= stackOffset)
                        stacks.Add(new Stack<char>());

                    stacks[stackOffset].Push(crates[c]);
                }

                index++;
            }
            index++;

            for (int i = 0; i < stacks.Count(); i++)
            {
                stacks[i] = new Stack<char>(stacks[i]); 
            }

            while(index < inputLines.Length)
            {
                var moves = inputLines[index]
                    .Split(' ')
                    .Where(c => int.TryParse(c, out _))
                    .Select(int.Parse)
                    .ToArray();
                int n = moves[0], s = moves[1], d = moves[2];

                var tempStack = new Stack<char>();
                for (int i = 0; i < n; i++)
                {
                    tempStack.Push(stacks[s - 1].Pop());
                }
                while(tempStack.Any())
                {
                    stacks[d-1].Push(tempStack.Pop());
                }

                index++;
            }

            var result = string.Join("", stacks.Select(s => s.Peek()));

            Console.WriteLine(result);
        }

        

    }
}
