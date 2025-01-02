using System.Text.RegularExpressions;

namespace AdventOfCode2024
{
    internal class Day3
    {
        private readonly string data;

        public Day3()
        {
            data = File.ReadAllText("Data\\Day3.txt");
        }

        public int Problem1()
        {
            return Regex.Matches(data, @"mul\((\d{1,3}),(\d{1,3})\)").Sum(match => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));
        }

        public int Problem2()
        {
            var enabled = true;
            var pos = 0;
            var total = 0;
            while(pos < data.Length)
            {
                var nextDo = data.IndexOf("do()", pos);
                var nextDont = data.IndexOf("don't()", pos);

                if(enabled)
                {
                    total += Regex.Matches(data[pos..(nextDont < 0 ? data.Length : nextDont)], @"mul\((\d{1,3}),(\d{1,3})\)").Sum(match => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));
                    enabled = false;
                    if(nextDont < 0)
                    {
                        break;
                    }
                    pos = nextDont + 8;
                }
                else
                {
                    enabled = true;
                    if(nextDo < 0)
                    {
                        break;
                    }
                    pos = nextDo + 4;
                }
            }

            return total;
        }
    }
}
