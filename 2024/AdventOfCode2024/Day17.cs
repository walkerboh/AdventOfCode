using System.Text.RegularExpressions;

namespace AdventOfCode2024
{
    internal class Day17
    {
        private long registerA;
        private long registerB;
        private long registerC;
        private readonly List<int> instructions = [];
        private int pointer = 0;
        private readonly List<int> output = [];

        public Day17()
        {
            var lines = File.ReadAllLines("Data\\Day17.txt");
            registerA = int.Parse(Regex.Match(lines[0], @"Register A: (\d+)").Groups[1].Value);
            registerB = int.Parse(Regex.Match(lines[1], @"Register B: (\d+)").Groups[1].Value);
            registerC = int.Parse(Regex.Match(lines[2], @"Register C: (\d+)").Groups[1].Value);
            instructions = lines[4].Replace("Program: ", "").Split(',').Select(int.Parse).ToList();
        }

        public string Problem1()
        {
            while(pointer < instructions.Count)
            {
                var instruction = instructions[pointer];
                var operand = instructions[pointer + 1];

                Operate(instruction, operand);
            }

            return string.Join(',', output);
        }

        public long Problem2()
        {
            return RunOctal("").OrderBy(l => l).First();
        }

        private List<long> RunOctal(string currentOctal)
        {
            var addition = 0;
            var vals = new List<long>();

            while(addition < 8)
            {
                var tempStartValue = $"{currentOctal}{addition++}";

                var startValue = Convert.ToInt64(tempStartValue, 8);

                output.Clear();

                pointer = 0;
                registerA = startValue;
                registerB = 0;
                registerC = 0;

                var instructionsRun = 0;

                while (pointer < instructions.Count && instructionsRun < 100_000)
                {
                    var instruction = instructions[pointer];
                    var operand = instructions[pointer + 1];

                    Operate(instruction, operand);

                    instructionsRun++;
                }

                if(output.Count > instructions.Count)
                {
                    continue;
                }

                if(output.SequenceEqual(instructions))
                {
                    vals.Add(startValue);
                }

                if (output.SequenceEqual(instructions[0..output.Count]) || output.SequenceEqual(instructions[(instructions.Count - output.Count)..]))
                {
                    vals.AddRange(RunOctal(tempStartValue));
                }
            }

            return vals;
        }

        private void Operate(int instruction, int operand)
        {
            switch (instruction)
            {
                case 0:
                    Adv(ComboOperand(operand));
                    break;
                case 1:
                    Bxl(operand);
                    break;
                case 2:
                    Bst(ComboOperand(operand));
                    break;
                case 3:
                    Jnz(operand);
                    break;
                case 4:
                    Bxc();
                    break;
                case 5:
                    OutInstr(ComboOperand(operand));
                    break;
                case 6:
                    Bdv(ComboOperand(operand));
                    break;
                case 7:
                    Cdv(ComboOperand(operand));
                    break;
                default:
                    throw new Exception("bad instruction logic");
            }
        }

        private long ComboOperand(int operand) => operand switch
        {
            0 => 0,
            1 => 1,
            2 => 2,
            3 => 3,
            4 => registerA,
            5 => registerB,
            6 => registerC,
            _ => throw new NotImplementedException("bad combo operand logic")
        };

        // 0
        private void Adv(long value)
        {
            registerA = (long)(registerA / Math.Pow(2, value));
            pointer += 2;
        }

        // 6
        private void Bdv(long value)
        {
            registerB = (long)(registerA / Math.Pow(2, value));
            pointer += 2;
        }

        // 7
        private void Cdv(long value)
        {
            registerC = (long)(registerA / Math.Pow(2, value));
            pointer += 2;
        }

        // 1
        private void Bxl(long value)
        {
            registerB ^= value;
            pointer += 2;
        }

        // 2
        private void Bst(long value)
        {
            registerB = value % 8;
            pointer += 2;
        }

        // 3
        private void Jnz(long value)
        {
            if(registerA == 0)
            {
                pointer += 2;
                return;
            }

            pointer = (int)value;
        }

        // 4
        private void Bxc()
        {
            registerB ^= registerC;
            pointer += 2;
        }

        // 5
        private void OutInstr(long value)
        {
            output.Add((int)(value % 8));
            pointer += 2;
        }
    }
}
