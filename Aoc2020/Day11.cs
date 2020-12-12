using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020
{
    // --- Day 11: Seating System ---
    public static class Day11
    {
        public static int Part1(IEnumerable<string> rawInputs)
        {
            var hasSeatingChanged = true;
            var seating = CreateLayout(rawInputs);

            while (hasSeatingChanged)
            {
                // initially assume nothing has changed
                hasSeatingChanged = false;
                var newSeating = seating.Select(a => a.ToArray()).ToArray(); //proper clone

                for (var row = 0; row < seating.Length; row++)
                {
                    for (var col = 0; col < seating[row].Length; col++)
                    {
                        switch (seating[row][col])
                        {
                            case 'L' when CanSit(seating, row, col):
                                newSeating[row][col] = '#';
                                hasSeatingChanged = true;
                                break;
                            case '#' when ShouldStand(seating, row, col):
                                newSeating[row][col] = 'L';
                                hasSeatingChanged = true;
                                break;
                        }
                    }
                }

                seating = newSeating;
            }

            return seating.Sum(row => row.Count(i => i == '#'));
        }

        public static long Part2(IEnumerable<string> rawInputs)
        {
            var hasSeatingChanged = true;
            var seating = CreateLayout(rawInputs);

            while (hasSeatingChanged)
            {
                // initially assume nothing has changed
                hasSeatingChanged = false;
                var newSeating = seating.Select(a => a.ToArray()).ToArray(); //proper clone

                for (var row = 0; row < seating.Length; row++)
                {
                    for (var col = 0; col < seating[row].Length; col++)
                    {
                        switch (seating[row][col])
                        {
                            case 'L' when CanSitP2(seating, row, col):
                                newSeating[row][col] = '#';
                                hasSeatingChanged = true;
                                break;
                            case '#' when ShouldStandP2(seating, row, col, 5):
                                newSeating[row][col] = 'L';
                                hasSeatingChanged = true;
                                break;
                        }
                    }
                }

                seating = newSeating;
            }

            return seating.Sum(row => row.Count(i => i == '#'));
        }

        private static char[][] CreateLayout(IEnumerable<string> rawInputs)
        {
            var y = 0;
            var inputs = rawInputs.ToList();
            var retVal = new char[inputs.Count][];

            foreach (var input in inputs)
            {
                retVal[y] = input.ToCharArray();
                y++;
            }

            return retVal;
        }

        private static bool CanSit(char[][] seatingChart, int row, int col)
        {
            var emptyChar = new char[] { 'L', '.' };
            var maxRow = seatingChart.Length - 1;
            var maxCol = seatingChart.First().Length - 1;

            // If true, the adjacent seat is empty
            var topLeft = row == 0 || col == 0 || emptyChar.Contains(seatingChart[row - 1][col - 1]);
            var topMid = row == 0 || emptyChar.Contains(seatingChart[row - 1][col]);
            var topRight = row == 0 || col == maxCol || emptyChar.Contains(seatingChart[row - 1][col + 1]);
            var left = col == 0 || emptyChar.Contains(seatingChart[row][col - 1]);
            var right = col == maxCol || emptyChar.Contains(seatingChart[row][col + 1]);
            var botLeft = row == maxRow || col == 0 || emptyChar.Contains(seatingChart[row + 1][col - 1]);
            var botMid = row == maxRow || emptyChar.Contains(seatingChart[row + 1][col]);
            var botRight = row == maxRow || col == maxCol || emptyChar.Contains(seatingChart[row + 1][col + 1]);

            return topLeft && topMid && topRight && left && right && botLeft && botMid && botRight;
        }

        private static bool ShouldStand(char[][] seatingChart, int row, int col)
        {
            var emptyChar = new char[] { 'L', '.' };
            var maxRow = seatingChart.Length - 1;
            var maxCol = seatingChart.First().Length - 1;

            // If true, the adjacent seat is empty
            var topLeft = row == 0 || col == 0 || emptyChar.Contains(seatingChart[row - 1][col - 1]);
            var topMid = row == 0 || emptyChar.Contains(seatingChart[row - 1][col]);
            var topRight = row == 0 || col == maxCol || emptyChar.Contains(seatingChart[row - 1][col + 1]);
            var left = col == 0 || emptyChar.Contains(seatingChart[row][col - 1]);
            var right = col == maxCol || emptyChar.Contains(seatingChart[row][col + 1]);
            var botLeft = row == maxRow || col == 0 || emptyChar.Contains(seatingChart[row + 1][col - 1]);
            var botMid = row == maxRow || emptyChar.Contains(seatingChart[row + 1][col]);
            var botRight = row == maxRow || col == maxCol || emptyChar.Contains(seatingChart[row + 1][col + 1]);

            var adjCount = (topLeft ? 0 : 1) + 
                           (topMid ? 0 : 1) +
                           (topRight ? 0 : 1) +
                           (left ? 0 : 1) +
                           (right ? 0 : 1) +
                           (botLeft ? 0 : 1) +
                           (botMid ? 0 : 1) +
                           (botRight ? 0 : 1);

            return adjCount >= 4;
        }

        private static bool CanSitP2(char[][] seatingChart, int row, int col)
        {
            var emptyChar = new char[] { 'L', '.' };
            var maxRow = seatingChart.Length - 1;
            var maxCol = seatingChart.First().Length - 1;

            // If true, the adjacent seat is empty
            var topLeft = row == 0 || col == 0 || emptyChar.Contains(NextVisibleSeat(seatingChart, row, col, Direction.TopLeft));
            var topMid = row == 0 || emptyChar.Contains(NextVisibleSeat(seatingChart, row, col, Direction.TopMid));
            var topRight = row == 0 || col == maxCol || emptyChar.Contains(NextVisibleSeat(seatingChart, row, col, Direction.TopRight));
            var left = col == 0 || emptyChar.Contains(NextVisibleSeat(seatingChart, row, col, Direction.Left));
            var right = col == maxCol || emptyChar.Contains(NextVisibleSeat(seatingChart, row, col, Direction.Right));
            var botLeft = row == maxRow || col == 0 || emptyChar.Contains(NextVisibleSeat(seatingChart, row, col, Direction.BotLeft));
            var botMid = row == maxRow || emptyChar.Contains(NextVisibleSeat(seatingChart, row, col, Direction.BotMid));
            var botRight = row == maxRow || col == maxCol || emptyChar.Contains(NextVisibleSeat(seatingChart, row, col, Direction.BotRight));

            return topLeft && topMid && topRight && left && right && botLeft && botMid && botRight;
        }

        private static bool ShouldStandP2(char[][] seatingChart, int row, int col, int threshHold = 4)
        {
            var emptyChar = new char[] { 'L', '.' };
            var maxRow = seatingChart.Length - 1;
            var maxCol = seatingChart.First().Length - 1;

            // If true, the adjacent seat is empty
            var topLeft = row == 0 || col == 0 || emptyChar.Contains(NextVisibleSeat(seatingChart, row, col, Direction.TopLeft));
            var topMid = row == 0 || emptyChar.Contains(NextVisibleSeat(seatingChart, row, col, Direction.TopMid));
            var topRight = row == 0 || col == maxCol || emptyChar.Contains(NextVisibleSeat(seatingChart, row, col, Direction.TopRight));
            var left = col == 0 || emptyChar.Contains(NextVisibleSeat(seatingChart, row, col, Direction.Left));
            var right = col == maxCol || emptyChar.Contains(NextVisibleSeat(seatingChart, row, col, Direction.Right));
            var botLeft = row == maxRow || col == 0 || emptyChar.Contains(NextVisibleSeat(seatingChart, row, col, Direction.BotLeft));
            var botMid = row == maxRow || emptyChar.Contains(NextVisibleSeat(seatingChart, row, col, Direction.BotMid));
            var botRight = row == maxRow || col == maxCol || emptyChar.Contains(NextVisibleSeat(seatingChart, row, col, Direction.BotRight));

            var adjCount = (topLeft ? 0 : 1) +
                           (topMid ? 0 : 1) +
                           (topRight ? 0 : 1) +
                           (left ? 0 : 1) +
                           (right ? 0 : 1) +
                           (botLeft ? 0 : 1) +
                           (botMid ? 0 : 1) +
                           (botRight ? 0 : 1);

            return adjCount >= threshHold;
        }

        private static char NextVisibleSeat(char[][] seatingChart, int row, int col, Direction direction)
        {
            var maxRow = seatingChart.Length - 1;
            var maxCol = seatingChart.First().Length - 1;

            while (true)
            {
                switch (direction)
                {
                    case Direction.TopLeft:
                        if (row == 0 || col == 0) return '.';
                        if (seatingChart[row - 1][col - 1] != '.') return seatingChart[row - 1][col - 1];
                        row--;
                        col--;
                        continue;
                    case Direction.TopMid:
                        if (row == 0) return '.';
                        if (seatingChart[row - 1][col] != '.') return seatingChart[row - 1][col];
                        row--;
                        continue;
                    case Direction.TopRight:
                        if (row == 0 || col == maxCol) return '.';
                        if (seatingChart[row - 1][col + 1] != '.') return seatingChart[row - 1][col + 1];
                        row--;
                        col++;
                        continue;
                    case Direction.Left:
                        if (col == 0) return '.';
                        if (seatingChart[row][col - 1] != '.') return seatingChart[row][col - 1];
                        col--;
                        continue;
                    case Direction.Right:
                        if (col == maxCol) return '.';
                        if (seatingChart[row][col + 1] != '.') return seatingChart[row][col + 1];
                        col++;
                        continue;
                    case Direction.BotLeft:
                        if (row == maxRow || col == 0) return '.';
                        if (seatingChart[row + 1][col - 1] != '.') return seatingChart[row + 1][col - 1];
                        row++;
                        col--;
                        continue;
                    case Direction.BotMid:
                        if (row == maxRow) return '.';
                        if (seatingChart[row + 1][col] != '.') return seatingChart[row + 1][col];
                        row++;
                        continue;
                    case Direction.BotRight:
                        if (row == maxRow || col == maxCol) return '.';
                        if (seatingChart[row + 1][col + 1] != '.') return seatingChart[row + 1][col + 1];
                        row++;
                        col++;
                        continue;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                }
            }
        }

        private enum Direction
        {
            TopLeft,
            TopMid,
            TopRight,
            Left,
            Right,
            BotLeft,
            BotMid,
            BotRight
        }
    }
}