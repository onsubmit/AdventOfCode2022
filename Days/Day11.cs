//-----------------------------------------------------------------------
// <copyright file="Day11.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AdventOfCode2022.Days
{
    using System.Text.RegularExpressions;
    using AdventOfCode2022.Models;
    using AdventOfCode2022.Utilities;

    /// <summary>
    /// Calculates the solution for the particular day.
    /// </summary>
    internal class Day11 : IDay
    {
        private const int MaxMonkeyBusinessRounds = 10000;

        private static readonly Regex MonkeyRegex = new(@"^Monkey \d+:$", RegexOptions.Compiled);
        private static readonly Regex StartItemsRegex = new(@"^  Starting items: (?<ITEMS>(\d+(, )?)+)$", RegexOptions.Compiled);
        private static readonly Regex OperationRegex = new(@"^  Operation: new = old (?<OPERATOR>[*+]) (?<VALUE>(old|\d+))$", RegexOptions.Compiled);
        private static readonly Regex TestRegex = new(@"^  Test: divisible by (?<VALUE>\d+)$", RegexOptions.Compiled);
        private static readonly Regex TestResultRegex = new(@"^    If (?<CONDITION>(true|false)): throw to monkey (?<MONKEY>\d+)$", RegexOptions.Compiled);
        private readonly Dictionary<Regex, Action<Match, Monkey>> actionMap;

        private readonly List<Monkey> monkeys = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Day11"/> class.
        /// </summary>
        public Day11()
        {
            this.actionMap = new()
            {
                {
                    // Starting items: 79, 98
                    StartItemsRegex,
                    this.SetStartingItems
                },
                {
                    // Operation: new = old * 19
                    OperationRegex,
                    this.SetOperation
                },
                {
                    // Test: divisible by 23
                    TestRegex,
                    this.SetTest
                },
                {
                    // If true: throw to monkey 2
                    TestResultRegex,
                    this.SetTestResult
                },
            };
        }

        /// <summary>
        /// Gets the solution for this day.
        /// </summary>
        /// <returns>The solution for this day.</returns>
        public string GetSolution()
        {
            using StreamReader sr = new("input\\Day11.txt");

            Monkey? monkey = null;

            string? line;
            while (true)
            {
                line = sr.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        // Blank line indicates end of monkey definition.
                        this.monkeys.Add(monkey ?? throw new InvalidDataException());
                    }

                    if (line == null)
                    {
                        // EOF
                        break;
                    }

                    continue;
                }

                if (MonkeyRegex.IsMatch(line))
                {
                    // Monkey 0:
                    monkey = new();
                    continue;
                }

                if (monkey == null)
                {
                    throw new InvalidDataException();
                }

                bool lineParsed = false;
                foreach (var kvp in this.actionMap)
                {
                    Match match = kvp.Key.Match(line);
                    if (match.Success)
                    {
                        kvp.Value(match, monkey);
                        lineParsed = true;
                        break;
                    }
                }

                if (!lineParsed)
                {
                    throw new InvalidDataException();
                }
            }

            return this.GetMonkeyBusinessLevel().ToString();
        }

        private void SetStartingItems(Match match, Monkey monkey)
        {
            monkey.Items = new(match.Groups["ITEMS"].Value.Split(',').Select(n => long.Parse(n.Trim())));
        }

        private void SetOperation(Match match, Monkey monkey)
        {
            string oper = match.Groups["OPERATOR"].Value;
            string valueString = match.Groups["VALUE"].Value;

            monkey.Operation = (old) =>
            {
                long value = valueString == "old" ? old : long.Parse(valueString);
                return oper == "+" ? old + value : old * value;
            };
        }

        private void SetTest(Match match, Monkey monkey)
        {
            int value = int.Parse(match.Groups["VALUE"].Value);
            monkey.Divisor = value;
        }

        private void SetTestResult(Match match, Monkey monkey)
        {
            string condition = match.Groups["CONDITION"].Value;
            int monkeyIndex = int.Parse(match.Groups["MONKEY"].Value);

            if (condition == "true")
            {
                monkey.TestTrueMonkey = monkeyIndex;
            }
            else
            {
                monkey.TestFalseMonkey = monkeyIndex;
            }
        }

        private long GetMonkeyBusinessLevel()
        {
            IEnumerable<long> divisors = this.monkeys.Select(m => m.Divisor);
            long leastCommonMultiple = Calculator.GetLeastCommonMultiple(divisors);

            Dictionary<int, long> activityLevel = Enumerable.Range(0, this.monkeys.Count).ToDictionary(x => x, x => 0L);

            for (int i = 0; i < MaxMonkeyBusinessRounds; i++)
            {
                for (int m = 0; m < this.monkeys.Count; m++)
                {
                    Monkey monkey = this.monkeys[m];
                    activityLevel[m] += monkey.Items.Count;

                    while (monkey.Items.TryDequeue(out long worryLevel))
                    {
                        long newWorryLevel = monkey.Operation(worryLevel) % leastCommonMultiple;
                        int newMonkey = monkey.Test(newWorryLevel) ? monkey.TestTrueMonkey : monkey.TestFalseMonkey;
                        this.monkeys[newMonkey].Items.Enqueue(newWorryLevel);
                    }
                }
            }

            IEnumerable<long> top2 = activityLevel.Values.OrderByDescending(x => x).Take(2);
            return top2.First() * top2.Last();
        }
    }
}
