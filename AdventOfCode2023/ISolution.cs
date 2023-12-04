using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal interface ISolution
    {
        public abstract static string Part1(string[] input);

        public abstract static string Part2(string[] input);
    }
}
