using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day3 : ISolution
    {
        public static string Part1(string[] input)
        {
            static bool isSymbol(char c)
            {
                return (c != '.' && (c < '0' || c > '9'));
            }

            int total = 0;
            int height = input.Length;
            int width = input[0].Length;

            for (int y = 0; y < height; y++)
            {
                bool sawSymbol = false;
                string bufferNumber = "";

                for (int x = 0; x < width; x++)
                {
                    bool symbolInBuffer = false;

                    //detect buffer symbol

                    if (isSymbol(input[y][x]))
                    {
                        symbolInBuffer = true;
                    }
                    if (y > 0 && isSymbol(input[y - 1][x]))
                    {
                        symbolInBuffer = true;
                    }
                    if ((y < height - 1) && isSymbol(input[y + 1][x]))
                    {
                        symbolInBuffer = true;
                    }

                    if (symbolInBuffer)
                    {
                        sawSymbol = true;
                    }

                    if (input[y][x] >= '0' && input[y][x] <= '9')
                    {
                        bufferNumber += input[y][x];
                    }
                    else if (sawSymbol && bufferNumber != "")
                    {
                        total += int.Parse(bufferNumber); //123&123
                        bufferNumber = "";
                        if (!symbolInBuffer)
                        {
                            sawSymbol = false;
                        }
                    }
                    else
                    {
                        bufferNumber = "";
                        if (!symbolInBuffer)
                        {
                            sawSymbol = false;
                        }
                    }

                    if (x == width - 1)
                    {
                        if (sawSymbol && bufferNumber != "")
                        {
                            total += int.Parse(bufferNumber); //123&123
                            bufferNumber = "";
                            if (!symbolInBuffer)
                            {
                                sawSymbol = false;
                            }
                        }
                    }
                }
            }

            return total.ToString();
        }

        public static string Part2(string[] input)
        {
            int total = 0;
            int height = input.Length;
            int width = input[0].Length;

            for (int y = 0; y < height; y++)
            {
                List<string>
                bool sawSymbol = false;
                string bufferNumber = "";

                for (int x = 0; x < width; x++)
                {
                    bool symbolInBuffer = false;

                    //detect buffer symbol

                    if (isSymbol(input[y][x]))
                    {
                        symbolInBuffer = true;
                    }
                    if (y > 0 && isSymbol(input[y - 1][x]))
                    {
                        symbolInBuffer = true;
                    }
                    if ((y < height - 1) && isSymbol(input[y + 1][x]))
                    {
                        symbolInBuffer = true;
                    }

                    if (symbolInBuffer)
                    {
                        sawSymbol = true;
                    }

                    if (input[y][x] >= '0' && input[y][x] <= '9')
                    {
                        bufferNumber += input[y][x];
                    }
                    else if (sawSymbol && bufferNumber != "")
                    {
                        total += int.Parse(bufferNumber); //123&123
                        bufferNumber = "";
                        if (!symbolInBuffer)
                        {
                            sawSymbol = false;
                        }
                    }
                    else
                    {
                        bufferNumber = "";
                        if (!symbolInBuffer)
                        {
                            sawSymbol = false;
                        }
                    }

                    if (x == width - 1)
                    {
                        if (sawSymbol && bufferNumber != "")
                        {
                            total += int.Parse(bufferNumber); //123&123
                            bufferNumber = "";
                            if (!symbolInBuffer)
                            {
                                sawSymbol = false;
                            }
                        }
                    }
                }
            }

            return total.ToString();
        }

        private int[] GetAdjacentNumbers(string[] input, int x, int y)
        {
            List<int> numbers = new();
            // ... or ..1 or .1. or .11 or 1.. or 1.1 or 11. or 111
            // top
            if(y > 0)
            {
                if (x > 0)
                {
                    if (isNumber(input[y][x - 1]))
                    {
                        numbers.Add(GetFullNumber(input[y], x - 1));
                    }
                }
                else
                {
                    if (x < input[0].Length - 1)
                    {

                    }
                    else if(isNumber())
                }
            }

            // left
            if(x > 0)
            {
                if (isNumber(input[y][x - 1]))
                {
                    numbers.Add(GetFullNumber(input[y], x - 1));
                }
            }

            // right
            if (x < input[0].Length - 1)
            {
                if (isNumber(input[y][x + 1]))
                {
                    numbers.Add(GetFullNumber(input[y], x + 1));
                }
            }
        }

        private int GetFullNumber(string line, int x)
        {
            int startInd = x;
            int endInd = x;
            while (isNumber(line[startInd - 1]))
            {
                startInd--;
            }

            while (isNumber(line[endInd + 1]))
            {
                endInd++;
            }

            return int.Parse(line.Substring(startInd, endInd - startInd));
        }

        private bool isNumber(char c)
        {
            return (c >= '0' && c <= '9');
        }
    }
}
