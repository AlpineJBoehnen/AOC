using System.Text.RegularExpressions;

namespace AdventOfCode.Y2023;

public partial class Day5 : AdventOfCodeDay
{
    public Day5() : base(2023, 5) { }

    protected override string SolvePart1(string[] input)
    {
        ulong[] seeds = NumberRegex().Matches(input[0]).Select(_ => ulong.Parse(_.Value)).ToArray();
        List<Map> seedToSoil = [];
        List<Map> soilToFert = [];
        List<Map> fertToWater = [];
        List<Map> waterToLight = [];
        List<Map> lightToTemp = [];
        List<Map> tempToHumid = [];
        List<Map> humidToLocat = [];

        int section = -1;
        for (int ii = 2; ii < input.Length; ii++)
        {
            if (input[ii] == string.Empty)
            {
                continue;
            }
            else if (input[ii].Contains(":"))
            {
                section++;
                continue;
            }

            string[] rowNumbers = NumberRegex().Matches(input[ii]).Select(_ => _.Value).ToArray();
            ulong to = ulong.Parse(rowNumbers[0]);
            ulong from = ulong.Parse(rowNumbers[1]);
            ulong range = ulong.Parse(rowNumbers[2]);

            switch (section)
            {
                case 0:
                    seedToSoil.Add(new(from, to, range));
                    continue;
                case 1:
                    soilToFert.Add(new(from, to, range));
                    continue;
                case 2:
                    fertToWater.Add(new(from, to, range));
                    continue;
                case 3:
                    waterToLight.Add(new(from, to, range));
                    continue;
                case 4:
                    lightToTemp.Add(new(from, to, range));
                    continue;
                case 5:
                    tempToHumid.Add(new(from, to, range));
                    continue;
                case 6:
                    humidToLocat.Add(new(from, to, range));
                    continue;
                default:
                    throw new NotImplementedException("To many sections");
            }
        }

        ulong lowestLocation = ulong.MaxValue;
        foreach (ulong seed in seeds)
        {
            ulong soil = GetMap(seed, seedToSoil).Convert(seed);
            ulong fert = GetMap(soil, soilToFert).Convert(soil);
            ulong water = GetMap(fert, fertToWater).Convert(fert);
            ulong light = GetMap(water, waterToLight).Convert(water);
            ulong temp = GetMap(light, lightToTemp).Convert(light);
            ulong humid = GetMap(temp, tempToHumid).Convert(temp);
            ulong locat = GetMap(humid, humidToLocat).Convert(humid);

            if (locat < lowestLocation)
            {
                lowestLocation = locat;
            }
        }

        return lowestLocation.ToString();
    }

    protected override string SolvePart2(string[] input)
    {
        List<Range> seedsRanges = [];
        ulong[] seedsRaw = NumberRegex().Matches(input[0]).Select(_ => ulong.Parse(_.Value)).ToArray();

        for (ulong ii = 0; ii < (ulong)seedsRaw.Length - 1; ii += 2)
        {
            seedsRanges.Add(new(seedsRaw[ii], seedsRaw[ii] + seedsRaw[ii+1] - 1));
        }

        List<Map> seedToSoil = [];
        List<Map> soilToFert = [];
        List<Map> fertToWater = [];
        List<Map> waterToLight = [];
        List<Map> lightToTemp = [];
        List<Map> tempToHumid = [];
        List<Map> humidToLocat = [];

        int section = -1;
        for (int ii = 2; ii < input.Length; ii++)
        {
            if (input[ii] == string.Empty)
            {
                continue;
            }
            else if (input[ii].Contains(":"))
            {
                section++;
                continue;
            }

            string[] rowNumbers = NumberRegex().Matches(input[ii]).Select(_ => _.Value).ToArray();
            ulong to = ulong.Parse(rowNumbers[0]);
            ulong from = ulong.Parse(rowNumbers[1]);
            ulong range = ulong.Parse(rowNumbers[2]);

            switch (section)
            {
                case 0:
                    seedToSoil.Add(new(from, to, range));
                    continue;
                case 1:
                    soilToFert.Add(new(from, to, range));
                    continue;
                case 2:
                    fertToWater.Add(new(from, to, range));
                    continue;
                case 3:
                    waterToLight.Add(new(from, to, range));
                    continue;
                case 4:
                    lightToTemp.Add(new(from, to, range));
                    continue;
                case 5:
                    tempToHumid.Add(new(from, to, range));
                    continue;
                case 6:
                    humidToLocat.Add(new(from, to, range));
                    continue;
                default:
                    throw new NotImplementedException("To many sections");
            }
        }

        var soil = ConvertRanges(seedsRanges, seedToSoil);
        var fert = ConvertRanges(soil, soilToFert);
        var water = ConvertRanges(fert, fertToWater);
        var light = ConvertRanges(water, waterToLight);
        var temp = ConvertRanges(light, lightToTemp);
        var humid = ConvertRanges(temp, tempToHumid);
        var locat = ConvertRanges(humid, humidToLocat);

        return locat.OrderBy(_ => _.Start).First().Start.ToString();
    }

    [GeneratedRegex(@"\d+")]
    private static partial Regex NumberRegex();

    private Map GetMap(ulong input, List<Map> maps)
    {
        Map? map = maps.SingleOrDefault(_ => _.IsInMap(input))
            ?? new Map(input, input, 1);

        return map;
    }

    private class Map
    {
        public ulong FromLow { get; set; }
        public ulong ToLow { get; set; }
        public ulong Range { get; set; }

        public Map(ulong fromLow, ulong toLow, ulong range)
        {
            FromLow = fromLow;
            ToLow = toLow;
            Range = range;
        }

        public bool IsInMap(ulong input)
        {
            return input >= FromLow && input <= FromLow + Range - 1;
        }

        public ulong Convert(ulong input)
        {
            if (!IsInMap(input))
            {
                throw new InvalidOperationException("Wrong Map");
            }

            ulong offset = input - FromLow;
            return ToLow + offset;
        }
    }

    private class Range
    {
        public ulong Start { get; set; }
        public ulong End { get; set; }

        public Range(ulong start, ulong end)
        {
            Start = start;
            End = end;
        }

        public Range Intersect(Range input)
        {
            ulong low;
            if (input.Start < Start)
            {
                low = Start;
            }
            else
            {
                low = input.Start;
            }

            ulong high;
            if (input.End > End)
            {
                high = End;
            }
            else
            {
                high = input.End;
            }

            return new Range(low, high);
        }

        public bool IsEmpty()
        {
            return Start == End;
        }
    }

    private List<Range> ConvertRanges(List<Range> inputRanges, List<Map> maps)
    {
        List<Range> outputRanges = [];

        foreach (var range in inputRanges.OrderBy(_ => _.Start))
        {
            bool rangeProcessed = false;

            foreach (var map in maps.OrderBy(_ => _.FromLow))
            {
                var mapRange = new Range(map.FromLow, map.FromLow + map.Range - 1);

                if (range.End >= mapRange.Start && range.Start <= mapRange.End)
                {
                    var inMap = range.Intersect(mapRange);

                    if (!inMap.IsEmpty())
                    {
                        outputRanges.Add(new Range(map.Convert(inMap.Start), map.Convert(inMap.End)));
                        rangeProcessed = true;
                    }
                }
            }

            if (!rangeProcessed)
            {
                outputRanges.Add(range);
            }
        }

        return outputRanges;
    }
}