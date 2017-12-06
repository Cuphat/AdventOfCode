using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017
{
    internal static partial class Program
    {
        private static void Day6()
        {
            const string input = @"11	11	13	7	0	15	5	5	4	4	1	1	7	1	15	11";
            Day6(input);
        }

        private static void Day6(string input)
        {
            var numbers = input.Split("	").Select(int.Parse).ToList();
            Day6(numbers);
        }

        private static void Day6(IEnumerable<int> input)
        {
            var currentConfiguration = input.ToList();

            var configurationsSeen = new List<string>(); // Was a HashSet for part 1, but a List to maintain order for part 2.
            while (!configurationsSeen.Contains(string.Join("	", currentConfiguration)))
            {
                configurationsSeen.Add(string.Join("	", currentConfiguration));

                var highestBankIndex = currentConfiguration.FindIndex(n => n == currentConfiguration.Max());
                var blocks = currentConfiguration[highestBankIndex];
                currentConfiguration[highestBankIndex] = 0;
                for (var i = highestBankIndex + 1; blocks > 0; i++, blocks--)
                {
                    if (i >= currentConfiguration.Count)
                        i = i - currentConfiguration.Count;
                    currentConfiguration[i]++;
                }
            }

            var duplicateIndex = configurationsSeen.FindIndex(c => c == string.Join("	", currentConfiguration));
            var loopSize = configurationsSeen.Count - duplicateIndex;

            Console.WriteLine($"Number of configurations seen before hitting a duplicate: {configurationsSeen.Count}");
            Console.WriteLine($"Loop size, starting with the duplicate: {loopSize}");
        }
    }
}
