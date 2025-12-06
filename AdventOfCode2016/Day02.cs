using System.Text;

namespace AdventOfCode2016
{
    internal class Day02 : CustomDay
    {
        private readonly List<string> Instructions;

        public Day02()
        {
            Instructions = GetInputLines();
        }

        private static int[] UpBound = [1, 2, 3];
        private static int[] DownBound = [7, 8, 9];
        private static int[] RightBound = [3, 6, 9];
        private static int[] LeftBound = [1, 4, 7];

        public override ValueTask<string> Solve_1()
        {
            var pos = 5;
            var sb = new StringBuilder();

            foreach (var line in Instructions)
            {
                foreach (var move in line)
                {
                    pos = Move(pos, move);
                }

                sb.Append(pos);
            }

            return ValueTask.FromResult(sb.ToString());

            static int Move(int cur, char dir)
            {
                return dir switch
                {
                    'U' => !UpBound.Contains(cur) ? cur - 3 : cur,
                    'D' => !DownBound.Contains(cur) ? cur + 3 : cur,
                    'R' => !RightBound.Contains(cur) ? cur + 1 : cur,
                    'L' => !LeftBound.Contains(cur) ? cur - 1 : cur,
                    _ => throw new Exception("Non-orthagonal moves are for squares... wait...")
                };
            }
        }

        public override ValueTask<string> Solve_2()
        {
            var pos = '5';
            var sb = new StringBuilder();

            foreach(var line in Instructions)
            {
                foreach(var move in line)
                {
                    pos = Move2(pos, move);
                }

                sb.Append(pos);
            }

            return ValueTask.FromResult(sb.ToString());
        }

        private static char Move2(char cur, char dir)
        {
            switch (dir)
            {
                case 'U':
                    return cur switch
                    {
                        '3' => '1',
                        '6' => '2',
                        '7' => '3',
                        '8' => '4',
                        'A' => '6',
                        'B' => '7',
                        'C' => '8',
                        'D' => 'B',
                        _ => cur
                    };
                case 'D':
                    return cur switch
                    {
                        '1' => '3',
                        '2' => '6',
                        '3' => '7',
                        '4' => '8',
                        '6' => 'A',
                        '7' => 'B',
                        '8' => 'C',
                        'B' => 'D',
                        _ => cur
                    };
                case 'R':
                    return cur switch
                    {
                        '2' => '3',
                        '3' => '4',
                        '5' => '6',
                        '6' => '7',
                        '7' => '8',
                        '8' => '9',
                        'A' => 'B',
                        'B' => 'C',
                        _ => cur
                    };
                case 'L':
                    return cur switch
                    {
                        '3' => '2',
                        '4' => '3',
                        '6' => '5',
                        '7' => '6',
                        '8' => '7',
                        '9' => '8',
                        'B' => 'A',
                        'C' => 'B',
                        _ => cur
                    };
                default:
                    throw new Exception("That no direction");
            }
        }
    }
}
