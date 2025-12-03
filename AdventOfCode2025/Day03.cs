using System.Formats.Asn1;
using System.Text;

namespace AdventOfCode2025
{
    internal class Day03 : CustomDay
    {
        private readonly IEnumerable<string> Banks;

        public Day03()
        {
            Banks = GetInputLines();
        }

        public override ValueTask<string> Solve_1()
        {
            var total = 0;

            foreach (var bank in Banks)
            {
                var best = 0;

                for (var i = 0; i < bank.Length - 1; i++)
                {
                    for (var j = i + 1; j < bank.Length; j++)
                    {
                        var joltage = int.Parse($"{bank[i]}{bank[j]}");

                        if (joltage > best)
                        {
                            best = joltage;
                        }
                    }
                }

                total += best;
            }

            return ValueTask.FromResult(total.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            long total = 0;

            foreach(var bank in Banks)
            {
                var sb = new StringBuilder();
                var curPos = 0;

                while(sb.Length < 12)
                {
                    var target = 9;

                    while(target >= 0)
                    {
                        var index = bank.IndexOf(target.ToString(), curPos);
                        if (index == -1 || (sb.Length + bank[index..].Length < 12))
                        {
                            target--;
                        }
                        else
                        {
                            sb.Append(target);
                            curPos = index + 1;
                            break;
                        }
                    }
                }

                total += long.Parse(sb.ToString());
            }

            return ValueTask.FromResult(total.ToString());
        }

        #region Part 2 Exhaustive Search bad

        //    long total = 0;

        //    var memo = new Dictionary<string, long>();

        //        foreach (var bank in Banks)
        //        {
        //            memo.Clear();

        //            total += Search(string.Empty, bank);
        //}

        //        return ValueTask.FromResult(total.ToString());

        //        long Search(string current, string remaining)
        //{
        //    if (memo.TryGetValue(current, out var best))
        //    {
        //        return best;
        //    }

        //    if (current.Length + remaining.Length < 12)
        //    {
        //        return 0;
        //    }

        //    if (current.Length == 12)
        //    {
        //        return long.Parse(current);
        //    }

        //    for (var i = 0; i < remaining.Length; i++)
        //    {
        //        var foundBest = Search($"{current}{remaining[i]}", remaining[(i + 1)..]);
        //        var curBest = memo.GetValueOrDefault(current);

        //        if (foundBest > curBest)
        //        {
        //            memo[current] = foundBest;
        //        }
        //    }

        //    return memo[current];
        //}

        #endregion
    }
}
