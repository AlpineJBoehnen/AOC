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
            else if(parts.Any(_ => _ == "OR"))
            {
                wires.Add(new Wire(parts[4], new Or(parts[0], parts[2])));
            }
            else if(parts.Any(_ => _ == "OR"))
            {
                wires.Add(new Wire(parts[4], new Or(parts[0], parts[2])));
            }
            else if(parts.Any(_ => _ == "OR"))
            {
                wires.Add(new Wire(parts[4], new Or(parts[0], parts[2])));
            }
            else if(parts.Any(_ => _ == "OR"))
            {
                wires.Add(new Wire(parts[4], new Or(parts[0], parts[2])));
            }
        }

        return "";
    }

    protected override string SolvePart2(string[] input)
    {
        return "";
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
        public ushort Execute(ICollection<Wire> wires);
    }

    private abstract class BinaryOperation : IOperation
    {
        public string InputAName { get; set; }
        public string InputBName { get; set; }

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
            return wires.First(_ => _.Name == InputName).Input.Execute(wires);
        }
    }

    private class Not(string inputName) : UnaryOperation(inputName)
    {
        public override ushort Execute(ICollection<Wire> wires)
        {
            return (ushort)~wires.First(_ => _.Name == InputName).Input.Execute(wires);
        }
    }

    private class And(string inputAName, string inputBName) : BinaryOperation(inputAName, inputBName)
    {
        public override ushort Execute(ICollection<Wire> wires)
        {
            return (ushort)(wires.First(_ => _.Name == inputAName).Input.Execute(wires) 
                & wires.First(_ => _.Name == inputBName).Input.Execute(wires));
        }
    }

    private class Or(string inputAName, string inputBName) : BinaryOperation(inputAName, inputBName)
    {
        public override ushort Execute(ICollection<Wire> wires)
        {
            return (ushort)(wires.First(_ => _.Name == inputAName).Input.Execute(wires)
                | wires.First(_ => _.Name == inputBName).Input.Execute(wires));
        }
    }

    private class LShift(string inputName, int shiftAmount) : UnaryOperation(inputName)
    {
        public override ushort Execute(ICollection<Wire> wires)
        {
            return (ushort)(wires.First(_ => _.Name == inputName).Input.Execute(wires) << shiftAmount);
        }
    }

    private class RShift(string inputName, int shiftAmount) : UnaryOperation(inputName)
    {
        public override ushort Execute(ICollection<Wire> wires)
        {
            return (ushort)(wires.First(_ => _.Name == inputName).Input.Execute(wires) >> shiftAmount);
        }
    }
}
