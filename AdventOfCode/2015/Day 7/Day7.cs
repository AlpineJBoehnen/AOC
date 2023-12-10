using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015;

public partial class Day7 : AdventOfCodeDay
{
    public Day7() : base(2015, 7) { }

    protected override string SolvePart1(string[] input)
    {
        List<Wire> wires = [];

        foreach (string line in input)
        {
            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Any(_ => _ == "AND"))
            {
                wires.Add(new Wire(parts[4], new And(parts[0], parts[2])));
            }
            else if (parts.Any(_ => _ == "OR"))
            {
                wires.Add(new Wire(parts[4], new Or(parts[0], parts[2])));
            }
            else if (parts.Any(_ => _ == "NOT"))
            {
                wires.Add(new Wire(parts[3], new Not(parts[1])));
            }
            else if (parts.Any(_ => _ == "LSHIFT"))
            {
                wires.Add(new Wire(parts[4], new LShift(parts[0], int.Parse(parts[2]))));
            }
            else if (parts.Any(_ => _ == "RSHIFT"))
            {
                wires.Add(new Wire(parts[4], new RShift(parts[0], int.Parse(parts[2]))));
            }
            else
            {
                wires.Add(new Wire(parts[2], new Forward(parts[0])));
            }
        }

        Dictionary<string, ushort> wireValues = [];
        foreach (Wire wire in wires)
        {
            wireValues.Add(wire.Name, wire.Input.Execute(wires));
        }

        return wireValues["a"].ToString();
    }

    protected override string SolvePart2(string[] input)
    {
        List<Wire> wires = [];

        foreach (string line in input)
        {
            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Any(_ => _ == "AND"))
            {
                wires.Add(new Wire(parts[4], new And(parts[0], parts[2])));
            }
            else if (parts.Any(_ => _ == "OR"))
            {
                wires.Add(new Wire(parts[4], new Or(parts[0], parts[2])));
            }
            else if (parts.Any(_ => _ == "NOT"))
            {
                wires.Add(new Wire(parts[3], new Not(parts[1])));
            }
            else if (parts.Any(_ => _ == "LSHIFT"))
            {
                wires.Add(new Wire(parts[4], new LShift(parts[0], int.Parse(parts[2]))));
            }
            else if (parts.Any(_ => _ == "RSHIFT"))
            {
                wires.Add(new Wire(parts[4], new RShift(parts[0], int.Parse(parts[2]))));
            }
            else
            {
                wires.Add(new Wire(parts[2], new Forward(parts[0])));
            }
        }

        Dictionary<string, ushort> wireValues = [];
        foreach (Wire wire in wires)
        {
            wireValues.Add(wire.Name, wire.Input.Execute(wires));
        }

        ushort aValue = wireValues["a"];
        wireValues.Clear();
        wires.ForEach(_ => _.Input.Cache = null);
        (wires.First(_ => _.Name == "b").Input as Forward)!.InputName = aValue.ToString();
        foreach (Wire wire in wires)
        {
            wireValues.Add(wire.Name, wire.Input.Execute(wires));
        }

        return wireValues["a"].ToString();
    }

    private class Wire
    {
        public string Name { get; set; }
        public IOperation Input { get; set; }

        public Wire(string name, IOperation input)
        {
            Name = name;
            Input = input;
        }
    }

    private interface IOperation
    {
        public ushort? Cache { get; set; }
        public ushort Execute(ICollection<Wire> wires);
    }

    private abstract class BinaryOperation : IOperation
    {
        public string InputAName { get; set; }
        public string InputBName { get; set; }
        public ushort? Cache { get; set; }

        public BinaryOperation(string inputAName, string inputBName)
        {
            InputAName = inputAName;
            InputBName = inputBName;
        }

        public abstract ushort Execute(ICollection<Wire> wires);
    }

    private abstract class UnaryOperation : IOperation
    {
        public string InputName { get; set; }
        public ushort? Cache { get; set; }

        public UnaryOperation(string inputName)
        {
            InputName = inputName;
        }

        public abstract ushort Execute(ICollection<Wire> wires);
    }

    private class Forward(string inputName) : UnaryOperation(inputName)
    {
        public override ushort Execute(ICollection<Wire> wires)
        {
            if (Cache.HasValue)
            {
                return Cache.Value;
            }

            if (ushort.TryParse(InputName, out ushort value))
            {
                Cache = value;
                return Cache.Value;
            }

            Cache = wires.First(_ => _.Name == InputName).Input.Execute(wires);
            return Cache.Value;
        }
    }

    private class Not(string inputName) : UnaryOperation(inputName)
    {
        public override ushort Execute(ICollection<Wire> wires)
        {
            if (Cache.HasValue)
            {
                return Cache.Value;
            }

            if (ushort.TryParse(InputName, out ushort value))
            {
                Cache = (ushort)~value;
                return Cache.Value;
            }

            Cache = (ushort)~wires.First(_ => _.Name == InputName).Input.Execute(wires);
            return Cache.Value;
        }
    }

    private class And(string inputAName, string inputBName) : BinaryOperation(inputAName, inputBName)
    {
        public override ushort Execute(ICollection<Wire> wires)
        {
            if (Cache.HasValue)
            {
                return Cache.Value;
            }

            if (ushort.TryParse(inputAName, out ushort valueA))
            {
                if (ushort.TryParse(inputBName, out ushort valueB))
                {
                    Cache = (ushort)(valueA & valueB);
                }
                else
                {
                    Cache = (ushort)(valueA & wires.First(_ => _.Name == inputBName).Input.Execute(wires));
                }
                return Cache.Value;
            }
            else if (ushort.TryParse(inputBName, out ushort valueB))
            {
                Cache = (ushort)(wires.First(_ => _.Name == inputAName).Input.Execute(wires) & valueB);
                return Cache.Value;
            }

            Cache = (ushort)(wires.First(_ => _.Name == inputAName).Input.Execute(wires)
                & wires.First(_ => _.Name == inputBName).Input.Execute(wires));
            return Cache.Value;
        }
    }

    private class Or(string inputAName, string inputBName) : BinaryOperation(inputAName, inputBName)
    {
        public override ushort Execute(ICollection<Wire> wires)
        {
            if (Cache.HasValue)
            {
                return Cache.Value;
            }

            if (ushort.TryParse(inputAName, out ushort valueA))
            {
                if (ushort.TryParse(inputBName, out ushort valueB))
                {
                    Cache = (ushort)(valueA | valueB);
                }
                else
                {
                    Cache = (ushort)(valueA | wires.First(_ => _.Name == inputBName).Input.Execute(wires));
                }
                return Cache.Value;
            }
            else if (ushort.TryParse(inputBName, out ushort valueB))
            {
                Cache = (ushort)(wires.First(_ => _.Name == inputAName).Input.Execute(wires) | valueB);
                return Cache.Value;
            }

            Cache = (ushort)(wires.First(_ => _.Name == inputAName).Input.Execute(wires)
                | wires.First(_ => _.Name == inputBName).Input.Execute(wires));
            return Cache.Value;
        }
    }

    private class LShift(string inputName, int shiftAmount) : UnaryOperation(inputName)
    {
        public override ushort Execute(ICollection<Wire> wires)
        {
            if (Cache.HasValue)
            {
                return Cache.Value;
            }

            if (ushort.TryParse(inputName, out ushort value))
            {
                Cache = (ushort)(value << shiftAmount);
                return Cache.Value;
            }

            Cache = (ushort)(wires.First(_ => _.Name == inputName).Input.Execute(wires) << shiftAmount);
            return Cache.Value;
        }
    }

    private class RShift(string inputName, int shiftAmount) : UnaryOperation(inputName)
    {
        public override ushort Execute(ICollection<Wire> wires)
        {
            if (Cache.HasValue)
            {
                return Cache.Value;
            }

            if (ushort.TryParse(inputName, out ushort value))
            {
                Cache = (ushort)(value >> shiftAmount);
                return Cache.Value;
            }

            Cache = (ushort)(wires.First(_ => _.Name == inputName).Input.Execute(wires) >> shiftAmount);
            return Cache.Value;
        }
    }
}
