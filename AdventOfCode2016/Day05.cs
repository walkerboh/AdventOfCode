using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode2016
{
    internal class Day05 : CustomDay
    {
        public override ValueTask<string> Solve_1()
        {
            var id = GetInputString();
            var sb = new StringBuilder();
            var ind = 1;

            while(sb.Length < 8)
            {
                var hash = MD5.HashData(Encoding.ASCII.GetBytes($"{id}{ind++}"));
                var hex = Convert.ToHexString(hash);
                if (hex[..5] == "00000")
                {
                    sb.Append(hex[5]);
                }
            }

            return ValueTask.FromResult(sb.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var id = GetInputString();
            var pwd = new char?[8];
            Array.Fill(pwd, null);
            var ind = 1;

            while (pwd.Any(x => x is null))
            {
                var hash = MD5.HashData(Encoding.ASCII.GetBytes($"{id}{ind++}"));
                var hex = Convert.ToHexString(hash);
                if (hex[..5] == "00000" && int.TryParse(hex[5].ToString(), out var pwdInd) && pwdInd >= 0 && pwdInd <8 && pwd[pwdInd] is null)
                {
                    pwd[pwdInd] = hex[6];
                }
            }

            return ValueTask.FromResult(string.Join("", pwd));
        }
    }
}
