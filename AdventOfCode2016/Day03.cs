namespace AdventOfCode2016
{
    internal class Day03 : CustomDay
    {
        public override ValueTask<string> Solve_1()
        {
            var total = 0;

            var lines = GetInputLines();

            foreach (var line in lines)
            {
                var numbers = line.Split(' ', (StringSplitOptions)3).Select(int.Parse).ToList();

                if (numbers[0] + numbers[1] > numbers[2]
                    && numbers[1] + numbers[2] > numbers[0]
                    && numbers[2] + numbers[0] > numbers[1])
                {
                    total++;
                }
            }

            return ValueTask.FromResult(total.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var total = 0;

            var lines = GetInputLines();

            for (var i = 0; i < lines.Count; i += 3)
            {
                var line1Split = lines[i].Split(' ', (StringSplitOptions)3).Select(int.Parse).ToList();
                var line2Split = lines[i + 1].Split(' ', (StringSplitOptions)3).Select(int.Parse).ToList();
                var line3Split = lines[i + 2].Split(' ', (StringSplitOptions)3).Select(int.Parse).ToList();

                var triangle1 = new int[] { line1Split[0], line2Split[0], line3Split[0] };
                var triangle2 = new int[] { line1Split[1], line2Split[1], line3Split[1] };
                var triangle3 = new int[] { line1Split[2], line2Split[2], line3Split[2] };

                if (triangle1[0] + triangle1[1] > triangle1[2]
                    && triangle1[1] + triangle1[2] > triangle1[0]
                    && triangle1[2] + triangle1[0] > triangle1[1])
                {
                    total++;
                }

                if (triangle2[0] + triangle2[1] > triangle2[2]
                    && triangle2[1] + triangle2[2] > triangle2[0]
                    && triangle2[2] + triangle2[0] > triangle2[1])
                {
                    total++;
                }

                if (triangle3[0] + triangle3[1] > triangle3[2]
                    && triangle3[1] + triangle3[2] > triangle3[0]
                    && triangle3[2] + triangle3[0] > triangle3[1])
                {
                    total++;
                }
            }

            return ValueTask.FromResult(total.ToString());
        }
    }
}
