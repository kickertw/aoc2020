using System.Collections.Generic;
using System.Linq;

namespace Aoc2020
{
    // --- Day 9: Encoding Error ---
    public static class Day9
    {
        public static long Part1(IEnumerable<string> rawInputs)
        {
            const int preambleSize = 25;
            var inputs = rawInputs.Select(long.Parse).ToList();

            for (var ii = 0; ii + preambleSize < inputs.Count; ii++)
            {
                var isValid = HasValidPair(inputs.GetRange(ii, preambleSize), inputs[ii + preambleSize]);
                if (!isValid)
                {
                    return inputs[ii + preambleSize];
                }
            }

            return -1;
        }

        public static long Part2(IEnumerable<string> rawInputs)
        {
            var minPos = 0;
            var sumLen = 2;
            var p1Ans = Part1(rawInputs);
            var inputs = rawInputs.Select(long.Parse).ToList();

            while (inputs[sumLen - 1] < p1Ans)
            {
                var sum = inputs.Skip(minPos).Take(sumLen).Sum();

                if (sum > p1Ans)
                {
                    minPos++;
                    sumLen = 2;
                }

                if (sum == p1Ans)
                {
                    //Console.WriteLine($"{inputs[minPos]},{inputs[minPos + sumLen - 1]}");
                    var subset = inputs.GetRange(minPos, sumLen);
                    return subset.Min() + subset.Max();
                }

                sumLen++;
            }

            return -1;
        }

        private static bool HasValidPair(IReadOnlyList<long> preamble, long sum)
        {
            for (var ii = 0; ii < preamble.Count(); ii++)
            {
                for (var jj = 0; jj < preamble.Count(); jj++)
                {
                    if (ii != jj && preamble[ii] + preamble[jj] == sum)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}