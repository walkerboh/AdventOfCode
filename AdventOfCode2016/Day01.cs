namespace AdventOfCode2016
{
    internal class Day01 : CustomDay
    {
        private readonly List<string> Instructions;

        public Day01()
        {
            Instructions = [.. GetInputString().Split(',', StringSplitOptions.TrimEntries)];
        }

        public override ValueTask<string> Solve_1()
        {
            var facing = Direction.North;
            (int x, int y) curPos = (0, 0);

            foreach (var instruction in Instructions)
            {
                var turn = instruction[0];
                var distance = int.Parse(instruction[1..]);

                facing = Turn(facing, turn);
                curPos = Move(curPos, facing, distance);
            }

            return ValueTask.FromResult((Math.Abs(curPos.x) + Math.Abs(curPos.y)).ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var facing = Direction.North;
            (int x, int y) curPos = (0, 0);
            HashSet<(int x, int y)> visited = [(0, 0)];

            foreach (var instruction in Instructions)
            {
                var turn = instruction[0];
                var distance = int.Parse(instruction[1..]);

                facing = Turn(facing, turn);
                
                for(var i = 0; i < distance; i++)
                {
                    curPos = Move(curPos, facing, 1);

                    if(visited.Contains(curPos))
                    {
                        return ValueTask.FromResult((Math.Abs(curPos.x) + Math.Abs(curPos.y)).ToString());
                    }

                    visited.Add(curPos);
                }
            }

            return ValueTask.FromResult("Apparently the instructions were bad");
        }

        private static Direction Turn(Direction current, char turn)
        {
            var turnRight = turn == 'R';

            return current switch
            {
                Direction.North => turnRight ? Direction.East : Direction.West,
                Direction.East => turnRight ? Direction.South : Direction.North,
                Direction.South => turnRight ? Direction.West : Direction.East,
                Direction.West => turnRight ? Direction.North : Direction.South,
                _ => throw new Exception("Cardinal directions don't work like that"),
            };
        }

        private static (int x, int y) Move((int x, int y) curPos, Direction facing, int distance)
        {
            return facing switch
            {
                Direction.North => (curPos.x, curPos.y + distance),
                Direction.East => (curPos.x + distance, curPos.y),
                Direction.South => (curPos.x, curPos.y - distance),
                Direction.West => (curPos.x - distance, curPos.y),
                _ => throw new Exception("Cardinal directions still don't work like that"),
            };
        }

        private enum Direction
        {
            North,
            East,
            South,
            West
        }
    }
}
