using Spectre.Console;
using System.Text.RegularExpressions;

namespace AdventOfCode2016
{
    internal partial class Day08 : CustomDay
    {
        private readonly List<string> Instructions;
        private readonly bool[,] Display = new bool[6, 50];

        public Day08()
        {
            Instructions = GetInputLines();
        }

        public override ValueTask<string> Solve_1()
        {
            foreach(var instruction in Instructions)
            {
                if(instruction.StartsWith("rect"))
                {
                    var nums = instruction[5..].Split('x').Select(int.Parse).ToList();
                    var width = nums[0];
                    var height = nums[1];

                    for(var i = 0; i < height; i++)
                    {
                        for(var j = 0; j < width; j++)
                        {
                            Display[i, j] = true;
                        }
                    }
                }
                else if (instruction.StartsWith("rotate row"))
                {
                    var match = RotateRowRegex().Match(instruction);

                    var row = int.Parse(match.Groups[1].Value);
                    var distance = int.Parse(match.Groups[2].Value);

                    var newRow = new bool[50];

                    for(var i = 0; i < newRow.Length; i++)
                    {
                        newRow[(i + distance) % newRow.Length] = Display[row, i];
                    }

                    for(var i = 0; i < newRow.Length; i++)
                    {
                        Display[row, i] = newRow[i];
                    }
                }
                else if (instruction.StartsWith("rotate column"))
                {
                    var match = RotateColumnRegex().Match(instruction);

                    var col = int.Parse(match.Groups[1].Value);
                    var distance = int.Parse(match.Groups[2].Value);

                    var newCol = new bool[6];

                    for (var i = 0; i < newCol.Length; i++)
                    {
                        newCol[(i + distance) % newCol.Length] = Display[i, col];
                    }

                    for (var i = 0; i < newCol.Length; i++)
                    {
                        Display[i, col] = newCol[i];
                    }
                }
            }

            var total = 0;

            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < 50; j++)
                {
                    if (Display[i, j])
                    {
                        total++;
                    }
                }
            }

            return ValueTask.FromResult(total.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            for (var i = 0; i < 6; i++)
            {
                for (var j = 0; j < 50; j++)
                {
                    Console.Write(Display[i, j] ? '#' : ' ');
                }
                Console.WriteLine();
            }

            return ValueTask.FromResult("See Console");
        }

        [GeneratedRegex(@"rotate row y=(\d+) by (\d+)")]
        private static partial Regex RotateRowRegex();
        [GeneratedRegex(@"rotate column x=(\d+) by (\d+)")]
        private static partial Regex RotateColumnRegex();
    }
}
