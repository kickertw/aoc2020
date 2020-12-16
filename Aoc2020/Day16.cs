using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020
{
    //--- Day 16: Ticket Translation ---
    public static class Day16
    {
        public static int Part1(IEnumerable<string> rawInputs)
        {
            var ticketTracker = new TicketTracker(rawInputs);
            return ticketTracker.GetErrorRate();
        }

        // There is a more efficient way, this is just brute forcing :(
        public static long Part2(IEnumerable<string> rawInputs)
        {
            var ticketTracker = new TicketTracker(rawInputs);
            
            // So we know which tickets are valid
            ticketTracker.GetErrorRate();
            return ticketTracker.GetDepartureAnswer();
        }

        private class TicketTracker
        {
            private List<Range> AcceptableRanges { get; set; }
            private int[] MyTicket { get; set; }
            private List<int[]> NearbyTickets { get; set; }
            private List<int[]> ValidNearbyTickets { get; set; }

            public TicketTracker(IEnumerable<string> rawInputs)
            {
                var section = 1;
                AcceptableRanges = new List<Range>();
                NearbyTickets = new List<int[]>();
                ValidNearbyTickets = new List<int[]>();

                foreach (var rawInput in rawInputs)
                {
                    if (rawInput.Length == 0) section++;

                    switch (section)
                    {
                        case 1:
                        {
                            var rangeSplit = rawInput.Split(": ");
                            var label = rangeSplit.First();
                            var inputs = rangeSplit.Last().Split(' ');
                            var range1 = inputs[0].Split('-').Select(int.Parse).ToList();
                            var range2 = inputs[2].Split('-').Select(int.Parse).ToList();

                            AcceptableRanges.Add(new Range(label, range1[0], range1[1], range2[0], range2[1]));
                            break;
                        }
                        case 2:
                            var ticketInputs = rawInput.Split(',');
                            if (ticketInputs.Length > 1)
                            {
                                MyTicket = ticketInputs.Select(int.Parse).ToArray();
                            }
                            break;
                        default:
                            var nearbyTicketInputs = rawInput.Split(',');
                            if (nearbyTicketInputs.Length > 1)
                            {
                                NearbyTickets.Add(nearbyTicketInputs.Select(int.Parse).ToArray());
                            }
                            break;
                    }
                }
            }

            public int GetErrorRate()
            {
                var retVal = 0;
                foreach (var ticket in NearbyTickets)
                {
                    var isValid = true;
                    foreach (var t in ticket)
                    {
                        if (AcceptableRanges.Any(i =>
                            Enumerable.Range(i.Min1, i.Max1 - i.Min1 + 1).Contains(t) ||
                            Enumerable.Range(i.Min2, i.Max2 - i.Min2 + 1).Contains(t))) continue;
                        retVal += t;
                        isValid = false;
                    }

                    if (isValid) ValidNearbyTickets.Add(ticket);
                }

                return retVal;
            }

            public long GetDepartureAnswer()
            {
                ValidNearbyTickets.Add(MyTicket);

                for (var ii = 0; ii < MyTicket.Count(); ii++)
                {
                    var ticketVals = ValidNearbyTickets.Select(tVal => tVal[ii]).ToList();
                    foreach (var range in AcceptableRanges.Where(range => ticketVals.All(j =>
                        Enumerable.Range(range.Min1, range.Max1 - range.Min1 + 1).Contains(j) ||
                        Enumerable.Range(range.Min2, range.Max2 - range.Min2 + 1).Contains(j))))
                    {
                        range.TicketPositions.Add(ii);
                    }
                }

                // Figure out which ranges belong to which positions
                while (AcceptableRanges.Any(i => i.TicketPositions.Count<int>() != 1))
                {
                    var rangesWithSinglePosition = AcceptableRanges.Where(i => i.TicketPositions.Count() == 1).ToList();
                    var filledPositions = rangesWithSinglePosition.Select(i => i.TicketPositions.First()).ToList();

                    foreach (var val in filledPositions)
                    {
                        foreach (var range in AcceptableRanges.Where(i => i.TicketPositions.Count() > 1 && i.TicketPositions.Contains(val)))
                        {
                            range.TicketPositions.Remove(val);
                        }
                    }
                }


                //foreach (var range in AcceptableRanges.Where(i => i.Label.Contains("departure")))
                //{
                //    retVal *= MyTicket[range.TicketPositions.First()];
                //}

                //return retVal;
                return AcceptableRanges.Where(i => i.Label.Contains("departure")).Aggregate<Range, long>(1, (current, range) => current * MyTicket[range.TicketPositions.First()]);
            }
        }
    }

    internal class Range
    {
        public string Label { get; set; }
        public int Min1 { get; set; }
        public int Max1 { get; set; }
        public int Min2 { get; set; }
        public int Max2 { get; set; }
        public List<int> TicketPositions { get; set; }


        public Range(string label, int min1, int max1, int min2, int max2)
        {
            Label = label;
            Min1 = min1;
            Max1 = max1;
            Min2 = min2;
            Max2 = max2;
            TicketPositions = new List<int>();
        }
    }
}