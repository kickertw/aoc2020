using System.Collections.Generic;
using System.Linq;

namespace Aoc2020
{
    public static class Day8
    {
        public static int Part1(IEnumerable<string> rawInputs)
        {
            var instructions = rawInputs.ToList();
            return GetTheAnswer(instructions).Key;
        }

        public static int Part2(IEnumerable<string> rawInputs)
        {
            for (var ii = 0; ii < rawInputs.Count(); ii++)
            {
                var instructions = rawInputs.ToList();

                var instruction = instructions[ii].Split(' ');
                if (instruction[0] == "acc")
                {
                    continue;
                }


                if (instruction[0] == "nop")
                {
                    instructions[ii] = "jmp " + instruction[1];
                }
                else if (instruction[0] == "jmp")
                {
                    instructions[ii] = "nop " + instruction[1];
                }

                var (key, value) = GetTheAnswer(instructions);

                if (value)
                {
                    return key;
                }
            }

            return 0;
        }

        private static KeyValuePair<int, bool> GetTheAnswer(List<string> instructions)
        {
            var cursor = 0;
            var accumulator = 0;
            var instructionsVisited = new HashSet<int>();

            while (cursor < instructions.Count)
            {
                var instruction = instructions[cursor].Split(' ');

                var isAdded = instructionsVisited.Add(cursor);
                if (!isAdded)
                {
                    return new KeyValuePair<int, bool>(accumulator, false);
                }

                switch (instruction[0])
                {
                    case "nop":
                        cursor++;
                        break;
                    case "acc":
                        accumulator += int.Parse(instruction[1]);
                        cursor++;
                        break;
                    case "jmp":
                        cursor += int.Parse(instruction[1]);
                        break;
                }
            }

            // We should never get here;
            return new KeyValuePair<int, bool>(accumulator, true);
        }
    }
}