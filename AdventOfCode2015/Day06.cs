using AoCHelper;
using System.Text.RegularExpressions;

namespace AdventOfCode2015
{
    internal partial class Day06 : BaseDay
    {
        [GeneratedRegex(@"^((turn on)|(turn off)|(toggle)) (\d{1,3}),(\d{1,3}) through (\d{1,3}),(\d{1,3})$")]
        private static partial Regex InstructionsRegex();

        private readonly IEnumerable<string> Instructions;

        public Day06()
        {
            Instructions = File.ReadAllLines(InputFilePath);
        }

        public override ValueTask<string> Solve_1()
        {
            var lights = new bool[1000, 1000];

            foreach (var line in Instructions)
            {
                var match = InstructionsRegex().Match(line);

                if (!match.Success)
                {
                    throw new Exception($"Bad regex parsing on {line}");
                }

                var action = match.Groups[1].Value switch
                {
                    "turn on" => Action.On,
                    "turn off" => Action.Off,
                    "toggle" => Action.Toggle,
                    _ => throw new Exception("Bad action parse")
                };

                var startX = int.Parse(match.Groups[5].Value);
                var startY = int.Parse(match.Groups[6].Value);
                var endX = int.Parse(match.Groups[7].Value);
                var endY = int.Parse(match.Groups[8].Value);

                for (var i = startX; i <= endX; i++)
                {
                    for (var j = startY; j <= endY; j++)
                    {
                        lights[i, j] = action switch
                        {
                            Action.On => true,
                            Action.Off => false,
                            Action.Toggle => !lights[i, j]
                        };
                    }
                }
            }

            var total = 0;

            for(var i = 0; i < 1000; i++)
            {
                for (var j = 0; j < 1000; j++)
                {
                    if (lights[i, j])
                    {
                        total++;
                    }
                }
            }

            return ValueTask.FromResult(total.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var lights = new int[1000, 1000];

            foreach (var line in Instructions)
            {
                var match = InstructionsRegex().Match(line);

                if (!match.Success)
                {
                    throw new Exception($"Bad regex parsing on {line}");
                }

                var action = match.Groups[1].Value switch
                {
                    "turn on" => Action.On,
                    "turn off" => Action.Off,
                    "toggle" => Action.Toggle,
                    _ => throw new Exception("Bad action parse")
                };

                var startX = int.Parse(match.Groups[5].Value);
                var startY = int.Parse(match.Groups[6].Value);
                var endX = int.Parse(match.Groups[7].Value);
                var endY = int.Parse(match.Groups[8].Value);

                for (var i = startX; i <= endX; i++)
                {
                    for (var j = startY; j <= endY; j++)
                    {
                        lights[i, j] = action switch
                        {
                            Action.On => lights[i, j] + 1,
                            Action.Off => Math.Max(0, lights[i, j] - 1),
                            Action.Toggle => lights[i, j] + 2
                        };
                    }
                }
            }

            var total = 0;

            for (var i = 0; i < 1000; i++)
            {
                for (var j = 0; j < 1000; j++)
                {
                    total += lights[i, j];
                }
            }

            return ValueTask.FromResult(total.ToString());
        }

        private enum Action
        {
            On,
            Off,
            Toggle
        }
    }
}
