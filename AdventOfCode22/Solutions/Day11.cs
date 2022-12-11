using System.Linq;
using System.Numerics;

namespace AdventOfCode22.Solutions
{
    internal class Day11
    {
        static Monkey[] TestCase = new[] {
            new Monkey
            {
                Items= new Queue<Congruence>(Congruence.FromNumbers(new int[] { 79, 98 })),
                Operation = old => old.Multiply(19),
                Divisor = 23,
                Iftrue = 2,
                IfFalse = 3
            },
            new Monkey
            {
                Items= new Queue<Congruence>(Congruence.FromNumbers(new int[] {54, 65, 75, 74})),
                Operation = old => old.Sum(6),
                Divisor = 19,
                Iftrue = 2,
                IfFalse = 0
            },
            new Monkey
            {
                Items= new Queue<Congruence>(Congruence.FromNumbers(new int[] { 79, 60, 97 })),
                Operation = old => old.Square(),
                Divisor = 13,
                Iftrue = 1,
                IfFalse = 3
            },
            new Monkey
            {
                Items= new Queue<Congruence>(Congruence.FromNumbers(new int[] { 74 })),
                Operation = old => old.Sum(3),
                Divisor = 17,
                Iftrue = 0,
                IfFalse = 1
            }

        };

        static Monkey[] Input = new[] {
            new Monkey
            {
                Items= new Queue<Congruence>(Congruence.FromNumbers(new int[] { 54, 98, 50, 94, 69, 62, 53, 85 })),
                Operation = old => old.Multiply(13),
                Divisor = 3,
                Iftrue = 2,
                IfFalse = 1
            },
            new Monkey
            {
                Items= new Queue<Congruence>(Congruence.FromNumbers(new int[] { 71, 55, 82 })),
                Operation = old => old.Sum( 2),
                Divisor = 13,
                Iftrue = 7,
                IfFalse = 2
            },
            new Monkey
            {
                Items= new Queue<Congruence>(Congruence.FromNumbers(new int[] { 77, 73, 86, 72, 87 })),
                Operation = old => old.Sum( 8),
                Divisor = 19,
                Iftrue = 4,
                IfFalse = 7
            },
            new Monkey
            {
                Items= new Queue<Congruence>(Congruence.FromNumbers(new int[] { 97, 91 })),
                Operation = old => old.Sum( 1),
                Divisor = 17,
                Iftrue = 6,
                IfFalse = 5
            },
            new Monkey
            {
                Items= new Queue<Congruence>(Congruence.FromNumbers(new int[] { 78, 97, 51, 85, 66, 63, 62 })),
                Operation = old => old.Multiply( 17),
                Divisor = 5,
                Iftrue = 6,
                IfFalse = 3
            },
            new Monkey
            {
                Items= new Queue<Congruence>(Congruence.FromNumbers(new int[] { 88 })),
                Operation = old => old.Sum( 3),
                Divisor = 7,
                Iftrue = 1,
                IfFalse = 0
            },
            new Monkey
            {
                Items= new Queue<Congruence>(Congruence.FromNumbers(new int[] { 87, 57, 63, 86, 87, 53 })),
                Operation = old => old.Square( ), 
                Divisor = 11,
                Iftrue = 5,
                IfFalse = 0
            },
            new Monkey
            {
                Items= new Queue<Congruence>(Congruence.FromNumbers(new int[] { 73, 59, 82, 65 })),
                Operation = old => old.Sum( 6),
                Divisor = 2,
                Iftrue = 4,
                IfFalse = 3
            }

        };


        public static void Compute(string[] inputLines)
        {
            var monkeys = Input;

            var counters = new long[monkeys.Length];

            for (int i = 1; i <= 10000; i++)
            {
                for (int m = 0; m < monkeys.Length; m++)
                {
                    var monkey = monkeys[m];
                    while(monkey.Items.Any())
                    {
                        counters[m]++;
                        var currentItem = monkey.Items.Dequeue();
                        monkey.Operation(currentItem);
                        var relayMonkey = (currentItem.Coeficients[monkey.Divisor] == 0) switch
                        {
                            true => monkey.Iftrue,
                            false => monkey.IfFalse
                        };
                        monkeys[relayMonkey].Items.Enqueue(currentItem);
                    }
                }
                LogCounters(counters, i);
            }
            var mostActive = counters.OrderDescending().Take(2).ToArray();
            Console.WriteLine(mostActive[0] * mostActive[1]);
        }

        static void LogCounters(long[] counters, int round)
        {
            if (round != 1 && round != 20 && round % 1000 != 0) return;

            Console.WriteLine(
                $"== After round {round} ==");
            for (int i = 0; i < counters.Length; i++)
            {
                Console.WriteLine($"Monkey {i} inspected items {counters[i]} times.");
            }
            Console.WriteLine();
        }

        class Monkey
        {
            public Queue<Congruence> Items { get; set; }
            public Action<Congruence> Operation { get; set; }

            public int Divisor { get; set; }
            public int Iftrue { get; set; }
            public int IfFalse { get; set; }
        }

        class Congruence
        {
            public int[] Coeficients { get; set; } = new int[24];

            public static IEnumerable<Congruence> FromNumbers(int[] numbers)
            {
                foreach (var num in numbers)
                {
                    var result = new Congruence();

                    for (int divisor = 1; divisor < result.Coeficients.Length; divisor++)
                    {
                        result.Coeficients[divisor] = num % divisor;
                    }
                    yield return result;
                }
            }

            public void Multiply(int num)
            {
                for (int divisor = 1; divisor < Coeficients.Length; divisor++)
                {
                    Coeficients[divisor] *= (num % divisor);
                    Coeficients[divisor] %= divisor;
                }
            }

            public void Sum(int num)
            {
                for (int divisor = 1; divisor < Coeficients.Length; divisor++)
                {
                    Coeficients[divisor] += (num % divisor);
                    Coeficients[divisor] %= divisor;
                }
            }

            public void Square()
            {
                for (int divisor = 1; divisor < Coeficients.Length; divisor++)
                {
                    Coeficients[divisor] *= Coeficients[divisor];
                    Coeficients[divisor] %= divisor;
                }
            }
        }

    }
}
 