using System.Runtime.CompilerServices;

namespace AdventOfCode.Y2023;

public class Day10 : AdventOfCodeDay
{
    public Day10() : base(2023, 10)
    {
    }

    /*
    | is a vertical pipe connecting north and south.
    - is a horizontal pipe connecting east and west.
    L is a 90-degree bend connecting north and east.
    J is a 90-degree bend connecting north and west.
    7 is a 90-degree bend connecting south and west.
    F is a 90-degree bend connecting south and east.
    . is ground; there is no pipe in this tile.
    S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has. 
     */
    // valid left, up, right, down pipes
    public Dictionary<char, char[]> Lefts = new()
    {
        ['|'] = [],
        ['-'] = ['S', '-', 'L', 'F'],
        ['L'] = [],
        ['J'] = ['S', '-', 'L', 'F'],
        ['7'] = ['S', '-', 'L', 'F'],
        ['F'] = [],
        ['S'] = ['S', '-', 'L', 'F'],
    };

    public Dictionary<char, char[]> Ups = new()
    {
        ['|'] = ['S', '|', '7', 'F'],
        ['-'] = [],
        ['L'] = ['S', '|', '7', 'F'],
        ['J'] = ['S', '|', '7', 'F'],
        ['7'] = [],
        ['F'] = [],
        ['S'] = ['S', '|', '7', 'F'],
    };

    public Dictionary<char, char[]> Rights = new()
    {
        ['|'] = [],
        ['-'] = ['S', '-', '7', 'J'],
        ['L'] = ['S', '-', '7', 'J'],
        ['J'] = [],
        ['7'] = [],
        ['F'] = ['S', '-', '7', 'J'],
        ['S'] = ['S', '-', '7', 'J'],
    };

    public Dictionary<char, char[]> Downs = new()
    {
        ['|'] = ['S', '|', 'J', 'L'],
        ['-'] = [],
        ['L'] = [],
        ['J'] = [],
        ['7'] = ['S', '|', 'J', 'L'],
        ['F'] = ['S', '|', 'J', 'L'],
        ['S'] = ['S', '|', 'J', 'L'],
    };
    /*
    ..F7.
    .FJ|.
    SJ.L7
    |F--J
    LJ...
     */
    protected override string SolvePart1(string[] input)
    {
        (int x, int y) current = (-1, -1);

        for (int y = 0; y < input.Length; y++)
        {
            if (current.x != -1)
            {
                break;
            }

            for (int x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == 'S')
                {
                    current = (x, y);
                }
            }
        }

        int travelled = 0;
        (int x, int y) previous = current;
        do
        {
            travelled++;

            if (current.x - 1 >= 0 && Lefts[input[current.y][current.x]].Contains(input[current.y][current.x - 1]) && previous != (current.x - 1, current.y))
            {
                previous = current;
                current = (current.x - 1, current.y);
            }
            else if (current.y - 1 >= 0 && Ups[input[current.y][current.x]].Contains(input[current.y - 1][current.x]) && previous != (current.x, current.y - 1))
            {
                previous = current;
                current = (current.x, current.y - 1);
            }
            else if (current.x + 1 < input[0].Length && Rights[input[current.y][current.x]].Contains(input[current.y][current.x + 1]) && previous != (current.x + 1, current.y))
            {
                previous = current;
                current = (current.x + 1, current.y);
            }
            else if (current.y + 1 < input.Length && Downs[input[current.y][current.x]].Contains(input[current.y + 1][current.x]) && previous != (current.x, current.y + 1))
            {
                previous = current;
                current = (current.x, current.y + 1);
            }
        } while (input[current.y][current.x] != 'S');

        return (travelled / 2).ToString();
    }

    protected override string SolvePart2(string[] input)
    {
        bool[][] path = new bool[input.Length][];
        for (int i = 0; i < input.Length; i++)
        {
            path[i] = new bool[input[i].Length];
        }

        (int x, int y) current = (-1, -1);

        for (int y = 0; y < input.Length; y++)
        {
            if (current.x != -1)
            {
                break;
            }

            for (int x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == 'S')
                {
                    current = (x, y);
                }
            }
        }

        int travelled = 0;
        (int x, int y) previous = current;
        do
        {
            path[current.y][current.x] = true;

            travelled++;

            if (current.x - 1 >= 0 && Lefts[input[current.y][current.x]].Contains(input[current.y][current.x - 1]) && previous != (current.x - 1, current.y))
            {
                previous = current;
                current = (current.x - 1, current.y);
            }
            else if (current.y - 1 >= 0 && Ups[input[current.y][current.x]].Contains(input[current.y - 1][current.x]) && previous != (current.x, current.y - 1))
            {
                previous = current;
                current = (current.x, current.y - 1);
            }
            else if (current.x + 1 < input[0].Length && Rights[input[current.y][current.x]].Contains(input[current.y][current.x + 1]) && previous != (current.x + 1, current.y))
            {
                previous = current;
                current = (current.x + 1, current.y);
            }
            else if (current.y + 1 < input.Length && Downs[input[current.y][current.x]].Contains(input[current.y + 1][current.x]) && previous != (current.x, current.y + 1))
            {
                previous = current;
                current = (current.x, current.y + 1);
            }
        } while (input[current.y][current.x] != 'S');

        //expand
        List<string> expanded = [];
        for (int y = 0; y < input.Length; y++)
        {
            string top = "";
            string bot = "";
            for (int x = 0; x < input[y].Length; x++)
            {
                if(!path[y][x])
                {
                    top += "..";
                    bot += "..";
                    continue;
                }

                switch (input[y][x])
                {
                    case '.':
                        top += "..";
                        bot += "..";
                        break;
                    case '|':
                        top += "|.";
                        bot += "|.";
                        break;
                    case '-':
                        top += "--";
                        bot += "..";
                        break;
                    case 'J':
                        top += "J.";
                        bot += "..";
                        break;
                    case 'L':
                        top += "L-";
                        bot += "..";
                        break;
                    case '7':
                        top += "7.";
                        bot += "|.";
                        break;
                    case 'F':
                        top += "F-";
                        bot += "|.";
                        break;
                    case 'S':
                        top += "SS";
                        bot += "SS";
                        break;
                }
            }
            expanded.Add(top);
            expanded.Add(bot);
        }

        //var whole = string.Join("\n", expanded);

        char[][] expan = new char[expanded[0].Length][];
        for (int i = 0; i < expan.Length; i++)
        {
            expan[i] = expanded[i].ToCharArray();
        }

        FloodMark(expan[0].Length - 1, expan.Length - 1, ref expan);
        var whole = string.Join("\n", expan.Select(_ => new string(_)));

        List<string> compressed = [];
        for (int y = 0; y < expan.Length; y += 2)
        {
            string line = "";
            for (int x = 0; x < expan[y].Length; x += 2)
            {
                line += expan[y][x];
            }

            compressed.Add(line);
        }

        whole = string.Join("\n", compressed);
        var pretty = MakePretty(whole);
        return whole.Count(_ => _ == '.').ToString();
    }

    string MakePretty(string map)
    {
        map = map.Replace('x', ' ');
        map = map.Replace('.', '■');
        //map = map.Replace('F', '┌');
        //map = map.Replace('L', '└');
        //map = map.Replace('7', '┐');
        //map = map.Replace('J', '┘');
        //map = map.Replace('|', '│');
        //map = map.Replace('-', '─');
        map = map.Replace('F', '╔');
        map = map.Replace('L', '╚');
        map = map.Replace('7', '╗');
        map = map.Replace('J', '╝');
        map = map.Replace('|', '║');
        map = map.Replace('-', '═');

        return map;
    }

    void FloodMark(int x, int y, ref char[][] map)
    {
        if(x < 0 || y < 0 || x >= map[0].Length || y >= map.Length)
        {
            return;
        }

        if (map[y][x] != '.')
        {
            return;
        }

        map[y][x] = 'x';

        FloodMark(x - 1, y, ref map);
        FloodMark(x + 1, y, ref map);
        FloodMark(x, y - 1, ref map);
        FloodMark(x, y + 1, ref map);

        return;
    }
}