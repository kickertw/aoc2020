using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Aoc2020
{
    public static class Day3
    {
        public static int Part1(IEnumerable<string> rawInputs, Point? slope = null)
        {
            var treeHitCounter = 0;
            slope ??= new Point(3, 1);
            var treeMap = rawInputs.ToList();

            var currentLocation = new Point(0, 0);
            currentLocation.X += slope.Value.X;
            currentLocation.Y += slope.Value.Y;

            while (currentLocation.Y < treeMap.Count)
            {
                var rowToSearch = treeMap[currentLocation.Y];
                while (rowToSearch.Length < currentLocation.X+1)
                {
                    // This is not efficient, but should get the job done
                    rowToSearch += rowToSearch;
                }

                //Console.WriteLine($"Current Location = [{currentLocation.X},{currentLocation.Y}]");
                if (rowToSearch[currentLocation.X] == '#')
                {
                    //Console.WriteLine($"Current Location = [{currentLocation.X},{currentLocation.Y}]");
                    treeHitCounter++;
                }

                currentLocation.X += slope.Value.X;
                currentLocation.Y += slope.Value.Y;
            }

            return treeHitCounter;
        }

        public static long Part2(IEnumerable<string> rawInputs)
        {
            var p1 = Part1(rawInputs, new Point(1, 1));
            var p2 = Part1(rawInputs, new Point(3, 1));
            var p3 = Part1(rawInputs, new Point(5, 1));
            var p4 = Part1(rawInputs, new Point(7, 1));
            var p5 = Part1(rawInputs, new Point(1, 2));

            return (long) p1 * (long) p2 * (long) p3 * (long) p4 * (long) p5;
        }
    }

}
