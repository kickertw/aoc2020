using System;
using System.IO;

namespace Aoc2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = File.ReadLines("inputs/day19.txt");

            //Console.WriteLine($"Part 1 answer = {Day19.Part1(inputs)}");
            Console.WriteLine($"Part 2 answer = {Day19.Part2(inputs)}");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}
