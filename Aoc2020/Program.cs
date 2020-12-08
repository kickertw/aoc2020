using System;
using System.IO;

namespace Aoc2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = File.ReadLines("inputs/day8.txt");

            Console.WriteLine($"Part 1 answer = {Day8.Part1(inputs)}");
            Console.WriteLine($"Part 2 answer = {Day8.Part2(inputs)}");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}
