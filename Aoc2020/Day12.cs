using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Aoc2020
{
    //--- Day 12: Rain Risk ---
    public static class Day12
    {
        public static int Part1(IEnumerable<string> rawInputs)
        {
            var ferry = new Point(0, 0);
            var facingDirection = 'E';

            foreach (var input in rawInputs)
            {
                var qty = int.Parse(input.Substring(1));

                switch (input[0])
                {
                    case 'N':
                    case 'S':
                    case 'E':
                    case 'W':
                    case 'F':
                        var dir = input[0] == 'F' ? facingDirection : input[0];
                        MovePoint(ref ferry, dir, qty);
                        break;
                    case 'L':
                    case 'R':
                        RotateFerry(ref facingDirection, input[0], qty);
                        break;
                }
            }

            return Math.Abs(ferry.X) + Math.Abs(ferry.Y);
        }

        public static int Part2(IEnumerable<string> rawInputs)
        {
            var ferry = new Point(0, 0);
            var wayPoint = new Point(10, 1);

            foreach (var input in rawInputs)
            {
                var qty = int.Parse(input.Substring(1));

                switch (input[0])
                {
                    case 'N':
                    case 'S':
                    case 'E':
                    case 'W':
                        MovePoint(ref wayPoint, input[0], qty);
                        break;
                    case 'L':
                    case 'R':
                        while (qty > 0)
                        {
                            wayPoint = RotateWayPoint(ferry, wayPoint.X - ferry.X, wayPoint.Y - ferry.Y, input[0], qty > 180 ? 180 : qty);
                            qty = qty <= 180 ? 0 : qty - 180;
                        }
                        
                        break;
                    case 'F':
                        var x = wayPoint.X - ferry.X;
                        var y = wayPoint.Y - ferry.Y;

                        for(var ii = 0; ii < qty; ii++)
                        {
                            MovePoint(ref ferry, 'E', x);
                            MovePoint(ref ferry, 'N', y);
                        }

                        wayPoint.X = ferry.X + x;
                        wayPoint.Y = ferry.Y + y;
                        break;
                }
            }

            return Math.Abs(ferry.X) + Math.Abs(ferry.Y);
        }

        private static void MovePoint(ref Point point, char direction, int distance)
        {
            switch (direction)
            {
                case 'N':
                    point.Y += distance;
                    break;
                case 'S':
                    point.Y -= distance;
                    break;
                case 'E':
                    point.X += distance;
                    break;
                case 'W':
                    point.X -= distance;
                    break;
            }
        }

        private static void RotateFerry(ref char facing, char direction, int degrees)
        {
            while (degrees > 0)
            {
                switch (facing)
                {
                    case 'N':
                        facing = direction == 'L' ? 'W' : 'E';
                        break;
                    case 'S':
                        facing = direction == 'L' ? 'E' : 'W';
                        break;
                    case 'E':
                        facing = direction == 'L' ? 'N' : 'S';
                        break;
                    case 'W':
                        facing = direction == 'L' ? 'S' : 'N';
                        break;
                }

                degrees -= 90;
            }
        }

        private static Point RotateWayPoint(Point centerPoint, int oldXDiff, int oldYDiff, char direction, int qty)
        {
            var newX = 0;
            var newY = 0;

            switch (qty)
            {
                case 90:
                    newX = direction == 'R' ? oldYDiff : oldYDiff * -1;
                    newY = direction == 'R' ? oldXDiff * -1 : oldXDiff;
                    break;
                case 180:
                    newX = oldXDiff * -1;
                    newY = oldYDiff * -1;
                    break;
            }

            return new Point(centerPoint.X + newX, centerPoint.Y + newY);
        }
    }
}