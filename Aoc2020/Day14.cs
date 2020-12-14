using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020
{
    //--- Day 14: Docking Data ---
    public static class Day14
    {
        public static long Part1(IEnumerable<string> rawInputs)
        {
            var bitMask = string.Empty;
            var memory = new Dictionary<int, string>();

            foreach (var input in rawInputs)
            {
                if (input.StartsWith("mask"))
                {
                    bitMask = input.Substring(7);
                }
                else
                {
                    var memVals = input.Split(" = ");
                    var memIdx = int.Parse(memVals[0].Substring(4, memVals[0].Length - 5));
                    var memVal = long.Parse(memVals[1]);

                    if (!memory.ContainsKey(memIdx))
                    {
                        memory.Add(memIdx, string.Empty);
                    }

                    memory[memIdx] = ApplyMask(bitMask, memVal);
                }

            }

            return memory.Sum(i => ConvertToLong(i.Value));
        }

        // There is a more efficient way, this is just brute forcing :(
        public static long Part2(IEnumerable<string> rawInputs)
        {
            var bitMask = string.Empty;
            var memory = new Dictionary<long, long>();

            foreach (var input in rawInputs)
            {
                if (input.StartsWith("mask"))
                {
                    bitMask = input.Substring(7);
                }
                else
                {
                    var memVals = input.Split(" = ");
                    var memIdx = int.Parse(memVals[0].Substring(4, memVals[0].Length - 5));
                    var memVal = long.Parse(memVals[1]);

                    var result = ApplyMaskForP2(bitMask, memIdx);

                    var memLocations = GetPossibleMemoryLocations(result);
                    foreach (var location in memLocations)
                    {
                        var idx = ConvertToLong(location);
                        memory[idx] = memVal;
                    }
                }

            }

            return memory.Sum(i => i.Value);
        }


        private static string ConvertToBinary(long number, int maskLen = 36)
        {
            return Convert.ToString(number, 2).PadLeft(maskLen, '0');
        }

        private static long ConvertToLong(string number)
        {
            return Convert.ToInt64(number, 2);
        }

        private static string ApplyMask(string mask, long nextValue, char keyValForUnchanged = 'X')
        {
            var retVal = "";
            var binaryVal = ConvertToBinary(nextValue);

            for (var ii = 0; ii < mask.Length; ii++)
            {
                if (mask[ii] == keyValForUnchanged) retVal += binaryVal[ii];
                else retVal += mask[ii];
            }

            return retVal;
        }

        private static string ApplyMaskForP2(string mask, long nextValue)
        {
            return ApplyMask(mask, nextValue, '0');
        }

        private static IEnumerable<string> GetPossibleMemoryLocations(string input)
        {
            var retVal = new List<string>();
            var floatingBitLocations = new HashSet<int>();

            // Get locations of floating bits
            for (var ii = 0; ii < input.Length; ii++)
            {
                if (input[ii] == 'X')
                {
                    floatingBitLocations.Add(ii);
                }
            }

            for (var ii = 0; ii < Math.Pow(2, floatingBitLocations.Count()); ii++)
            {
                var newInput = "";
                var permutation = ConvertToBinary(ii, floatingBitLocations.Count());

                foreach (var x in input.ToCharArray())
                {
                    if (x == 'X')
                    {
                        newInput += permutation.First();
                        if (permutation.Length > 1)
                        {
                            permutation = permutation.Substring(1);
                        }
                    }
                    else
                    {
                        newInput += x;
                    }
                }

                retVal.Add(newInput);
            }

            return retVal;
        }
    }
}