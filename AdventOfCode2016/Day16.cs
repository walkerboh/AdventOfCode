using System.Text;

namespace AdventOfCode2016
{
    internal class Day16 : CustomDay
    {
        public override ValueTask<string> Solve_1()
        {
            var maxSize = 272;
            var data = GetInputString();

            while(data.Length < maxSize)
            {
                data = $"{data}0{string.Join("", data.Reverse().Select(c => c == '1' ? '0' : '1'))}";
            }

            data = data[0..maxSize];

            var checkSum = CheckSum(data);

            while(checkSum.Length % 2 == 0)
            {
                checkSum = CheckSum(checkSum);
            }

            return ValueTask.FromResult(checkSum);

            static string CheckSum(string input)
            {
                var sb = new StringBuilder();

                for(var i = 0; i < input.Length - 1; i+=2)
                {
                    if (input[i] == input[i+1])
                    {
                        sb.Append('1');
                    }
                    else
                    {
                        sb.Append('0');
                    }
                }

                return sb.ToString();
            }
        }

        public override ValueTask<string> Solve_2()
        {
            var maxSize = 35651584;
            var data = GetInputString();

            while (data.Length < maxSize)
            {
                data = $"{data}0{string.Join("", data.Reverse().Select(c => c == '1' ? '0' : '1'))}";
            }

            data = data[0..maxSize];

            var checkSum = CheckSum(data);

            while (checkSum.Length % 2 == 0)
            {
                checkSum = CheckSum(checkSum);
            }

            return ValueTask.FromResult(checkSum);

            static string CheckSum(string input)
            {
                var sb = new StringBuilder();

                for (var i = 0; i < input.Length - 1; i += 2)
                {
                    if (input[i] == input[i + 1])
                    {
                        sb.Append('1');
                    }
                    else
                    {
                        sb.Append('0');
                    }
                }

                return sb.ToString();
            }
        }
    }
}
