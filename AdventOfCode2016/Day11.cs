using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2016
{
    internal class Day11 : CustomDay
    {
        private class Item(int type, bool chip, int floor)
        {
            public Item(Item other) : this(other.Type, other.Chip, other.Floor)
            {
            }

            public int Type { get; set; } = type;
            public bool Chip { get; set; } = chip;
            public int Floor { get; set; } = floor;
        }

        private readonly List<Item> InitialItems = [
            new(1, false, 1),
            new(2, false, 1),
            new(3, false, 1),
            new(4, false, 1),
            new(5, false, 1),
            new(1, true, 2),
            new(2, true, 1),
            new(3, true, 2),
            new(4, true, 1),
            new(5, true, 1),
            ];

        private readonly List<Item> ExtendedItems = [
            new (6, false, 1),
            new (6, true, 1),
            new (7, false, 1),
            new (7, true, 1),
            ];

        private class State
        {
            public State()
            {
            }

            [SetsRequiredMembers]
            public State(State other)
            {
                Items = [.. other.Items.Select(i => new Item(i))];
                Elevator = other.Elevator;
                Steps = other.Steps;
            }

            public required List<Item> Items { get; set; }
            public int Elevator { get; set; } = 1;
            public int Steps { get; set; } = 0;

            public bool IsValid()
            {
                foreach (var type in new int[] { 1, 2, 3, 4, 5 })
                {
                    var chip = Items.Single(i => i.Type == type && i.Chip);
                    if (Items.Single(i => i.Type == type && !i.Chip).Floor != chip.Floor && Items.Any(i => !i.Chip && i.Floor == chip.Floor))
                    {
                        return false;
                    }
                }

                return true;
            }

            public bool IsEndState() => Items.All(i => i.Floor == 4);

            public string GetHash() => $"{Elevator}-{string.Join('-', Items.GroupBy(i => i.Type).OrderBy(g => g.Key).Select(g => $"{g.First(i => i.Chip).Floor}&{g.First(i => !i.Chip).Floor}"))}";
        }

        public override ValueTask<string> Solve_1()
        {
            return ValueTask.FromResult($"{Search(new State() { Items = InitialItems })}");
        }

        //33 min 34 seconds synchronous
        public override ValueTask<string> Solve_2()
        {
            return ValueTask.FromResult($"{SearchAsync(new State() { Items = [.. InitialItems.Union(ExtendedItems)] })}");
        }

        private int Search(State initialState)
        {
            var queue = new Queue<State>();
            queue.Enqueue(initialState);
            var set = new HashSet<string>();
            var bestSteps = int.MaxValue;

            while (queue.Count != 0)
            {
                var state = queue.Dequeue();

                if (set.Contains(state.GetHash()) || state.Steps > bestSteps)
                {
                    continue;
                }
                else
                {
                    set.Add(state.GetHash());
                }

                var floorItems = state.Items.Where(i => i.Floor == state.Elevator);
                var possibleMoves = floorItems.Select(i => new List<Item> { i }).Union(floorItems.GetCombinations(2)).ToList();

                foreach (var possibleMove in possibleMoves)
                {
                    if (state.Elevator < 4)
                    {
                        var newState = new State(state);
                        foreach (var item in possibleMove)
                        {
                            newState.Items.Single(i => i.Type == item.Type && i.Chip == item.Chip).Floor++;
                        }
                        newState.Elevator++;
                        newState.Steps++;

                        if (newState.IsEndState() && newState.Steps < bestSteps)
                        {
                            bestSteps = newState.Steps;
                            continue;
                        }

                        if (newState.IsValid() && !set.Contains(newState.GetHash()))
                        {
                            queue.Enqueue(newState);
                        }
                    }

                    if (state.Elevator > 1)
                    {
                        var newState = new State(state);
                        foreach (var item in possibleMove)
                        {
                            newState.Items.Single(i => i.Type == item.Type && i.Chip == item.Chip).Floor--;
                        }
                        newState.Elevator--;
                        newState.Steps++;

                        if (newState.IsEndState() && newState.Steps < bestSteps)
                        {
                            bestSteps = newState.Steps;
                            continue;
                        }

                        if (newState.IsValid() && !set.Contains(newState.GetHash()))
                        {
                            queue.Enqueue(newState);
                        }
                    }
                }
            }

            return bestSteps;
        }

        private int SearchAsync(State initialState)
        {
            var queue = new ConcurrentQueue<State>();
            queue.Enqueue(initialState);
            var set = new ConcurrentBag<string>();
            var bestSteps = int.MaxValue;

            Action action = () =>
            {
                while (queue.TryDequeue(out var state))
                {
                    if (set.Contains(state.GetHash()) || state.Steps > bestSteps)
                    {
                        continue;
                    }
                    else
                    {
                        set.Add(state.GetHash());
                    }

                    var floorItems = state.Items.Where(i => i.Floor == state.Elevator);
                    var possibleMoves = floorItems.Select(i => new List<Item> { i }).Union(floorItems.GetCombinations(2)).ToList();

                    foreach (var possibleMove in possibleMoves)
                    {
                        if (state.Elevator < 4)
                        {
                            var newState = new State(state);
                            foreach (var item in possibleMove)
                            {
                                newState.Items.Single(i => i.Type == item.Type && i.Chip == item.Chip).Floor++;
                            }
                            newState.Elevator++;
                            newState.Steps++;

                            if (newState.IsEndState() && newState.Steps < bestSteps)
                            {
                                bestSteps = newState.Steps;
                                continue;
                            }

                            if (newState.IsValid() && !set.Contains(newState.GetHash()))
                            {
                                queue.Enqueue(newState);
                            }
                        }

                        if (state.Elevator > 1)
                        {
                            var newState = new State(state);
                            foreach (var item in possibleMove)
                            {
                                newState.Items.Single(i => i.Type == item.Type && i.Chip == item.Chip).Floor--;
                            }
                            newState.Elevator--;
                            newState.Steps++;

                            if (newState.IsEndState() && newState.Steps < bestSteps)
                            {
                                bestSteps = newState.Steps;
                                continue;
                            }

                            if (newState.IsValid() && !set.Contains(newState.GetHash()))
                            {
                                queue.Enqueue(newState);
                            }
                        }
                    }
                }
            };

            Parallel.Invoke(action, action, action, action);

            return bestSteps;
        }
    }
}
