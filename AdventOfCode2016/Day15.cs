using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2016
{
    internal partial class Day15 : CustomDay
    {
        private readonly List<Disc> Discs = [];

        public override ValueTask<string> Solve_1()
        {
            LoadDiscs();

            var t = 0;

            while(true)
            {
                if (Discs.All(d => d.PositionAtRelativeTime(t) == 0))
                {
                    return ValueTask.FromResult(t.ToString());
                }

                t++;
            }
        }

        public override ValueTask<string> Solve_2()
        {
            LoadDiscs();

            Discs.Add(new Disc(Discs.Count + 1, 11, 0));

            var t = 0;

            while (true)
            {
                if (Discs.All(d => d.PositionAtRelativeTime(t) == 0))
                {
                    return ValueTask.FromResult(t.ToString());
                }

                t++;
            }
        }

        private class Disc(int number, int positions, int start)
        {
            public int Number { get; set; } = number;
            public int Positions { get; set; } = positions;
            public int Start { get; set; } = start;
            public int PositionAtTime(int t) => (Start + t) % Positions;
            public int PositionAtRelativeTime(int t) => (Start + t + Number) % Positions;
        }

        private void LoadDiscs()
        {
            Discs.Clear();

            var lines = GetInputLines();

            foreach (var line in lines)
            {
                var match = DiscRegex().Match(line);
                Discs.Add(new Disc(
                    int.Parse(match.Groups[1].Value),
                    int.Parse(match.Groups[2].Value),
                    int.Parse(match.Groups[3].Value)));
            }
        }

        [GeneratedRegex(@"Disc #(\d) has (\d+) positions; at time=0, it is at position (\d+).")]
        private static partial Regex DiscRegex();
    }
}
