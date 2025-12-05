namespace AdventOfCode2015
{
    internal class Day25 : CustomDay
    {
        private readonly int TargetRow;
        private readonly int TargetColumn;

        public Day25()
        {
            var input = GetInputString().Split(' ', StringSplitOptions.TrimEntries);
            TargetRow = int.Parse(input[0]);
            TargetColumn = int.Parse(input[1]);
        }

        public override ValueTask<string> Solve_1()
        {
            var startRow = 2;
            long value = 20151125;

            while (true)
            {
                for (var col = 1; col <= startRow; col++)
                {
                    var row = startRow - col + 1;

                    value = (value * 252533) % 33554393;

                    if (row == TargetRow && col == TargetColumn)
                    {
                        return ValueTask.FromResult(value.ToString());
                    }
                }

                startRow++;
            }
        }

        public override ValueTask<string> Solve_2()
        {
            throw new NotImplementedException();
        }
    }
}
