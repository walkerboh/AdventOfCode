namespace AdventOfCode2015
{
    internal class Day08 : CustomDay
    {
        private readonly IEnumerable<string> Lines;

        public Day08()
        {
            Lines = GetInputLines();
        }

        public override ValueTask<string> Solve_1()
        {
            var totalCode = 0;
            var totalChars = 0;

            foreach(var line in Lines)
            {
                var ind = 0;

                while(ind < line.Length)
                {
                    if (line[ind] == '"')
                    {
                        totalCode++;
                        ind++;
                    }
                    else if (line[ind] == '\\')
                    {
                        if (line[ind + 1] != 'x')
                        {
                            totalCode += 2;
                            totalChars++;
                            ind += 2;
                        }
                        else
                        {
                            totalCode += 4;
                            totalChars++;
                            ind += 4;
                        }
                    }
                    else
                    {
                        totalChars++;
                        totalCode++;
                        ind++;
                    }
                }
            }

            return ValueTask.FromResult((totalCode - totalChars).ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var total = 0;

            foreach (var line in Lines)
            {
                var newlines = ReplaceLiterals(line);
                total += $"\"{ReplaceLiterals(line)}\"".Length - line.Length;
            }

            return ValueTask.FromResult(total.ToString());

            static string ReplaceLiterals(string text) => text.Replace(@"\", @"\\").Replace(@"""", @"\""");
        }
    }
}
