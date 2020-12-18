using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Aoc2020
{
    //--- Day 17: Conway Cubes ---
    public static class Day17
    {
        public static int Part1(IEnumerable<string> rawInputs)
        {
            var cycles = 6;
            var theCube = InitCube(rawInputs);

            for (var ii = 0; ii < cycles; ii++)
            {
                theCube = CheckCubeStates(theCube);
            }

            return CountActive(theCube, '#');
        }

        // There is a more efficient way, this is just brute forcing :(
        public static long Part2(IEnumerable<string> rawInputs)
        {
            var cycles = 6;
            var theCube = InitCubeP2(rawInputs);

            for (var ii = 0; ii < cycles; ii++)
            {
                theCube = CheckCubeStatesP2(theCube);
            }

            return CountActiveP2(theCube, '#');
        }

        private static char[][][] InitCube(IEnumerable<string> rawInputs)
        {
            var retVal = new char[1][][];
            retVal[0] = rawInputs.Select(input => input.ToCharArray()).ToArray();
            return retVal;
        }

        private static char[][][][] InitCubeP2(IEnumerable<string> rawInputs)
        {
            var retVal = new char[1][][][];
            retVal[0] = new char[1][][];
            retVal[0][0] = rawInputs.Select(input => input.ToCharArray()).ToArray();
            return retVal;
        }

        private static char[][][] GrowCube(char [][][] input)
        {
            // add more rows and cols
            var layer = input.Length;
            var row = input[0].Length;
            var col = input[0][0].Length;

            var retVal = new char[layer + 2][][];

            for (var z = 0; z < layer + 2; z++)
            {
                for (var y = 0; y < row + 2; y++)
                {
                    for (var x = 0; x < col + 2; x++)
                    {
                        if (z == 0 || z == layer + 1 || y == 0 || y == row + 1 || x == 0 || x == col + 1)
                        {
                            retVal[z] ??= new char[row + 2][];
                            retVal[z][y] ??= new char[col + 2];

                            retVal[z][y][x] = '.';
                        }
                        else
                        {
                            retVal[z][y][x] = input[z - 1][y - 1][x - 1];
                        }
                    }
                }
            }

            return retVal;
        }

        private static char[][][][] GrowCubeP2(char[][][][] input)
        {
            // add more rows and cols
            var dim = input.Length;
            var layer = input[0].Length;
            var row = input[0][0].Length;
            var col = input[0][0][0].Length;

            var retVal = new char[dim + 2][][][];

            for (var d = 0; d < dim + 2; d++)
            {
                for (var z = 0; z < layer + 2; z++)
                {
                    for (var y = 0; y < row + 2; y++)
                    {
                        for (var x = 0; x < col + 2; x++)
                        {
                            if (d == 0 || d == dim + 1 || z == 0 || z == layer + 1 || y == 0 || y == row + 1 || x == 0 || x == col + 1)
                            {
                                retVal[d] ??= new char[layer + 2][][];
                                retVal[d][z] ??= new char[row + 2][];
                                retVal[d][z][y] ??= new char[col + 2];

                                retVal[d][z][y][x] = '.';
                            }
                            else
                            {
                                retVal[d][z][y][x] = input[d - 1][z - 1][y - 1][x - 1];
                            }
                        }
                    }
                }
            }


            return retVal;
        }

        private static char[][][] CheckCubeStates(char[][][] input)
        {
            var orig = GrowCube(input);
            var retVal = CopyCube(orig);

            for (var z = 0; z < orig.Length; z++)
            {
                for (var y = 0; y < orig[0].Length; y++)
                {
                    for (var x = 0; x < orig[0][0].Length; x++)
                    {
                        var activeCount = 0;

                        for (var zz = -1; zz < 2; zz++)
                        {
                            for (var yy = -1; yy < 2; yy++)
                            {
                                for (var xx = -1; xx < 2; xx++)
                                {
                                    if (zz == 0 && yy == 0 && xx == 0) continue;

                                    activeCount += IsActive(orig, z + zz, y + yy, x + xx) ? 1 : 0;
                                }
                            }
                        }

                        if (orig[z][y][x] == '#' && activeCount < 2 || activeCount > 3)
                        {
                            retVal[z][y][x] = '.';
                        }
                        else if (orig[z][y][x] == '.' && activeCount == 3)
                        {
                            retVal[z][y][x] = '#';
                        }
                    }
                }
            }

            return retVal;
        }

        private static char[][][][] CheckCubeStatesP2(char[][][][] input)
        {
            var orig = GrowCubeP2(input);
            var retVal = CopyCubeP2(orig);

            for (var d = 0; d < orig.Length; d++)
            {
                for (var z = 0; z < orig[0].Length; z++)
                {
                    for (var y = 0; y < orig[0][0].Length; y++)
                    {
                        for (var x = 0; x < orig[0][0][0].Length; x++)
                        {
                            var activeCount = 0;

                            for (var dd = -1; dd < 2; dd++)
                            {
                                for (var zz = -1; zz < 2; zz++)
                                {
                                    for (var yy = -1; yy < 2; yy++)
                                    {
                                        for (var xx = -1; xx < 2; xx++)
                                        {
                                            if (dd == 0 && zz == 0 && yy == 0 && xx == 0) continue;

                                            activeCount += IsActiveP2(orig, d + dd, z + zz, y + yy, x + xx) ? 1 : 0;
                                        }
                                    }
                                }
                            }

                            if (orig[d][z][y][x] == '#' && activeCount < 2 || activeCount > 3)
                            {
                                retVal[d][z][y][x] = '.';
                            }
                            else if (orig[d][z][y][x] == '.' && activeCount == 3)
                            {
                                retVal[d][z][y][x] = '#';
                            }
                        }
                    }
                }
            }

            return retVal;
        }

        private static int CountActive(char[][][] cube, char key)
        {
            return cube.Sum(layer => layer.Sum(row => row.Count(col => col == key)));
        }

        private static int CountActiveP2(char[][][][] cube, char key)
        {
            return cube.Sum(dim => dim.Sum(layer => layer.Sum(row => row.Count(col => col == key))));
        }

        private static char[][][] CopyCube(char[][][] input)
        {
            var retVal = new char[input.Length][][];
            for (var z = 0; z < retVal.Length; z++)
            {
                retVal[z] ??= new char[input[z].Length][];

                for (var y = 0; y < retVal[0].Length; y++)
                {
                    retVal[z][y] ??= new char[input[z][y].Length];

                    for (var x = 0; x < retVal[0][0].Length; x++)
                    {
                        retVal[z][y][x] = input[z][y][x];
                    }
                }
            }

            return retVal;
        }

        private static char[][][][] CopyCubeP2(char[][][][] input)
        {
            var retVal = new char[input.Length][][][];

            for (var d = 0; d < retVal.Length; d++)
            {
                retVal [d] ??= new char[input[d].Length][][];

                for (var z = 0; z < retVal[0].Length; z++)
                {
                    retVal[d][z] ??= new char[input[d][z].Length][];

                    for (var y = 0; y < retVal[0][0].Length; y++)
                    {
                        retVal[d][z][y] ??= new char[input[d][z][y].Length];

                        for (var x = 0; x < retVal[0][0][0].Length; x++)
                        {
                            retVal[d][z][y][x] = input[d][z][y][x];
                        }
                    }
                }
            }

            return retVal;
        }

        private static bool IsActive(char[][][] input, int layer, int row, int col)
        {
            if (layer < 0 || layer >= input.Length || row < 0 || row >= input[0].Length || col < 0 ||
                col >= input[0][0].Length) return false;
            
            return input[layer][row][col] == '#';
        }

        private static bool IsActiveP2(char[][][][] input, int dimension, int layer, int row, int col)
        {
            if (dimension < 0 || dimension >= input.Length || 
                layer < 0 || layer >= input[0].Length || 
                row < 0 || row >= input[0][0].Length ||
                col < 0 || col >= input[0][0][0].Length) return false;
            
            return input[dimension][layer][row][col] == '#';
        }
    }
}