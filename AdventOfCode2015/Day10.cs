using System.Text;

namespace AdventOfCode2015
{
    internal class Day10 : CustomDay
    {
        public override ValueTask<string> Solve_1()
        {
            var numberString = GetInputString();

            for(var i = 0; i < 40; i++)
            {
                numberString = LookAndSay(numberString);
            }

            return ValueTask.FromResult(numberString.Length.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var numberString = GetInputString();

            for (var i = 0; i < 50; i++)
            {
                numberString = LookAndSay(numberString);
            }

            return ValueTask.FromResult(numberString.Length.ToString());
        }

        private static string LookAndSay(string input)
        {
            var ind = 0;
            var sb = new StringBuilder();

            while (ind < input.Length)
            {
                var num = input[ind];
                var cnt = 1;

                while (ind < input.Length - 1 && num == input[ind + 1])
                {
                    cnt++;
                    ind++;
                }

                ind++;

                sb.Append($"{cnt}{num}");
            }

            return sb.ToString();
        }
    }
}
