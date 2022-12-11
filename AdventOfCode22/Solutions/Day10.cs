namespace AdventOfCode22.Solutions
{
    internal class Day10
    {
        public static void Compute(string[] inputLines)
        {
            var signals = new List<int>();
            var currentSignal = 1;

            var crt = new[] {
                new char[40],
                new char[40],
                new char[40],
                new char[40],
                new char[40],
                new char[40]
            };

            var commands = Transform(inputLines).ToArray();
            
            for (int i = 0; i < commands.Length; i++)
            {
                var v = commands[i];
                
                if ((i + 1 - 20) % 40 == 0)
                    signals.Add(currentSignal * (i+1));

                var line = i / 40;
                var offset = i % 40;
                char cur = 
                    currentSignal >= offset - 1 && 
                    currentSignal <= offset + 1 
                    ? '#' : '.';
                crt[line][offset] = cur;

                currentSignal += v;
            }
            Console.WriteLine(signals.Sum());

            foreach (var chars in crt)
            {
                Console.WriteLine(new string(chars));
            }
        }

        static IEnumerable<int> Transform(string[] inputLines)
        {
            foreach (var cli in inputLines)
            {
                yield return 0;
                if (!cli.Contains("noop"))
                    yield return int.Parse(cli.Split(' ')[1]);
            }
        }

    }
}
