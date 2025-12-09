using System.Text.RegularExpressions;

namespace AdventOfCode2016
{
    internal partial class Day10 : CustomDay
    {
        private readonly List<Bot> Bots = [];

        [GeneratedRegex(@"^value (\d+) goes to bot (\d+)$")]
        private static partial Regex ValueRegex();

        [GeneratedRegex(@"^bot (\d+) gives low to (\w+) (\d+) and high to (\w+) (\d+)$")]
        private static partial Regex RuleRegex();

        private void LoadBots()
        {
            Bots.Clear();

            var rules = GetInputLines();

            foreach (var rule in rules)
            {
                var match = ValueRegex().Match(rule);

                if (match.Success)
                {
                    var botId = int.Parse(match.Groups[2].Value);
                    var value = int.Parse(match.Groups[1].Value);

                    var bot = Bots.SingleOrDefault(b => b.Id == botId);

                    if (bot is not null)
                    {
                        bot.Holding.Add(value);
                    }
                    else
                    {
                        bot = new Bot(botId);
                        bot.Holding.Add(value);
                        Bots.Add(bot);
                    }

                    continue;
                }

                match = RuleRegex().Match(rule);

                if (match.Success)
                {
                    var botId = int.Parse(match.Groups[1].Value);
                    var lowBot = match.Groups[2].Value == "bot";
                    var low = int.Parse(match.Groups[3].Value);
                    var highBot = match.Groups[4].Value == "bot";
                    var high = int.Parse(match.Groups[5].Value);

                    var bot = Bots.SingleOrDefault(b => b.Id == botId);

                    if (bot is not null)
                    {
                        bot.LowTarget = low;
                        bot.LowBot = lowBot;
                        bot.HighTarget = high;
                        bot.HighBot = highBot;
                    }
                    else
                    {
                        bot = new Bot(botId)
                        {
                            LowTarget = low,
                            LowBot = lowBot,
                            HighTarget = high,
                            HighBot = highBot
                        };
                        Bots.Add(bot);
                    }

                    continue;
                }

                throw new Exception("Invalid rule parse");
            }
        }

        public override ValueTask<string> Solve_1()
        {
            LoadBots();

            while (true)
            {
                var endBot = Bots.SingleOrDefault(b => b.Holding.Contains(61) && b.Holding.Contains(17));

                if (endBot is not null)
                {
                    return ValueTask.FromResult(endBot.Id.ToString());
                }

                var actionBot = Bots.First(b => b.Holding.Count == 2);
                var low = actionBot.Holding.Min();
                var high = actionBot.Holding.Max();

                actionBot.Holding.Clear();

                if (actionBot.LowBot)
                {
                    Bots.Single(b => b.Id == actionBot.LowTarget).Holding.Add(low);
                }

                if (actionBot.HighBot)
                {
                    Bots.Single(b => b.Id == actionBot.HighTarget).Holding.Add(high);
                }
            }
        }

        public override ValueTask<string> Solve_2()
        {
            LoadBots();

            var outputs = new Dictionary<int, int>();

            while (Bots.Any(b => b.Holding.Count == 2))
            {
                var actionBot = Bots.First(b => b.Holding.Count == 2);
                var low = actionBot.Holding.Min();
                var high = actionBot.Holding.Max();

                actionBot.Holding.Clear();

                if (actionBot.LowBot)
                {
                    Bots.Single(b => b.Id == actionBot.LowTarget).Holding.Add(low);
                }
                else
                {
                    outputs.Add(actionBot.LowTarget ?? throw new Exception($"Invalid setup for bot {actionBot.Id}"), low);
                }

                if (actionBot.HighBot)
                {
                    Bots.Single(b => b.Id == actionBot.HighTarget).Holding.Add(high);
                }
                else
                {
                    outputs.Add(actionBot.HighTarget ?? throw new Exception($"Invalid setup for bot {actionBot.Id}"), high);
                }
            }

            return ValueTask.FromResult(((long)outputs[0] * outputs[1] * outputs[2]).ToString());
        }

        private class Bot(int id)
        {
            public int Id { get; set; } = id;
            public List<int> Holding { get; set; } = [];
            public int? LowTarget { get; set; }
            public bool LowBot { get; set; }
            public int? HighTarget { get; set; }
            public bool HighBot { get; set; }
        }
    }
}
