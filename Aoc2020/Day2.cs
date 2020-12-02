using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020
{
    // Day 2: Password Philosophy
    public static class Day2
    {
        // Returns number of valid passwords
        public static int Part1(IEnumerable<string> rawInputs)
        {
            List<PasswordInput> inputs = ConvertInputs(rawInputs);
            var retVal = 0;

            foreach (var input in inputs)
            {
                var count = input.Password.Count(i => i == input.RequiredLetter);
                if (count >= input.Input1 && count <= input.Input2)
                {
                    retVal++;
                }
            }

            return retVal;
        }

        public static int Part2(IEnumerable<string> rawInputs)
        {
            List<PasswordInput> inputs = ConvertInputs(rawInputs);
            var retVal = 0;

            foreach (var input in inputs)
            {
                var check1 = input.Password[input.Input1 - 1] == input.RequiredLetter;
                var check2 = input.Password[input.Input2 - 1] == input.RequiredLetter;
                if ((check1 && !check2) || (!check1 && check2))
                {
                    retVal++;
                }
            }

            return retVal;
        }

        private static List<PasswordInput> ConvertInputs(IEnumerable<string> rawInputs)
        {
            var inputs = new List<PasswordInput>();

            foreach (var rawInput in rawInputs)
            {
                var rawInputSplit = rawInput.Split(' ');
                inputs.Add(new PasswordInput
                {
                    Input1 = int.Parse(rawInputSplit[0].Split('-')[0]),
                    Input2 = int.Parse(rawInputSplit[0].Split('-')[1]),
                    RequiredLetter = rawInputSplit[1][0],
                    Password = rawInputSplit[2]
                });
            }

            return inputs;
        }
    }

    /// <summary>
    /// For Part1, Input1 = Minimum number of times the RequiredLetter needs to appear
    ///            Input2 = Max number of times the RequiredLetter needs to appear
    /// For Part2, Input1 = First position you're checking for the required letter
    ///            Input2 = 2nd position you're checking for the required letter
    /// </summary>
    public class PasswordInput
    {
        public char RequiredLetter { get; set; }
        public int Input1 { get; set; }
        public int Input2 { get; set; }
        public string Password { get; set; }
    }
}
