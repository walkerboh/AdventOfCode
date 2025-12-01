using System.Security.Cryptography;

namespace AdventOfCode2024
{
    internal class Day25
    {
        private readonly List<List<int>> locks = [];
        private readonly List<List<int>> keys = [];

        public Day25()
        {
            var lines = File.ReadAllLines("Data\\Day25.txt");

            var lineNum = 0;

            while(lineNum < lines.Length)
            {
                if (lines[lineNum][0] == '#')
                {
                    locks.Add(ParseLock(lines[lineNum..(lineNum + 7)]));
                }
                else
                {
                    keys.Add(ParseKey(lines[lineNum..(lineNum + 7)]));
                }

                lineNum += 8;
            }

            List<int> ParseKey(string[] lines)
            {
                return ParseLock(lines.Reverse().ToArray());
            }

            List<int> ParseLock(string[] lines)
            {
                var heights = Enumerable.Repeat(0, lines[0].Length).ToList();

                foreach(var line in lines[1..])
                {
                    for(var i = 0; i < line.Length; i++)
                    {
                        if (line[i] == '#')
                        {
                            heights[i]++;
                        }
                    }
                }

                return heights;
            }
        }

        public int Problem1()
        {
            return keys.Sum(k => locks.Count(l => KeyLockFit(k, l)));

            bool KeyLockFit(List<int> k, List<int> l)
            {
                return k.Zip(l, (kh, lh) => kh + lh).All(h => h <= 5);
            }
        }
    }
}
