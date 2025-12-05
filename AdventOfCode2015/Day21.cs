namespace AdventOfCode2015
{
    internal class Day21 : CustomDay
    {
        private readonly int StartingBossHitPoints;
        private readonly int BossDamage;
        private readonly int BossArmor;

        private static readonly List<(int cost, int damage)> Weapons = [(8, 4), (10, 5), (25, 6), (40, 7), (74, 8)];
        private static readonly List<(int cost, int armor)> Armor = [(0, 0), (13, 1), (31, 2), (53, 3), (75, 4), (102, 5)];
        private static readonly List<(int cost, int damage, int armor)> Rings = [(25, 1, 0), (50, 2, 0), (100, 3, 0), (20, 0, 1), (40,0, 2), (80, 0, 3)];
        private static readonly List<IEnumerable<(int cost, int damage, int armor)>> RingCombos = [.. Rings.GetCombinations().Where(c => c.Count() <= 2)];

        private List<EquipSet> EquipSets = [];

        public Day21()
        {
            var lines = GetInputLines();
            StartingBossHitPoints = int.Parse(lines[0].Split(":", StringSplitOptions.TrimEntries)[1]);
            BossDamage = int.Parse(lines[1].Split(":", StringSplitOptions.TrimEntries)[1]);
            BossArmor = int.Parse(lines[2].Split(":", StringSplitOptions.TrimEntries)[1]);
        }

        public override ValueTask<string> Solve_1()
        {
            foreach(var weapon in Weapons)
            {
                foreach(var armor in Armor)
                {
                    var baseCost = weapon.cost + armor.cost;
                    EquipSets.Add(new(baseCost, weapon.damage, armor.armor));

                    foreach(var combo in RingCombos)
                    {
                        EquipSets.Add(new(baseCost + combo.Sum(c => c.cost), weapon.damage + combo.Sum(c => c.damage), armor.armor + combo.Sum(c => c.armor)));
                    }
                }
            }

            List<EquipSet> sortedEquipSets = [.. EquipSets.OrderBy(es => es.Cost)];

            foreach(var set in sortedEquipSets)
            {
                if(SimulateBattle(set.Damage, set.Armor))
                {
                    return ValueTask.FromResult(set.Cost.ToString());
                }
            }

            return ValueTask.FromResult("It's dangerous to go alone.");
        }

        public override ValueTask<string> Solve_2()
        {

            List<EquipSet> sortedEquipSets = [.. EquipSets.OrderByDescending(es => es.Cost)];

            foreach (var set in sortedEquipSets)
            {
                if (!SimulateBattle(set.Damage, set.Armor))
                {
                    return ValueTask.FromResult(set.Cost.ToString());
                }
            }

            return ValueTask.FromResult("It's dangerous to go alone.");
        }

        private bool SimulateBattle(int damage, int armor)
        {
            var playerHitPoints = 100;
            var bossHitPoints = StartingBossHitPoints;
            var playerTurn = true;

            while (playerHitPoints > 0 && bossHitPoints > 0)
            {
                if (playerTurn)
                {
                    bossHitPoints -= Math.Max(1, damage - BossArmor);
                }
                else
                {
                    playerHitPoints -= Math.Max(1, BossDamage - armor);
                }

                playerTurn = !playerTurn;
            }

            return playerHitPoints > 0;
        }

        private class EquipSet(int cost, int damage, int armor)
        {
            public int Cost { get; set; } = cost;
            public int Damage { get; set; } = damage;
            public int Armor { get; set; } = armor; 
        }
    }
}
