using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aoc2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = File.ReadLines("inputs/day5.txt");

            Console.WriteLine($"Part 1 answer = {Day5.Part1(inputs)}");
            Console.WriteLine($"Part 2 answer = {Day5.Part2(inputs)}");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}
