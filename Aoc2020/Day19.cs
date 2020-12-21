using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace Aoc2020
{
    //--- Day 19: Monster Messages ---
    public static class Day19
    {
        public static int Part1(IEnumerable<string> rawInputs)
        {
            var rules = new Dictionary<int, string>();
            var allowedPatterns = new HashSet<string>();
            var messages = new List<string>();

            // Parse Inputs
            var parseForRules = true;
            foreach (var rawInput in rawInputs)
            {
                if (rawInput.Length == 0)
                {
                    parseForRules = false;
                    continue;
                }

                if (parseForRules)
                {
                    var ruleset = rawInput.Split(": ");
                    var ruleId = int.Parse(ruleset[0]);
                    rules.Add(ruleId, ruleset[1]);
                }
                else
                {
                    messages.Add(rawInput);
                }
            }

            rules = SimplifyRuleSet(rules);

            // Generate all possible combinations
            allowedPatterns = GetAllowedPatterns(rules);

            var retVal = 0;
            foreach(var message in messages)
            {
                if (allowedPatterns.Contains(message)) retVal++;
            }

            return retVal;
        }

        // There is a more efficient way, this is just brute forcing :(
        public static long Part2(IEnumerable<string> rawInputs)
        {
            return 0;
        }

        private static HashSet<string> GetAllowedPatterns(Dictionary<int, string> allRules)
        {
            var retVal = new HashSet<string>();
            var workingList = new HashSet<string>();
            workingList.Add(allRules[0]);

            while (workingList.Any())
            {                
                var oldRule = workingList.First();
                if (Regex.IsMatch(oldRule, @"^[ab ]*$"))
                {
                    retVal.Add(Regex.Replace(oldRule, @"\s+", ""));
                    workingList.Remove(oldRule);
                    continue;
                }

                // Find the first rule and replace it
                // If there's a "|" we need to permuate and add to the working list
                var splitRule = oldRule.Split(' ');
                for (var ii = 0; ii < splitRule.Length;)
                {
                    if (int.TryParse(splitRule[ii], out int nextRule))
                    {
                        // check for "|" and if it exists, we need to add permutations
                        if (allRules[nextRule].Contains('|'))
                        {
                            var nextRuleSplit = allRules[nextRule].Split(" | ");
                            splitRule[ii] = nextRuleSplit[0];

                            var temp = string.Join(" ", splitRule);
                            if (Regex.IsMatch(temp, @"^[ab ]*$"))
                            {
                                retVal.Add(Regex.Replace(temp, @"\s+", ""));
                            }
                            else
                            {
                                workingList.Add(temp);
                            }
                            
                            splitRule[ii] = nextRuleSplit[1];
                            temp = string.Join(" ", splitRule);
                            if (Regex.IsMatch(temp, @"^[ab ]*$"))
                            {
                                retVal.Add(Regex.Replace(temp, @"\s+", ""));
                            }
                            else
                            {
                                workingList.Add(temp);
                            }

                            workingList.Remove(oldRule);
                        }
                        else
                        {
                            splitRule[ii] = allRules[nextRule].Replace("\"", "");
                            var temp = string.Join(" ", splitRule);
                            if (Regex.IsMatch(temp, @"^[ab ]*$"))
                            {
                                retVal.Add(Regex.Replace(temp, @"\s+", ""));
                            }
                            else
                            {
                                workingList.Add(temp);
                            }

                            workingList.Remove(oldRule);
                        }
                        break;
                    }
                    else
                    {
                        ii++;
                    }
                }
            }

            return retVal;
        }

        /// <summary>
        /// Find rules with no Pipes
        /// </summary>
        /// <param name="allRules"></param>
        /// <returns></returns>
        private static Dictionary<int, string> SimplifyRuleSet(Dictionary<int, string> allRules)
        {
            var aIndex = allRules.First(x => x.Value == "\"a\"").Key;
            var bIndex = allRules.First(x => x.Value == "\"b\"").Key;
            for (var ii = 0; ii < allRules.Count(); ii++)
            {
                if (ii != aIndex && ii != bIndex)
                {
                    var splitRule = allRules[ii].Split(" ");

                    for (var jj = 0; jj < splitRule.Length; jj++)
                    {
                        if (splitRule[jj] == aIndex.ToString())
                        {
                            splitRule[jj] = "a";
                        }

                        if (splitRule[jj] == bIndex.ToString())
                        {
                            splitRule[jj] = "b";
                        }
                    }
                    allRules[ii] = string.Join(" ", splitRule);
                }
            }

            allRules.Remove(aIndex);
            allRules.Remove(bIndex);

            while (!allRules.Where(i => i.Key > 0).All(i => i.Value.Contains("|")))
            {
                var ruleToRemove = new List<int>();
                var noPermRuleIds = allRules.Where(i => !i.Value.Split(' ').Any(j => j == "|") && i.Key > 0).Select(i => i.Key).ToList();
                foreach (var nprId in noPermRuleIds)
                {
                    var newVal = allRules[nprId];
                    var rulesToFix = allRules.Where(i => i.Value.Split(' ').Any(j => j == nprId.ToString()) && i.Key != nprId).Select(i => i.Key).ToList();

                    foreach (var rtfId in rulesToFix)
                    {
                        var splitRule = allRules[rtfId].Split(" ");
                        for (var jj = 0; jj < splitRule.Length; jj++)
                        {
                            if (splitRule[jj] == nprId.ToString())
                            {
                                splitRule[jj] = newVal;
                            }
                        }
                        allRules[rtfId] = string.Join(" ", splitRule);
                    }

                    allRules.Remove(nprId);
                }
            }

            return allRules;
        }
    }
}