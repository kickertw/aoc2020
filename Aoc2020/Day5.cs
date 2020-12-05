using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Aoc2020
{
    public static class Day5
    {
        public static int Part1(IEnumerable<string> rawInputs)
        {
            var seatId = 0;

            foreach (var input in rawInputs)
            {
                var tempId = GetSeatId(input);
                seatId = seatId < tempId ? tempId : seatId;
            }

            return seatId;
        }

        public static int Part2(IEnumerable<string> rawInputs)
        {
            var seatIdList = new List<int>();

            foreach (var input in rawInputs)
            {
                seatIdList.Add(GetSeatId(input));
            }

            seatIdList.Sort();
            for(var ii = 0; ii < seatIdList.Count; ii++)
            {
                if (seatIdList[ii] + 2 == seatIdList[ii + 1])
                {
                    return seatIdList[ii] + 1;
                }
            }

            // We should never get here;
            return 0;
        }

        private static int GetSeatId(string input)
        {
            var min = 0;
            var max = 127;
            var row = 0;
            var col = 0;

            foreach (var letter in input)
            {
                if ((letter == 'L' || letter == 'R') && min == max)
                {
                    row = min;
                    min = 0;
                    max = 7;
                }

                switch (letter)
                {
                    case 'F':
                    case 'L':
                        //Console.Write($"max = {max} =>");
                        max -= ((max + 1 - min) / 2);
                        //Console.Write($" {max}");
                        break;                    
                    case 'B':
                    case 'R':
                        //Console.Write($"min = {min} =>");
                        min += ((max + 1 - min) / 2);
                        //Console.Write($"{min}");
                        break;
                }
                //Console.WriteLine($"    min={min} max={max}");
            }

            col = min;

            //Console.WriteLine($"row={row} col={col}");
            //Console.WriteLine("");
            return row * 8 + col;
        }
    }
}
