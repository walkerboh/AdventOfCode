
namespace AdventOfCode2015
{
    internal class Day19 : CustomDay
    {
        private readonly List<(string Inital, string Replace)> Replacements = [];
        private readonly string Molecule;

        public Day19()
        {
            foreach (var line in GetInputLines())
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                if (line.Contains("=>"))
                {
                    var split = line.Split("=>", StringSplitOptions.TrimEntries);
                    Replacements.Add((split[0], split[1]));
                }
                else
                {
                    Molecule = line;
                }
            }
        }

        public override ValueTask<string> Solve_1()
        {
            var set = new HashSet<string>();

            foreach (var replacement in Replacements)
            {
                var pos = Molecule.IndexOf(replacement.Inital);

                while (pos > -1)
                {
                    set.Add(Molecule.Remove(pos, replacement.Inital.Length).Insert(pos, replacement.Replace));
                    pos = Molecule.IndexOf(replacement.Inital, pos + 1);
                }
            }

            return ValueTask.FromResult(set.Count.ToString());
        }

        // End result, ran DFS for a while, added breakpoint, say best 212, let run for a while, still 212. Was correct
        public override ValueTask<string> Solve_2()
        {
            var step = 0;
            HashSet<string> molecules = [Molecule];

            while(!molecules.Contains("e"))
            {
                var newMolecules = new HashSet<string>();

                foreach(var molecule in molecules)
                {
                    newMolecules.UnionWith(PerformAllReplacements(molecule));
                }

                molecules = newMolecules;
            }

            return ValueTask.FromResult(step.ToString());

            IEnumerable<string> PerformAllReplacements(string molecule)
            {
                var set = new HashSet<string>();

                foreach (var replacement in Replacements)
                {
                    var pos = molecule.IndexOf(replacement.Replace);

                    while (pos > -1)
                    {
                        set.Add(molecule.Remove(pos, replacement.Replace.Length).Insert(pos, replacement.Inital));
                        pos = molecule.IndexOf(replacement.Replace, pos + 1);
                    }
                }

                return set;
            }

            // DFS slow
            //var best = int.MaxValue;

            //TryReplacements(Molecule, 0);

            //return ValueTask.FromResult(best.ToString());

            //void TryReplacements(string molecule, int steps)
            //{
            //    if (steps > best)
            //    {
            //        return;
            //    }

            //    if (molecule == "e")
            //    {
            //        if (steps < best)
            //        {
            //            best = steps;
            //        }

            //        return;
            //    }

            //    foreach (var replacement in Replacements)
            //    {
            //        var pos = molecule.IndexOf(replacement.Replace);

            //        if (pos > -1)
            //        {
            //            var newMolecule = molecule.Remove(pos, replacement.Replace.Length).Insert(pos, replacement.Inital);
            //            TryReplacements(newMolecule, steps + 1);
            //        }
            //    }
            //}
        }
    }
}
