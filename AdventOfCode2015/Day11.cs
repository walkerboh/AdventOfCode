using System.Text.RegularExpressions;

namespace AdventOfCode2015
{
    internal partial class Day11 : CustomDay
    {
        public override ValueTask<string> Solve_1()
        {
            var oldPassword = GetInputString();

            var newPassword = NextPassword(oldPassword);

            while(!IsValidPassword(newPassword))
            {
                newPassword = NextPassword(newPassword);
            }

            Task1Result = newPassword;

            return ValueTask.FromResult(newPassword);
        }

        private string? Task1Result;

        public override ValueTask<string> Solve_2()
        {
            var oldPassword = Task1Result;

            var newPassword = NextPassword(oldPassword);

            while (!IsValidPassword(newPassword))
            {
                newPassword = NextPassword(newPassword);
            }

            return ValueTask.FromResult(newPassword);
        }

        private readonly char[] BadLetters = ['i', 'o', 'l'];

        [GeneratedRegex(@"(\w)\1")]
        public static partial Regex DoubleLetters();

        private bool IsValidPassword(string password)
        {
            if (password.Intersect(BadLetters).Any())
            {
                return false;
            }

            var matches = DoubleLetters().Matches(password);

            if (matches.Count < 2)
            {
                return false;
            }

            for(var i = 0; i < password.Length - 2; i++)
            {
                if (password[i] + 1 == password[i + 1] && password[i + 1] + 1 == password[i + 2])
                {
                    return true;
                }
            }

            return false;
        }

        private static string NextPassword(string password)
        {
            var letters = password.ToArray();
            var ind = letters.Length - 1;

            while(ind > 0)
            {
                if (letters[ind] != 'z')
                {
                    letters[ind] = (char)(letters[ind] + 1);
                    break;
                }
                else
                {
                    letters[ind] = 'a';
                    ind--;
                }
            }

            return new string(letters);
        }
    }
}
