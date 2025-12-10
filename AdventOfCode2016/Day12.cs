using System.Net.Mime;
using System.Text.RegularExpressions;

namespace AdventOfCode2016
{
    internal partial class Day12 : CustomDay
    {
        private readonly List<string> Instructions;

        [GeneratedRegex(@"^(\w{3}) (\S+)(?: (\S+)$)?")]
        private static partial Regex InstructionRegex();

        public Day12()
        {
            Instructions = GetInputLines();
        }

        private class State
        {
            public int A { get; set; }
            public int B { get; set; }
            public int C { get; set; }
            public int D { get; set; }

            public void AssignRegister(string register, int value)
            {
                switch (register)
                {
                    case "a":
                        A = value;
                        break;
                    case "b":
                        B = value;
                        break;
                    case "c":
                        C = value;
                        break;
                    case "d":
                        D = value;
                        break;
                }
            }

            public int GetRegister(string register) =>
                register switch
                {
                    "a" => A,
                    "b" => B,
                    "c" => C,
                    "d" => D
                };
        }

        public override ValueTask<string> Solve_1()
        {
            var state = new State();
            var pos = 0;

            while(pos < Instructions.Count)
            {
                var instruction = Instructions[pos];

                var match = InstructionRegex().Match(instruction);

                switch (match.Groups[1].Value)
                {
                    case "cpy":
                        int toAssign;

                        if (int.TryParse(match.Groups[2].Value, out var amt))
                        {
                            toAssign = amt;
                        }
                        else
                        {
                            toAssign = state.GetRegister(match.Groups[2].Value);
                        }

                        state.AssignRegister(match.Groups[3].Value, toAssign);
                        pos++;
                        break;
                    case "inc":
                        state.AssignRegister(match.Groups[2].Value, state.GetRegister(match.Groups[2].Value) + 1);
                        pos++;
                        break;
                    case "dec":
                        state.AssignRegister(match.Groups[2].Value, state.GetRegister(match.Groups[2].Value) - 1);
                        pos++;
                        break;
                    case "jnz":
                        if (int.TryParse(match.Groups[2].Value, out var jmp) && jmp != 0)
                        {
                            var distance = int.Parse(match.Groups[3].Value);
                            pos += distance;
                        }
                        else if (state.GetRegister(match.Groups[2].Value) != 0)
                        {
                            var distance = int.Parse(match.Groups[3].Value);
                            pos += distance;
                        }
                        else
                        {
                            pos++;
                        }
                        break;
                }
            }

            return ValueTask.FromResult(state.A.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var state = new State();
            state.C = 1;
            var pos = 0;

            while (pos < Instructions.Count)
            {
                var instruction = Instructions[pos];

                var match = InstructionRegex().Match(instruction);

                switch (match.Groups[1].Value)
                {
                    case "cpy":
                        int toAssign;

                        if (int.TryParse(match.Groups[2].Value, out var amt))
                        {
                            toAssign = amt;
                        }
                        else
                        {
                            toAssign = state.GetRegister(match.Groups[2].Value);
                        }

                        state.AssignRegister(match.Groups[3].Value, toAssign);
                        pos++;
                        break;
                    case "inc":
                        state.AssignRegister(match.Groups[2].Value, state.GetRegister(match.Groups[2].Value) + 1);
                        pos++;
                        break;
                    case "dec":
                        state.AssignRegister(match.Groups[2].Value, state.GetRegister(match.Groups[2].Value) - 1);
                        pos++;
                        break;
                    case "jnz":
                        if (int.TryParse(match.Groups[2].Value, out var jmp) && jmp != 0)
                        {
                            var distance = int.Parse(match.Groups[3].Value);
                            pos += distance;
                        }
                        else if (state.GetRegister(match.Groups[2].Value) != 0)
                        {
                            var distance = int.Parse(match.Groups[3].Value);
                            pos += distance;
                        }
                        else
                        {
                            pos++;
                        }
                        break;
                }
            }

            return ValueTask.FromResult(state.A.ToString());
        }
    }
}
