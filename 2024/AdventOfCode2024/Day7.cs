namespace AdventOfCode2024
{
    internal class Day7
    {
        private readonly List<Equation> data;

        public Day7()
        {
            data = File.ReadAllLines("Data\\Day7.txt").Select(d => new Equation(d)).ToList();
        }

        /// <summary>
        /// Release - 4.93ms avg
        /// </summary>
        /// <returns></returns>
        public long Problem1Iterative()
        {
            long total = 0;

            foreach (var row in data)
            {
                var rowTotals = new List<long>() { row.Numbers[0] };

                foreach (var num in row.Numbers[1..])
                {
                    var tempTotals = new List<long>();

                    foreach (var tot in rowTotals)
                    {
                        tempTotals.Add(tot + num);
                        tempTotals.Add(tot * num);
                    }

                    rowTotals = tempTotals.Where(t => t <= row.Total).ToList();
                }

                if (rowTotals.Any(t => t == row.Total))
                {
                    total += row.Total;
                }
            }

            return total;
        }

        /// <summary>
        /// Release - 46.27ms avg
        /// </summary>
        /// <returns></returns>
        public long Problem1Recursive()
        {
            long total = 0;

            foreach (var row in data)
            {
                var addTotal = DoMath(row.Numbers, 0, Operator.Add);
                if (addTotal.Any(t => t == row.Total))
                {
                    total += row.Total;
                }
                else
                {
                    var multTotal = DoMath(row.Numbers, 0, Operator.Mult);
                    if (multTotal.Any(t => t == row.Total))
                    {
                        total += row.Total;
                    }
                }
            }

            return total;

            static IEnumerable<long> DoMath(List<long> nums, long runningTotal, Operator op)
            {
                var totals = new List<long>();
                var newRunningTotal = op == Operator.Add ? runningTotal + nums[0] : runningTotal * nums[0];

                if (nums.Count == 1)
                {
                    totals.Add(newRunningTotal);
                    return totals;
                }
                else
                {
                    totals.AddRange(DoMath(nums[1..], newRunningTotal, Operator.Add));
                    totals.AddRange(DoMath(nums[1..], newRunningTotal, Operator.Mult));
                    return totals;
                }
            }
        }

        public long Problem1()
        {
            return Problem1Iterative();
        }

        public long Problem2()
        {
            long total = 0;

            foreach (var row in data)
            {
                var rowTotals = new List<long>() { row.Numbers[0] };

                foreach (var num in row.Numbers[1..])
                {
                    var tempTotals = new List<long>();

                    foreach (var tot in rowTotals)
                    {
                        tempTotals.Add(tot + num);
                        tempTotals.Add(tot * num);
                        tempTotals.Add(long.Parse($"{tot}{num}"));
                    }

                    rowTotals = tempTotals.Where(t => t <= row.Total).ToList();
                }

                if (rowTotals.Any(t => t == row.Total))
                {
                    total += row.Total;
                }
            }

            return total;
        }

        enum Operator
        {
            Add,
            Mult
        }

        private class Equation
        {
            public Equation(string data)
            {
                var parts = data.Split(':');
                Total = long.Parse(parts[0]);
                Numbers = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            }

            
            public readonly List<long> Numbers;
            public readonly long Total;
        }
    }
}
