namespace AdventOfCode2016
{
    internal class Day07 : CustomDay
    {
        private readonly List<string> Addresses;

        public Day07()
        {
            Addresses = GetInputLines();
        }

        public override ValueTask<string> Solve_1()
        {
            var total = 0;

            foreach (var address in Addresses)
            {
                var inBrackets = false;
                var valid = false;

                for (var i = 0; i < address.Length - 3; i++)
                {
                    var sub = address[i..(i + 4)];
                    if (sub.Contains('['))
                    {
                        inBrackets = true;
                    }
                    else if (sub.Contains(']'))
                    {
                        inBrackets = false;
                    }
                    else if (sub[0] == sub[3] && sub[1] == sub[2] && sub[0] != sub[1])
                    {
                        if (!inBrackets)
                        {
                            valid = true;
                        }
                        else
                        {
                            valid = false;
                            break;
                        }
                    }
                }

                if (valid)
                {
                    total++;
                }
            }

            return ValueTask.FromResult(total.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var total = 0;

            foreach (var address in Addresses)
            {
                var inBrackets = false;
                var outBracket = new List<string>();
                var inBracket = new List<string>();


                for (var i = 0; i < address.Length - 2; i++)
                {
                    var sub = address[i..(i + 3)];
                    if (sub.Contains('['))
                    {
                        inBrackets = true;
                    }
                    else if (sub.Contains(']'))
                    {
                        inBrackets = false;
                    }
                    else if (sub[0] == sub[2] && sub[0] != sub[1])
                    {
                        if (inBrackets)
                        {
                            inBracket.Add(sub);
                        }
                        else
                        {
                            outBracket.Add(sub);
                        }
                    }
                }

                if(inBracket.Any(s => outBracket.Contains($"{s[1]}{s[0]}{s[1]}")))
                {
                    total++;
                }
            }

            return ValueTask.FromResult(total.ToString());
        }
    }
}
