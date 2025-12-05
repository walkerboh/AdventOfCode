namespace AdventOfCode2015
{
    internal class Day23 : CustomDay
    {
        private readonly List<string> Instructions;

        public Day23()
        {
            Instructions = GetInputLines();
        }

        public override ValueTask<string> Solve_1()
        {
            uint a = 0, b = 0;
            var pos = 0;

            while (pos < Instructions.Count)
            {
                var ins = Instructions[pos];

                var cmd = ins[0..3];
                var args = ins[3..].Trim();

                var amt = 0;

                switch (cmd)
                {
                    case "hlf":
                        if (args == "a")
                        {
                            a /= 2;
                        }
                        else
                        {
                            b /= 2;
                        }
                        pos++;
                        break;
                    case "tpl":
                        if (args == "a")
                        {
                            a *= 3;
                        }
                        else
                        {
                            b *= 3;
                        }
                        pos++;
                        break;
                    case "inc":
                        if (args == "a")
                        {
                            a++;
                        }
                        else
                        {
                            b++;
                        }
                        pos++;
                        break;
                    case "jmp":
                        amt = int.Parse(args);
                        pos += amt;
                        break;
                    case "jie":
                        var split = args.Split(',', StringSplitOptions.TrimEntries);
                        var reg = split[0];
                        amt = int.Parse(split[1]);

                        if ((reg == "a" && a % 2 == 0) || (reg == "b" && b % 2 == 0))
                        {
                            pos += amt;
                        }
                        else
                        {
                            pos++;
                        }
                        break;
                    case "jio":
                        split = args.Split(',', StringSplitOptions.TrimEntries);
                        reg = split[0];
                        amt = int.Parse(split[1]);

                        if ((reg == "a" && a == 1) || (reg == "b" && b == 1))
                        {
                            pos += amt;
                        }
                        else
                        {
                            pos++;
                        }
                        break;
                }
            }

            return ValueTask.FromResult(b.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            uint a = 1, b = 0;
            var pos = 0;

            while (pos < Instructions.Count)
            {
                var ins = Instructions[pos];

                var cmd = ins[0..3];
                var args = ins[3..].Trim();

                var amt = 0;

                switch (cmd)
                {
                    case "hlf":
                        if (args == "a")
                        {
                            a /= 2;
                        }
                        else
                        {
                            b /= 2;
                        }
                        pos++;
                        break;
                    case "tpl":
                        if (args == "a")
                        {
                            a *= 3;
                        }
                        else
                        {
                            b *= 3;
                        }
                        pos++;
                        break;
                    case "inc":
                        if (args == "a")
                        {
                            a++;
                        }
                        else
                        {
                            b++;
                        }
                        pos++;
                        break;
                    case "jmp":
                        amt = int.Parse(args);
                        pos += amt;
                        break;
                    case "jie":
                        var split = args.Split(',', StringSplitOptions.TrimEntries);
                        var reg = split[0];
                        amt = int.Parse(split[1]);

                        if ((reg == "a" && a % 2 == 0) || (reg == "b" && b % 2 == 0))
                        {
                            pos += amt;
                        }
                        else
                        {
                            pos++;
                        }
                        break;
                    case "jio":
                        split = args.Split(',', StringSplitOptions.TrimEntries);
                        reg = split[0];
                        amt = int.Parse(split[1]);

                        if ((reg == "a" && a == 1) || (reg == "b" && b == 1))
                        {
                            pos += amt;
                        }
                        else
                        {
                            pos++;
                        }
                        break;
                }
            }

            return ValueTask.FromResult(b.ToString());
        }
    }
}
