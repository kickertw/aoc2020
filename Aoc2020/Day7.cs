using System.Collections.Generic;
using System.Linq;

namespace Aoc2020
{
    public static class Day7
    {
        public static HashSet<Bag> RootBags = new HashSet<Bag>();
        public static HashSet<Bag> OuterBags = new HashSet<Bag>();

        public static int Part1(IEnumerable<string> rawInputs, string keyColor = "shiny gold")
        {
            CreateBagList(rawInputs);

            var outerBagSize = 0;
            OuterBags.UnionWith(RootBags.Where(i => i.ChildBags != null && i.ChildBags.Any(j => j.Color == keyColor)));
            while (outerBagSize != OuterBags.Count())
            {
                var tempList = new HashSet<Bag>();
                outerBagSize = OuterBags.Count();
                foreach (var outerBag in OuterBags)
                {
                    tempList.UnionWith(RootBags.Where(i => i.ChildBags != null && i.ChildBags.Any(j => j.Color == outerBag.Color)));
                }

                OuterBags.UnionWith(tempList);
            }

            return OuterBags.Count();
        }

        public static int Part2(IEnumerable<string> rawInputs, string keyColor = "shiny gold")
        {
            if (!RootBags.Any())
            {
                CreateBagList(rawInputs);
            }

            var rootBag = RootBags.FirstOrDefault(i => i.Color == keyColor);
            return rootBag?.CountChildren(RootBags) ?? 0;
        }

        private static void CreateBagList(IEnumerable<string> rawInputs)
        {
            foreach (var input in rawInputs)
            {
                var bag = new Bag
                {
                    Color = input.Substring(0, input.IndexOf("bags contain") - 1)
                };

                var childBagInputs = input.Substring(input.IndexOf("bags contain") + 13).Split(',');
                foreach (var rawChildInput in childBagInputs)
                {
                    if (rawChildInput == "no other bags.") continue;
                    bag.ChildBags ??= new HashSet<Bag>();

                    var childInput = rawChildInput.Trim().Split(' ');
                    bag.ChildBags.Add(new Bag()
                    {
                        Color = childInput[1] + " " + childInput[2],
                        Qty = int.Parse(childInput[0])
                    });
                }

                RootBags.Add(bag);
            }
        }
    }

    public class Bag
    {
        public string Color { get; set; }
        public int Qty { get; set; }

        public HashSet<Bag> ChildBags { get; set; }

        public int CountChildren(HashSet<Bag> rootBags)
        {
            var retVal = 0;

            if (ChildBags == null) return retVal;

            foreach (var child in ChildBags)
            {
                var realChild = rootBags.FirstOrDefault(i => i.Color == child.Color);
                retVal += child.Qty + (child.Qty * (realChild?.CountChildren(rootBags) ?? 0));
            }

            return retVal;
        }
    }
}
