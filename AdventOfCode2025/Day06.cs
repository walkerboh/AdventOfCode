
namespace AdventOfCode2025
{
    internal class Day06 : CustomDay
    {
        public override ValueTask<string> Solve_1()
        {
            var lines = GetInputLines();
            var problemCount = lines[0].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Length;
            var problems = new List<Problem>();
            for (var i = 0; i < problemCount; i++)
            {
                problems.Add(new Problem());
            }

            foreach (var line in lines)
            {
                var split = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                if (char.IsNumber(line[0]))
                {
                    for (var i = 0; i < split.Length; i++)
                    {
                        problems[i].Numbers.Add(long.Parse(split[i]));
                    }
                }
                else
                {
                    for (var i = 0; i < split.Length; i++)
                    {
                        problems[i].Operation = split[i].First(); ;
                    }
                }
            }

            long total = 0;

            foreach(var problem in problems)
            {
                if (problem.Operation == '+')
                {
                    total += problem.Numbers.Sum();
                }
                else
                {
                    total += problem.Numbers.Aggregate((res, cur) => res * cur);
                }
            }

            return ValueTask.FromResult(total.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var lines = GetInputLines();
            lines.Reverse();

            List<Problem> problems = [];
            Problem? problem = null;

            for(var ind = 0; ind < lines[0].Length; ind++)
            {
                if (!char.IsWhiteSpace(lines[0][ind]))
                {
                    if (problem is not null)
                    {
                        problems.Add(problem);
                    }
                    problem = new Problem
                    {
                        Operation = lines[0][ind]
                    };
                }

                var number = $"{lines[4][ind]}{lines[3][ind]}{lines[2][ind]}{lines[1][ind]}";

                if(string.IsNullOrWhiteSpace(number))
                {
                    continue;
                }
                else
                {
                    problem.Numbers.Add(long.Parse(number));
                }
            }

            problems.Add(problem);

            long total = 0;

            foreach (var calc in problems)
            {
                if (calc.Operation == '+')
                {
                    total += calc.Numbers.Sum();
                }
                else
                {
                    total += calc.Numbers.Aggregate((res, cur) => res * cur);
                }
            }

            return ValueTask.FromResult(total.ToString());
        }

        private class Problem
        {
            public List<long> Numbers { get; set; } = [];
            public char Operation { get; set; }
        }
    }
}
