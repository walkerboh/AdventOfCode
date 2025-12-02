using System.Text.RegularExpressions;

namespace AdventOfCode2015
{
    internal partial class Day07 : CustomDay
    {
        [GeneratedRegex(@"^(\d+) -> (\w+)$")]
        private static partial Regex SignalRegex();

        [GeneratedRegex(@"^(\w+) -> (\w+)$")]
        private static partial Regex WireRegex();

        [GeneratedRegex(@"^NOT (\w+) -> (\w+)$")]
        private static partial Regex NotRegex();

        [GeneratedRegex(@"^(\w+) (\w+) (\S+) -> (\w+)$")]
        private static partial Regex OperatorRegex();

        public override ValueTask<string> Solve_1()
        {
            var wires = new Dictionary<string, ushort>();

            var instructions = GetInputLines().ToList();

            var inputs = instructions.Where(i => SignalRegex().IsMatch(i)).ToList();

            foreach(var input in inputs)
            {
                var match = SignalRegex().Match(input);
                wires.Add(match.Groups[2].Value, ushort.Parse(match.Groups[1].Value));
                instructions.Remove(input);
            }

            var gates = instructions.Select(CreateGate).ToList();

            while(gates.Any())
            {
                var knownGate = gates.First(g => g.GetInputCables().All(c => wires.ContainsKey(c)));
                knownGate.GetInputValues(wires);
                var output = knownGate.GetOutputValue();
                wires.Add(knownGate.OutputCable, output);
                gates.Remove(knownGate);
            }

            FinalPart1Answer = wires["a"];

            return ValueTask.FromResult(FinalPart1Answer.ToString());
        }

        private ushort FinalPart1Answer;

        public override ValueTask<string> Solve_2()
        {
            var wires = new Dictionary<string, ushort>();

            var instructions = GetInputLines().ToList();

            var inputs = instructions.Where(i => SignalRegex().IsMatch(i)).ToList();

            foreach (var input in inputs)
            {
                var match = SignalRegex().Match(input);
                wires.Add(match.Groups[2].Value, ushort.Parse(match.Groups[1].Value));
                instructions.Remove(input);
            }

            wires["b"] = FinalPart1Answer;

            var gates = instructions.Select(CreateGate).ToList();

            while (gates.Any())
            {
                var knownGate = gates.First(g => g.GetInputCables().All(c => wires.ContainsKey(c)));
                knownGate.GetInputValues(wires);
                var output = knownGate.GetOutputValue();
                wires.Add(knownGate.OutputCable, output);
                gates.Remove(knownGate);
            }

            return ValueTask.FromResult(wires["a"].ToString());
        }

        // To Me in the future: I know I over-engineered this. It wasn't the easiest amount of work, but it was the easiest mental paradigm for solving it for me that day
        // Except the "Constant" gates, probably could have done something smarter there

        private static Gate CreateGate(string input)
        {
            Match match = NotRegex().Match(input);

            if(match.Success)
            {
                return new NotGate(match.Groups[2].Value, match.Groups[1].Value);
            }

            match = WireRegex().Match(input);

            if(match.Success)
            {
                return new Wire(match.Groups[2].Value, match.Groups[1].Value);
            }

            match = OperatorRegex().Match(input);

            if(match.Success)
            {
                var op = match.Groups[2].Value;

                return op switch
                {
                    "AND" => CheckAndGate(match.Groups[4].Value, match.Groups[1].Value, match.Groups[3].Value),
                    "OR" => CheckOrGate(match.Groups[4].Value, match.Groups[1].Value, match.Groups[3].Value),
                    "LSHIFT" => new LShiftGate(match.Groups[4].Value, match.Groups[1].Value, ushort.Parse(match.Groups[3].Value)),
                    "RSHIFT" => new RShiftGate(match.Groups[4].Value, match.Groups[1].Value, ushort.Parse(match.Groups[3].Value)),
                    _ => throw new Exception("Invalid OP found")
                };
            }

            throw new Exception($"Input {input} does not match any regex.");
        }

        private static Gate CheckAndGate(string output, string input1, string input2)
        {
            if(ushort.TryParse(input1, out var val))
            {
                return new ConstantAndGate(output, input2, val);
            }
            else if(ushort.TryParse(input2, out val))
            {
                return new ConstantAndGate(output, input1, val);
            }
            else
            {
                return new AndGate(output, input1, input2);
            }
        }

        private static Gate CheckOrGate(string output, string input1, string input2)
        {
            if (ushort.TryParse(input1, out var val))
            {
                return new ConstantOrGate(output, input2, val);
            }
            else if (ushort.TryParse(input2, out val))
            {
                return new ConstantOrGate(output, input1, val);
            }
            else
            {
                return new OrGate(output, input1, input2);
            }
        }

        private abstract class Gate(string output, string inputCable1)
        {
            public string OutputCable { get; set; } = output;
            public string InputCable1 { get; set; } = inputCable1;
            public ushort? InputValue1 { get; set; }
            public virtual IEnumerable<string> GetInputCables() => [InputCable1];
            public virtual void GetInputValues(Dictionary<string, ushort> wires) => InputValue1 = wires[InputCable1];
            public abstract ushort GetOutputValue();
        }

        private class Wire(string output, string inputCable1) : Gate(output, inputCable1)
        {
            public override ushort GetOutputValue() => InputValue1 ?? throw new Exception("missing value");
        }

        private class AndGate(string output, string inputCable1, string inputCable2) : Gate(output, inputCable1)
        {
            public string InputCable2 { get; set; } = inputCable2;
            public ushort? InputValue2 { get; set; }
            public override IEnumerable<string> GetInputCables() => [InputCable1, InputCable2];
            public override void GetInputValues(Dictionary<string, ushort> wires)
            {
                InputValue1 = wires[InputCable1];
                InputValue2 = wires[InputCable2];
            }
            public override ushort GetOutputValue() => (ushort?)(InputValue1 & InputValue2) ?? throw new Exception("values missing");
        }

        private class ConstantAndGate(string output, string inputCable1, ushort inputValue2) : Gate(output, inputCable1)
        {
            public ushort InputValue2 { get; set; } = inputValue2;
            public override ushort GetOutputValue() => (ushort?)(InputValue1 & InputValue2) ?? throw new Exception("values missing");
        }

        private class OrGate(string output, string inputCable1, string inputCable2) : Gate(output, inputCable1)
        {
            public string InputCable2 { get; set; } = inputCable2;
            public ushort? InputValue2 { get; set; }
            public override IEnumerable<string> GetInputCables() => [InputCable1, InputCable2];
            public override void GetInputValues(Dictionary<string, ushort> wires)
            {
                InputValue1 = wires[InputCable1];
                InputValue2 = wires[InputCable2];
            }
            public override ushort GetOutputValue() => (ushort?)(InputValue1 | InputValue2) ?? throw new Exception("values missing");
        }

        private class ConstantOrGate(string output, string inputCable1, ushort inputValue2) : Gate(output, inputCable1)
        {
            public ushort InputValue2 { get; set; } = inputValue2;
            public override ushort GetOutputValue() => (ushort?)(InputValue1 | InputValue2) ?? throw new Exception("values missing");
        }

        private abstract class ShiftGate(string output, string inputCable1, ushort shiftValue) : Gate(output, inputCable1)
        {
            public ushort ShiftValue { get; set; } = shiftValue;
        }

        private class LShiftGate(string output, string inputCable1, ushort shiftValue) : ShiftGate(output, inputCable1, shiftValue)
        {
            public override ushort GetOutputValue() => (ushort?)(InputValue1 << ShiftValue) ?? throw new Exception("values missing");
        }

        private class RShiftGate(string output, string inputCable1, ushort shiftValue) : ShiftGate(output, inputCable1, shiftValue)
        {
            public override ushort GetOutputValue() => (ushort?)(InputValue1 >> ShiftValue) ?? throw new Exception("values missing");
        }

        private class NotGate(string output, string inputCable1) : Gate(output, inputCable1)
        {
            public override ushort GetOutputValue() => (ushort?)~InputValue1 ?? throw new Exception("values missing");
        }
    }
}
