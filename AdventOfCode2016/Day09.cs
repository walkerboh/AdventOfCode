using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2016
{
    internal partial class Day09 : CustomDay
    {
        public override ValueTask<string> Solve_1()
        {
            var compressed = GetInputString();
            var index = 0;
            var output = new StringBuilder();

            while (index < compressed.Length)
            {
                if (compressed[index] == '(')
                {
                    var closeIndex = compressed.IndexOf(')', index);
                    var command = compressed[index..(closeIndex + 1)];

                    var match = CommandRegex().Match(command);
                    var length = int.Parse(match.Groups[1].Value);
                    var times = int.Parse(match.Groups[2].Value);

                    var repeat = compressed[(closeIndex + 1)..(closeIndex + 1 + length)];

                    for (var i = 0; i < times; i++)
                    {
                        output.Append(repeat);
                    }

                    index = closeIndex + 1 + length;
                }
                else
                {
                    output.Append(compressed[index++]);
                }
            }

            return ValueTask.FromResult(output.Length.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var compressed = GetInputString();
            var index = 0;
            long length = 0;

            while (index < compressed.Length)
            {
                if (compressed[index] == '(')
                {
                    var closeIndex = compressed.IndexOf(')', index);
                    var command = compressed[index..(closeIndex + 1)];

                    var match = CommandRegex().Match(command);
                    var compressedLength = int.Parse(match.Groups[1].Value);
                    var times = int.Parse(match.Groups[2].Value);

                    var repeat = compressed[(closeIndex + 1)..(closeIndex + 1 + compressedLength)];

                    var decompressLength = GetLength(repeat);

                    length += decompressLength * times;
                    index = closeIndex + 1 + compressedLength;
                }
                else
                {
                    length++;
                    index++;
                }
            }

            return ValueTask.FromResult(length.ToString());

            long GetLength(string section)
            {
                long length = 0;
                var index = 0;

                while(index < section.Length)
                {
                    if (section[index] == '(')
                    {
                        var closeIndex = section.IndexOf(')', index);
                        var command = section[index..(closeIndex + 1)];

                        var match = CommandRegex().Match(command);
                        var compressedLength = int.Parse(match.Groups[1].Value);
                        var times = int.Parse(match.Groups[2].Value);

                        var repeat = section[(closeIndex + 1)..(closeIndex + 1 + compressedLength)];

                        var decompressLength = GetLength(repeat);

                        length += decompressLength * times;
                        index = closeIndex + 1 + compressedLength;
                    }
                    else
                    {
                        length++;
                        index++;
                    }
                }

                return length;
            }
        }

        [GeneratedRegex(@"\((\d+)x(\d+)\)")]
        private static partial Regex CommandRegex();
    }
}
