using System.Text.RegularExpressions;

namespace AdventOfCode2024
{
    internal partial class Day24
    {
        private readonly Dictionary<string, bool> knownSignals = [];
        private readonly List<(string Cable1, Operation Op, string Cable2, string OutputCable)> gates = [];

        public Day24()
        {
            var lines = File.ReadAllLines("Data\\Day24.txt");

            var lineNum = 0;

            while (!string.IsNullOrWhiteSpace(lines[lineNum]))
            {
                var match = InputCableRegex().Match(lines[lineNum]);
                knownSignals.Add(match.Groups[1].Value, match.Groups[2].Value == "1");
                lineNum++;
            }

            lineNum++;

            while(lineNum < lines.Length)
            {
                var match = GateRegex().Match(lines[lineNum]);
                gates.Add((match.Groups[1].Value, ParseOp(match.Groups[2].Value), match.Groups[3].Value, match.Groups[4].Value));
                lineNum++;
            }
        }

        public long Problem1()
        {
            while(gates.Count > 0)
            {
                var gate = gates.First(g => knownSignals.ContainsKey(g.Cable1) && knownSignals.ContainsKey(g.Cable2));
                knownSignals[gate.OutputCable] = RunOp(knownSignals[gate.Cable1], knownSignals[gate.Cable2], gate.Op);
                gates.Remove(gate);
            }

            var zSignals = knownSignals.Where(s => s.Key.StartsWith('z')).OrderByDescending(s => s.Key).ToList();
            var binary = string.Join("", zSignals.Select(s => s.Value ? "1" : "0"));

            return Convert.ToInt64(binary, 2);
        }

        private static bool RunOp(bool cable1, bool cable2, Operation op) => op switch
        {
            Operation.AND => cable1 && cable2,
            Operation.OR => cable1 || cable2,
            Operation.XOR => cable1 ^ cable2,
            _ => throw new Exception("Bad RunOp")
        };

        enum Operation
        {
            AND,
            OR,
            XOR
        }

        private Operation ParseOp(string op) => op switch
        {
            "AND" => Operation.AND,
            "OR" => Operation.OR,
            "XOR" => Operation.XOR,
            _ => throw new Exception("Bad ParseOp")
        };

        [GeneratedRegex(@"([a-z0-9]+): (0|1)")]
        private static partial Regex InputCableRegex();

        [GeneratedRegex(@"([a-z0-9]+) (AND|OR|XOR) ([a-z0-9]+) -> ([a-z0-9]+)")]
        private static partial Regex GateRegex();
    }
}
