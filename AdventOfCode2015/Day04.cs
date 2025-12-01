using AoCHelper;
using System.Security.Cryptography;

namespace AdventOfCode2015
{
    internal class Day04 : BaseDay
    {
        private readonly string Key;

        public Day04()
        {
            Key = File.ReadAllText(InputFilePath);
        }

        public override ValueTask<string> Solve_1()
        {
            var num = 1;

            while (true)
            {
                var inputBytes = System.Text.Encoding.ASCII.GetBytes($"{Key}{num}");
                var hashBytes = MD5.HashData(inputBytes);

                var hex = Convert.ToHexString(hashBytes);

                if (hex.StartsWith("00000"))
                {
                    return ValueTask.FromResult(num.ToString());
                }

                num++;
            }
        }

        public override ValueTask<string> Solve_2()
        {
            var num = 1;

            while (true)
            {
                var inputBytes = System.Text.Encoding.ASCII.GetBytes($"{Key}{num}");
                var hashBytes = MD5.HashData(inputBytes);

                var hex = Convert.ToHexString(hashBytes);

                if (hex.StartsWith("000000"))
                {
                    return ValueTask.FromResult(num.ToString());
                }

                num++;
            }
        }
    }
}
