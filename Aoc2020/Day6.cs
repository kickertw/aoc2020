using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020
{
    public static class Day6
    {
        public static int Part1(IEnumerable<string> rawInputs, bool forPart2 = false)
        {
            var retVal = 0;
            var newGroup = true;
            var groupAnswers = new HashSet<char>();

            foreach (string input in rawInputs)
            {
                if (input.Length == 0)
                {
                    //Console.WriteLine("");
                    retVal += groupAnswers.Count;
                    groupAnswers.Clear();
                    newGroup = true;
                    continue;
                }

                if (forPart2)
                {
                    if (newGroup)
                    {
                        groupAnswers.UnionWith(input.ToList());
                        //Console.WriteLine($"New Group starting at [{string.Join(",", groupAnswers)}]");
                        newGroup = false;
                    }

                    //Console.Write($"    Before = {string.Join(",", groupAnswers)} / ");
                    groupAnswers.IntersectWith(input.ToList());
                    //Console.WriteLine($"After = {string.Join(",", groupAnswers)} / ");
                }
                else
                {
                    groupAnswers.UnionWith(input.ToList());                    
                }
            }

            return retVal + groupAnswers.Count;
        }

        public static int Part2(IEnumerable<string> rawInputs)
        {
            return Part1(rawInputs, true);
        }
    }
}
