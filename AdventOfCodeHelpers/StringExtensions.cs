using System.Security.Cryptography;
using System.Text;

namespace AdventOfCodeHelpers
{
    public static class StringExtensions
    {
        public static string Md5Hash(this string input) => Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes(input))).ToLower();
    }
}
