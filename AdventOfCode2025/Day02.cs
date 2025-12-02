using System.Text.RegularExpressions;

namespace AdventOfCode2025
{
    internal class Day02 : CustomDay
    {
        private readonly string IdRanges;

        public Day02()
        {
            IdRanges = GetInputString();
        }

        public override ValueTask<string> Solve_1()
        {
            long total = 0;

            foreach (var range in IdRanges.Split(','))
            {
                var split = range.Split('-');
                var min = long.Parse(split[0]);
                var max = long.Parse(split[1]);

                for (var i = min; i <= max; i++)
                {
                    var str = i.ToString();
                    var halfLength = str.Length / 2;

                    var regex = @$"^(.{{{halfLength}}})\1$";
                    var match = Regex.Match(str, regex);

                    if(match.Success)
                    {
                        total += i;
                    }
                }
            }

            return ValueTask.FromResult(total.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            long total = 0;

            foreach (var range in IdRanges.Split(','))
            {
                var split = range.Split('-');
                var min = long.Parse(split[0]);
                var max = long.Parse(split[1]);

                for (var i = min; i <= max; i++)
                {
                    var str = i.ToString();
                    var halfLength = str.Length / 2;

                    for (var j = 1; j <= halfLength; j++)
                    {
                        var regex = @$"^(.{{{j}}})(\1)+$";
                        var match = Regex.Match(str, regex);

                        if (match.Success)
                        {
                            total += i;
                            break;
                        }
                    }
                }
            }

            return ValueTask.FromResult(total.ToString());
        }
    }
}
