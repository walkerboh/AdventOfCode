namespace AdventOfCode2015
{
    internal class Day22 : CustomDay
    {
        public readonly int StartingBossHitPoints;
        private readonly int BossDamage;

        public Day22()
        {
            var lines = GetInputLines();
            StartingBossHitPoints = int.Parse(lines[0].Split(":", StringSplitOptions.TrimEntries)[1]);
            BossDamage = int.Parse(lines[1].Split(":", StringSplitOptions.TrimEntries)[1]);
        }

        public override ValueTask<string> Solve_1()
        {
            Queue<BattleState> states = [];
            states.Enqueue(new BattleState(StartingBossHitPoints));

            var bestManaSpend = int.MaxValue;

            while (states.Count > 0)
            {
                var state = states.Dequeue();

                if(state.PoisonTurns > 0)
                {
                    state.BossHitPoints -= 3;
                    state.PoisonTurns--;
                }

                if(state.RechargeTurns > 0)
                {
                    state.Mana += 101;
                    state.RechargeTurns--;
                }

                if(state.ShieldTurns > 0)
                {
                    state.ShieldTurns--;
                }

                if(state.BossHitPoints <= 0)
                {
                    if(state.ManaSpent <  bestManaSpend)
                    {
                        bestManaSpend = state.ManaSpent;
                    }

                    continue;
                }

                if(state.ManaSpent > bestManaSpend)
                {
                    continue;
                }

                if(!state.PlayerTurn)
                {
                    state.HitPoints -= BossDamage - (state.ShieldTurns > 0 ? 7 : 0);

                    if(state.HitPoints <= 0)
                    {
                        continue;
                    }

                    state.PlayerTurn = true;
                    states.Enqueue(state);
                }
                else
                {
                    var availableSpells = state.AvailableSpells();

                    if(availableSpells.Count == 0)
                    {
                        continue;
                    }

                    foreach(var spell in availableSpells)
                    {
                        var newState = state.ActionSpell(spell);

                        if(newState.BossHitPoints <= 0)
                        {
                            if (newState.ManaSpent < bestManaSpend)
                            {
                                bestManaSpend = newState.ManaSpent;
                            }

                            continue;
                        }

                        newState.PlayerTurn = false;
                        states.Enqueue(newState);
                    }
                }
            }

            return ValueTask.FromResult(bestManaSpend.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            Queue<BattleState> states = [];
            states.Enqueue(new BattleState(StartingBossHitPoints));

            var bestManaSpend = int.MaxValue;

            while (states.Count > 0)
            {
                var state = states.Dequeue();

                if(state.PlayerTurn)
                {
                    state.HitPoints--;

                    if(state.HitPoints <= 0)
                    {
                        continue;
                    }
                }

                if (state.PoisonTurns > 0)
                {
                    state.BossHitPoints -= 3;
                    state.PoisonTurns--;
                }

                if (state.RechargeTurns > 0)
                {
                    state.Mana += 101;
                    state.RechargeTurns--;
                }

                if (state.ShieldTurns > 0)
                {
                    state.ShieldTurns--;
                }

                if (state.BossHitPoints <= 0)
                {
                    if (state.ManaSpent < bestManaSpend)
                    {
                        bestManaSpend = state.ManaSpent;
                    }

                    continue;
                }

                if (state.ManaSpent > bestManaSpend)
                {
                    continue;
                }

                if (!state.PlayerTurn)
                {
                    state.HitPoints -= BossDamage - (state.ShieldTurns > 0 ? 7 : 0);

                    if (state.HitPoints <= 0)
                    {
                        continue;
                    }

                    state.PlayerTurn = true;
                    states.Enqueue(state);
                }
                else
                {
                    var availableSpells = state.AvailableSpells();

                    if (availableSpells.Count == 0)
                    {
                        continue;
                    }

                    foreach (var spell in availableSpells)
                    {
                        var newState = state.ActionSpell(spell);

                        if (newState.BossHitPoints <= 0)
                        {
                            if (newState.ManaSpent < bestManaSpend)
                            {
                                bestManaSpend = newState.ManaSpent;
                            }

                            continue;
                        }

                        newState.PlayerTurn = false;
                        states.Enqueue(newState);
                    }
                }
            }

            return ValueTask.FromResult(bestManaSpend.ToString());
        }

        private class BattleState(int startingBossHitPoints)
        {
            public BattleState(BattleState other) : this(other.BossHitPoints)
            {
                PlayerTurn = other.PlayerTurn;
                HitPoints = other.HitPoints;
                Mana = other.Mana;
                ManaSpent = other.ManaSpent;
                ShieldTurns = other.ShieldTurns;
                PoisonTurns = other.PoisonTurns;
                RechargeTurns = other.RechargeTurns;
            }

            public bool PlayerTurn { get; set; } = true;

            public int HitPoints { get; set; } = 50;
            public int Mana { get; set; } = 500;
            public int ManaSpent { get; set; }
            public int BossHitPoints { get; set; } = startingBossHitPoints;
            
            public int ShieldTurns { get; set; }
            public int PoisonTurns { get; set; }
            public int RechargeTurns { get; set; }

            public List<Spell> AvailableSpells()
            {
                List<Spell> spells = [];

                if(Mana >= 53)
                {
                    spells.Add(Spell.MagicMissle);
                }

                if(Mana >= 73)
                {
                    spells.Add(Spell.Drain);
                }

                if(ShieldTurns == 0 && Mana >= 113)
                {
                    spells.Add(Spell.Shield);
                }
                
                if (PoisonTurns == 0 && Mana >= 173)
                {
                    spells.Add(Spell.Poison);
                }

                if (RechargeTurns == 0 && Mana >= 229)
                {
                    spells.Add(Spell.Recharge);
                }

                return spells;
            }

            public BattleState ActionSpell(Spell spell)
            {
                var newState = new BattleState(this);

                switch (spell)
                {
                    case Spell.MagicMissle:
                        newState.Mana -= 53;
                        newState.ManaSpent += 53;
                        newState.BossHitPoints -= 4;
                        return newState;
                    case Spell.Drain:
                        newState.Mana -= 73;
                        newState.ManaSpent += 73;
                        newState.BossHitPoints -= 2;
                        newState.HitPoints += 2;
                        return newState;
                    case Spell.Shield:
                        newState.Mana -= 113;
                        newState.ManaSpent += 113;
                        newState.ShieldTurns = 6;
                        return newState;
                    case Spell.Poison:
                        newState.Mana -= 173;
                        newState.ManaSpent += 173;
                        newState.PoisonTurns = 6;
                        return newState;
                    case Spell.Recharge:
                        newState.Mana -= 229;
                        newState.ManaSpent += 229;
                        newState.RechargeTurns = 5;
                        return newState;
                }

                throw new Exception("Desmond the Moon Bear");
            }
        }

        private enum Spell
        {
            MagicMissle,
            Drain,
            Shield,
            Poison,
            Recharge
        }
    }
}
