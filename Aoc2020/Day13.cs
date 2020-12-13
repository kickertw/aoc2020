using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Aoc2020
{
    //--- Day 13: Shuttle Search ---
    public static class Day13
    {
        public static long Part1(IEnumerable<string> rawInputs)
        {
            var inputs = rawInputs.ToList();
            var earliestDepartureTime = int.Parse(inputs[0]);
            var busIds = GetBusIds(inputs[1]);

            var closestTime = long.MaxValue;
            var earliestBus = 0;
            foreach(var busId in busIds)
            {
                var nextAvailableTime = ClosestMultiple(earliestDepartureTime, busId);
                if (nextAvailableTime < closestTime)
                {
                    closestTime = nextAvailableTime;
                    earliestBus = busId;
                }
            }

            return earliestBus * (closestTime - earliestDepartureTime);
        }

        // There is a more efficient way, this is just brute forcing :(
        public static long Part2(IEnumerable<string> rawInputs)
        {
            var inputs = rawInputs.ToList();
            var busIds = GetBusIdsP2(inputs[1]);
            var startingPoint = 402550000000059;

            var maxBus = busIds.First(i => i.Key == busIds.Keys.Max());
            long ii = ClosestMultiple(startingPoint, maxBus.Key) - maxBus.Key;
            var maxBusOffset = maxBus.Value;
            var searchList = busIds.Where(i => i.Key != maxBus.Key);

            long checkInterval = 10000000000;
            long check = startingPoint + checkInterval;

            while (true)
            {
                var lastIndex = ii;
                foreach (var busId in searchList)
                {
                    var multiplier = busId.Key;
                    var offset = busId.Value;

                    if ((ii - maxBusOffset + offset) % multiplier != 0)
                    {
                        lastIndex = ii;
                        ii += maxBus.Key;
                        break;
                    }
                }

                if (lastIndex == ii)
                {
                    return ii - maxBusOffset;
                }
            }
        }

        private static HashSet<int> GetBusIds(string rawIds)
        {
            var busIds = new HashSet<int>();

            foreach (var busId in rawIds.Split(','))
            {
                if (busId != "x")
                {
                    busIds.Add(int.Parse(busId));
                }
            }

            return busIds;
        }

        private static Dictionary<int, int> GetBusIdsP2(string rawIds)
        {
            var offset = 0;
            var busIds = new Dictionary<int, int>();

            foreach (var busId in rawIds.Split(','))
            {
                if (busId != "x")
                {
                    busIds.Add(int.Parse(busId), offset);
                }

                offset++;
            }

            return busIds;
        }

        private static long ClosestMultiple(long n, long x)
        {
            if (x > n)
                return x;

            if (n % x == 0)
                return n;

            var mult = (n / x) + 1;
            return x * mult;
        }
    }
}