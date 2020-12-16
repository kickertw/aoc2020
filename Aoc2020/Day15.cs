using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020
{
    //--- Day 15: Rambunctious Recitation ---
    public static class Day15
    {
        public static int Part1(IEnumerable<string> rawInputs, int turnToGet = 2020)
        {
            var idx = 1;
            var inputs = rawInputs.First().Split(',');
            var spokenNumberCounter = inputs.ToDictionary(input => int.Parse(input), input => new SpokenNumberTracker(idx++));

            var lastSpokenNumber = int.Parse(inputs.Last());
            for (var ii = spokenNumberCounter.Count() + 1; ii <= turnToGet; ii++)
            {
                if (spokenNumberCounter.ContainsKey(lastSpokenNumber))
                {
                    if (spokenNumberCounter[lastSpokenNumber].LastTurnSpoken == spokenNumberCounter[lastSpokenNumber].LastLastTurnSpoken)
                    {
                        lastSpokenNumber = 0;
                    }
                    else
                    {
                        lastSpokenNumber = spokenNumberCounter[lastSpokenNumber].LastTurnSpoken -
                                           spokenNumberCounter[lastSpokenNumber].LastLastTurnSpoken;
                    }
                }

                if (spokenNumberCounter.ContainsKey(lastSpokenNumber))
                {
                    spokenNumberCounter[lastSpokenNumber].LastLastTurnSpoken = spokenNumberCounter[lastSpokenNumber].LastTurnSpoken;
                    spokenNumberCounter[lastSpokenNumber].LastTurnSpoken = ii;
                }
                else
                {
                    spokenNumberCounter.Add(lastSpokenNumber, new SpokenNumberTracker(ii));
                }
            }

            return lastSpokenNumber;
        }

        // There is a more efficient way, this is just brute forcing :(
        public static long Part2(IEnumerable<string> rawInputs)
        {
            return Part1(rawInputs, 30000000);
        }
    }

    public class SpokenNumberTracker
    {
        public int LastTurnSpoken { get; set; }
        public int LastLastTurnSpoken { get; set; }

        public SpokenNumberTracker(int lastTurnSpoken)
        {
            LastTurnSpoken = lastTurnSpoken;
            LastLastTurnSpoken = lastTurnSpoken;
        }

        public void UpdateLastTurn(int currentTurn)
        {
            LastLastTurnSpoken = LastTurnSpoken;
            LastTurnSpoken = currentTurn;
        }
    }
}