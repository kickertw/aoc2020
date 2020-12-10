using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020
{
    // --- Day 10: Adapter Array ---
    public static class Day10
    {
        public static int Part1(IEnumerable<string> rawInputs)
        {
            var differences = new List<int>() { 0, 0, 0 };
            var inputs = GetSortedInputs(rawInputs);

            var currentJoltage = 0;
            foreach (var input in inputs.Where(input => input > 0))
            {
                differences[input - currentJoltage - 1]++;
                currentJoltage = input;
            }

            Console.WriteLine(string.Join(',',differences));
            return differences[0] * differences[2];
        }

        public static long Part2(IEnumerable<string> rawInputs)
        {
            var parsedInput = GetSortedInputs(rawInputs);
            var permCount = new Dictionary<int, long>();

            for (var ii = 0; ii < parsedInput.Count(); ii++)
            {
                Console.Write($"Current Value = {parsedInput[ii]} - ");
                var currentPerms = 0L;
                if (ii - 3 >= 0 && parsedInput[ii] - 3 <= parsedInput[ii - 3])
                {
                    currentPerms += permCount[ii - 3];
                }

                if (ii - 2 >= 0 && parsedInput[ii] - 3 <= parsedInput[ii - 2])
                {
                    currentPerms += permCount[ii - 2];
                }

                if (ii - 1 >= 0 && parsedInput[ii] - 3 <= parsedInput[ii - 1])
                {
                    currentPerms += permCount[ii - 1];
                }

                permCount[ii] = currentPerms == 0 ? 1 : currentPerms;
                Console.WriteLine($"Perms = {permCount[ii]}");
            }

            return permCount.Last().Value;
        }

        private static List<int> GetSortedInputs(IEnumerable<string> rawInputs)
        {
            var inputs = rawInputs.Select(int.Parse).ToList();
            inputs.Add(0);
            inputs.Add(inputs.Max() + 3);
            inputs.Sort();

            return inputs;
        }
    }
}