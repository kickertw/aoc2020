using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Aoc2020
{
    public static class Day1
    {
        public static int Part1(List<int> inputs, int acceptedSum = 2020)
        {
            for(var ii = 0; ii < inputs.Count; ii++)
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

        public static int Part2(List<int> inputs, int acceptedSum = 2020)
        {
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
