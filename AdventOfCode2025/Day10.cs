using Microsoft.Z3;

namespace AdventOfCode2025
{
    internal class Day10 : CustomDay
    {
        public override ValueTask<string> Solve_1()
        {
            var lines = GetInputLines();

            List<(bool[] Goal, List<List<int>> Buttons)> machines = [];

            long total = 0;

            foreach(var line in lines)
            {
                var lights = line[1..line.IndexOf(']')];
                var buttons = line[line.IndexOf('(')..line.IndexOf('{')];

                machines.Add(([.. lights.Select(c => c == '#')], [.. buttons.Split(' ', (StringSplitOptions)3).Select(b => b.Trim('(', ')')).Select(b => b.Split(',')).Select(b => b.Select(int.Parse).ToList())]));
            }

            foreach(var machine in machines)
            {
                var queue = new Queue<(bool[] lights, int steps)>();
                queue.Enqueue((new bool[machine.Goal.Length], 0));
                var best = int.MaxValue;
                var seen = new HashSet<string>();
                var found = false;

                while(queue.TryDequeue(out var state))
                {
                    if(state.steps > best || seen.Contains(Hash(state.lights)))
                    {
                        continue;
                    }

                    seen.Add(Hash(state.lights));

                    foreach(var button in machine.Buttons)
                    {
                        bool[] newLights = [.. state.lights];

                        foreach(var ind in button)
                        {
                            newLights[ind] = !newLights[ind];
                        }

                        if (Enumerable.SequenceEqual(newLights, machine.Goal))
                        {
                            found = true;
                            if(state.steps + 1 < best)
                            {
                                best = state.steps + 1;
                            }

                            continue;
                        }
                        else
                        {
                            if (!seen.Contains(Hash(newLights)))
                            {
                                queue.Enqueue((newLights, state.steps + 1));
                            }
                        }
                    }
                }

                if(!found)
                {
                    throw new Exception("whoa");
                }

                total += best;
            }

            return ValueTask.FromResult(total.ToString());

            static string Hash(bool[] lights) => $"{string.Join("", lights.Select(l => l ? '#' : '.'))}";
        }

        // Learning Z3
        // Thanks to this existing solution for giving me a framework to understand how Z3 works: https://github.com/mohammedsouleymane/AdventOfCode/blob/main/AdventOfCode/Aoc2025/Day10.cs
        public override ValueTask<string> Solve_2()
        {
            var lines = GetInputLines();

            List<(int[] Goal, List<List<int>> Buttons)> machines = [];

            long total = 0;

            foreach (var line in lines)
            {
                var lights = line[1..line.IndexOf(']')];
                var buttons = line[line.IndexOf('(')..line.IndexOf('{')];
                var joltages = line[(line.IndexOf('{') + 1)..^1];

                machines.Add(([.. joltages.Split(',').Select(int.Parse)], [.. buttons.Split(' ', (StringSplitOptions)3).Select(b => b.Trim('(', ')')).Select(b => b.Split(',')).Select(b => b.Select(int.Parse).ToList())]));
            }

            foreach (var machine in machines)
            {
                using var context = new Context();

                // Create an Integer variable for each possible button (ie. a variable for the number of times each button is pressed)
                var variables = machine.Buttons.Select(b => context.MkIntConst(string.Join(',', b))).ToList();

                var optimizer = context.MkOptimize();

                for(var i = 0; i < machine.Goal.Length; i++)
                {
                    // Create a list of the button variables that could incrememnt the value for the given goal
                    List<IntExpr> vars = [];
                    for(var j = 0; j < machine.Buttons.Count; j++)
                    {
                        if (machine.Buttons[j].Contains(i))
                        {
                            vars.Add(variables[j]);
                        }
                    }

                    // No buttons hit this goal (shouldn't happen)
                    if(vars.Count == 0)
                    {
                        throw new Exception("Cannot solve machine, not all buttons pressable.");
                    }

                    // Sum those variables, add a constraint that the sum must be equal to the goal amount
                    var sum = context.MkAdd(vars);
                    var constraint = context.MkEq(sum, context.MkInt(machine.Goal[i]));
                    optimizer.Add(constraint);
                }

                // Create a sum of the total number of times the buttons are pressed, and tell the optimizer we want the smallest possible value for it
                var totalPresses = context.MkAdd(variables);
                optimizer.MkMinimize(totalPresses);

                // All button variables must be greater than or equal to 0, no negative button pressing
                foreach(var variable in variables)
                {
                    optimizer.Add(context.MkGe(variable, context.MkInt(0)));
                }

                var result = optimizer.Check();
                if(result != Status.SATISFIABLE)
                {
                    throw new Exception("Cannot solve machine.");
                }

                var model = optimizer.Model;

                total+= variables.Sum(v => ((IntNum)model.Evaluate(v)).Int);
            }

            return ValueTask.FromResult(total.ToString());
        }
    }
}
