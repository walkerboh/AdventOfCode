namespace AdventOfCode2015
{
    internal class Day20 : CustomDay
    {
        private readonly int Target;

        public Day20()
        {
            Target = int.Parse(GetInputString());
        }

        public override ValueTask<string> Solve_1()
        {
            var houses = new int[Target];
            Array.Fill(houses, 1);
            for (var elf = 2; elf < houses.Length; elf++)
            {
                for (var house = elf - 1; house < houses.Length; house += elf)
                {
                    houses[house] += elf * 10;
                }

                if (houses[elf - 1] >= Target)
                {
                    return ValueTask.FromResult(elf.ToString());
                }
            }

            return ValueTask.FromResult("error");
        }

        public override ValueTask<string> Solve_2()
        {
            var houses = new int[Target];
            Array.Fill(houses, 1);
            for (var elf = 2; elf < houses.Length; elf++)
            {
                var i = 0;
                for (var house = elf - 1; house < houses.Length; house += elf)
                {
                    houses[house] += elf * 11;
                    i++;
                    if (i >= 50)
                    {
                        break;
                    }
                }

                if (houses[elf - 1] >= Target)
                {
                    return ValueTask.FromResult(elf.ToString());
                }
            }

            return ValueTask.FromResult("error");
        }
    }
}
