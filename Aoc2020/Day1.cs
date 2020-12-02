using System.Collections.Generic;
using System.Linq;

namespace Aoc2020
{
    public static class Day1
    {
        public static int Part1(IEnumerable<string> rawInputs, int acceptedSum = 2020)
        {
            var inputs = rawInputs.Select(line => int.Parse(line)).ToList();

            for (var ii = 0; ii < inputs.Count; ii++)
            {
                for (var jj = 0; jj < inputs.Count; jj++)
                {
                    if (jj != ii && inputs[jj] + inputs[ii] == acceptedSum)
                    {
                        return inputs[jj] * inputs[ii];
                    }
                }
            }

            return -1;
        }

        public static int Part2(IEnumerable<string> rawInputs, int acceptedSum = 2020)
        {
            var inputs = rawInputs.Select(line => int.Parse(line)).ToList();

            for (var ii = 0; ii < inputs.Count; ii++)
            {
                for (var jj = 0; jj < inputs.Count; jj++)
                {
                    for (var kk = 0; kk < inputs.Count; kk++)
                    {
                        if (jj != ii && ii != kk && jj != kk && inputs[jj] + inputs[ii] + inputs[kk] == acceptedSum)
                        {
                            return inputs[jj] * inputs[ii] * inputs[kk];
                        }
                    }
                }
            }

            return -1;
        }
    }
}
