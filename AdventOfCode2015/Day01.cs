using AoCHelper;

namespace AdventOfCode2015
{
    internal class Day01 : BaseDay
    {
        private readonly string Instructions = string.Empty;

        public Day01()
        {
            Instructions = File.ReadAllText(InputFilePath);
        }

        public override ValueTask<string> Solve_1()
        {
            var floor = 0;

            foreach (var sym in Instructions)
            {
                if (sym == '(')
                {
                    floor++;
                }
                else
                {
                    floor--;
                }
            }

            return ValueTask.FromResult(floor.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var floor = 0;

            for (int i = 0; i < Instructions.Length; i++)
            {
                if (Instructions[i] == '(')
                {
                    floor++;
                }
                else
                {
                    floor--;
                }

                if (floor == -1)
                {
                    return ValueTask.FromResult((i + 1).ToString());
                }
            }

            return ValueTask.FromResult("error");
        }
    }
}
