using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace Aoc2020
{
    //--- Day 18: Operation Order ---
    public static class Day18
    {
        public static long Part1(IEnumerable<string> rawInputs, bool forP2 = false)
        {
            return rawInputs.Sum(rawInput => Calculate(rawInput, forP2));
        }

        // There is a more efficient way, this is just brute forcing :(
        public static long Part2(IEnumerable<string> rawInputs)
        {
            return Part1(rawInputs, true);
        }

        public static long Calculate(string rawInput, bool forP2)
        {
            while (true)
            {
                var inputs = Regex.Replace(rawInput, @"\s+", "").ToCharArray().ConvertToStringArray();

                // If there aren't any parenthesis, just evaluate it and be done
                if (inputs.All(i => i != "("))
                {
                    return forP2 ? EvaluateP2(inputs) : Evaluate(inputs);
                }

                var openParensIdx = -1;
                var closeParensIdx = -1;
                for (var ii = 0; ii < inputs.Length; ii++)
                {
                    if (inputs[ii] == "(")
                    {
                        openParensIdx = ii;
                    }

                    if (inputs[ii] != ")") continue;

                    closeParensIdx = ii;
                    break;
                }

                var temp = inputs.Skip(openParensIdx + 1).Take(closeParensIdx - openParensIdx - 1).ToArray();
                var tempVal = Calculate(string.Join("", temp), forP2);

                var newInput = inputs.Take(openParensIdx).ToList();
                newInput.Add(tempVal.ToString());
                newInput.AddRange(inputs.Skip(closeParensIdx + 1));
                rawInput = string.Join("", newInput);
            }
        }

        private static string[] ConvertToStringArray(this char[] chars)
        {
            var retVal = new List<string>();

            var tempVal = "";
            foreach (var t in chars)
            {
                if (int.TryParse(t.ToString(), out _))
                {
                    tempVal += t;
                }
                else
                {
                    if (tempVal.Length > 0) retVal.Add(tempVal);
                    tempVal = "";
                    retVal.Add(t.ToString());
                }
            }

            if (tempVal.Length > 0) retVal.Add(tempVal);

            return retVal.ToArray();
        }

        public static long Evaluate(string[] inputs)
        {
            var retVal = 0L;
            for (var ii = 0; ii < inputs.Length;)
            {
                if (ii == 0)
                {
                    retVal = long.Parse(inputs[ii++]);
                }

                var op = inputs[ii++];
                var right = long.Parse(inputs[ii++]);

                switch (op)
                {
                    case "+":
                        retVal += right;
                        break;
                    case "*":
                        retVal *= right;
                        break;
                }
            }

            return retVal;
        }

        public static long EvaluateP2(string[] inputs)
        {
            var inputList = inputs.ToList();

            while (true)
            {
                var multIdx = inputList.IndexOf("+");

                if (inputList.Count == 3 || multIdx == -1) return Evaluate(inputList.ToArray());

                var subProduct = long.Parse(inputList[multIdx - 1]) + long.Parse(inputList[multIdx + 1]);
                
                //rebuild the expression
                var newList = multIdx == 1 ? new List<string>() : inputList.Take(multIdx-1).ToList();
                newList.Add(subProduct.ToString());
                newList.AddRange(inputList.Skip(multIdx + 2));
                inputList = newList;
            }
        }
    }
}