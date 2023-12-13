using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;

namespace AdventOfCode.Y2023;

public class Day13 : AdventOfCodeDay
{
    public Day13() : base(2023, 13)
    {
    }

    protected override string SolvePart1(string[] input)
    {
        List<List<string>> puzzles = [];
        foreach (string line in input)
        {
            if (puzzles.Count == 0 || line == "")
            {
                puzzles.Add(new List<string>());
            }

            if (line != "")
            {
                puzzles.Last().Add(line);
            }
        }

        ulong total = 0;
        foreach (List<string> puzzle in puzzles)
        {
            List<uint> rowHashes = [];
            List<uint> colHashes = [];
            for (int yy = 0; yy < puzzle.Count; yy++)
            {
                rowHashes.Add(Hash(puzzle[yy]));
            }

            for (int xx = 0; xx < puzzle[0].Length; xx++)
            {
                string col = "";
                for (int yy = 0; yy < puzzle.Count; yy++)
                {
                    col += puzzle[yy][xx];
                }

                colHashes.Add(Hash(col));
            }

            total += (ulong)GetSymmetryValue(rowHashes, colHashes);
        }

        return total.ToString();
    }

    protected override string SolvePart2(string[] input)
    {
        List<List<string>> puzzles = [];
        foreach (string line in input)
        {
            if (puzzles.Count == 0 || line == "")
            {
                puzzles.Add(new List<string>());
            }

            if (line != "")
            {
                puzzles.Last().Add(line);
            }
        }

        ulong total = 0;
        foreach (List<string> puzzle in puzzles)
        {
            List<uint> rowHashes = [];
            List<uint> colHashes = [];
            for (int yy = 0; yy < puzzle.Count; yy++)
            {
                rowHashes.Add(Hash(puzzle[yy]));
            }

            for (int xx = 0; xx < puzzle[0].Length; xx++)
            {
                string col = "";
                for (int yy = 0; yy < puzzle.Count; yy++)
                {
                    col += puzzle[yy][xx];
                }

                colHashes.Add(Hash(col));
            }

            total += (ulong)GetSymmetryValue2(rowHashes, colHashes);
        }

        return total.ToString();
    }

    // Row - 100 200 300 V 300 200 100 400 returns 300
    // Col - 100 200 300 V 300 200 100 400 returns 3
    private static int GetSymmetryValue(List<uint> rowHashes, List<uint> colHashes)
    {
        for (int xx = 0; xx < rowHashes.Count - 1; xx++)
        {
            bool equal = true;
            int it = 0;
            while (xx - it >= 0 && xx + 1 + it < rowHashes.Count)
            {
                if (rowHashes[xx - it] != rowHashes[xx + 1 + it])
                {
                    equal = false;
                    break;
                }
                else
                {

                }

                it++;
            }

            if (equal)
            {
                return (xx + 1) * 100;
            }
        }

        for (int xx = 0; xx < colHashes.Count - 1; xx++)
        {
            bool equal = true;
            int it = 0;
            while (xx - it >= 0 && xx + 1 + it < colHashes.Count)
            {
                if (colHashes[xx - it] != colHashes[xx + 1 + it])
                {
                    equal = false;
                    break;
                }

                it++;
            }

            if (equal)
            {
                return xx + 1;
            }
        }

        throw new Exception("No symmetry found");
    }

    private static int GetSymmetryValue2(List<uint> rowHashes, List<uint> colHashes)
    {
        for (int xx = 0; xx < rowHashes.Count - 1; xx++)
        {
            bool equal = true;
            bool cleanedSmudge = false;
            int it = 0;
            while (xx - it >= 0 && xx + 1 + it < rowHashes.Count)
            {
                if (rowHashes[xx - it] != rowHashes[xx + 1 + it])
                {
                    if (IsOneBitOff(rowHashes[xx - it], rowHashes[xx + 1 + it]) && !cleanedSmudge)
                    {
                        cleanedSmudge = true;
                    }
                    else
                    {
                        equal = false;
                        break;
                    }
                }

                it++;
            }

            if (equal && cleanedSmudge)
            {
                return (xx + 1) * 100;
            }
        }

        for (int xx = 0; xx < colHashes.Count - 1; xx++)
        {
            bool equal = true;
            bool cleanedSmudge = false;
            int it = 0;
            while (xx - it >= 0 && xx + 1 + it < colHashes.Count)
            {
                if (colHashes[xx - it] != colHashes[xx + 1 + it])
                {
                    if (IsOneBitOff(colHashes[xx - it], colHashes[xx + 1 + it]) && !cleanedSmudge)
                    {
                        cleanedSmudge = true;
                    }
                    else
                    {
                        equal = false;
                        break;
                    }
                }

                it++;
            }

            if (equal && cleanedSmudge)
            {
                return xx + 1;
            }
        }

        throw new Exception("No symmetry found");
    }

    private static bool IsOneBitOff(uint a, uint b)
    {
        int count = 0;
        uint xor = a ^ b;

        while (xor != 0)
        {
            xor &= (xor - 1);
            count++;
        }

        return count == 1;
    }

    private static uint Hash(string text)
    {
        uint hash = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '#')
            {
                hash |= (uint)(1 << i);
            }
        }

        return hash;
    }
}