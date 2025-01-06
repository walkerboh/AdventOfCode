namespace AdventOfCode2024
{
    internal class Day22
    {
        private readonly List<long> secretNumbers;

        public Day22()
        {
            secretNumbers = File.ReadAllLines("Data\\Day22.txt").Select(long.Parse).ToList();
        }

        public long Problem1()
        {
            return secretNumbers.Sum(sn => GetNthSecrentNumber(sn, 2000));
        }

        public long Problem2()
        {
            var allSequencePrices = secretNumbers.Select(GetSequencePrices);
            var totalSequencePrices = new Dictionary<string, long>();
            var all = allSequencePrices.SelectMany(s => s).ToList();

            foreach (var sequencePrice in allSequencePrices.SelectMany(s => s))
            {
                if(totalSequencePrices.TryGetValue(sequencePrice.Key, out var value))
                {
                    totalSequencePrices[sequencePrice.Key] = value + sequencePrice.Value;
                }
                else
                {
                    totalSequencePrices[sequencePrice.Key] = sequencePrice.Value;
                }
            }

            return totalSequencePrices.Max(tsp => tsp.Value);
        }

        private Dictionary<string, long> GetSequencePrices(long number)
        {
            var priceChanges = new List<(long Price, long Change)>();
            var sequencePrices = new Dictionary<string, long>();

            for(var i = 0; i < 2000; i++)
            {
                var newNumber = CalculateNextSecretNumber(number);
                priceChanges.Add((newNumber % 10, newNumber % 10 - number % 10));
                number = newNumber;
            }

            for(var i = 0; i < priceChanges.Count - 4; i++)
            {
                var sequence = $"{priceChanges[i].Change},{priceChanges[i + 1].Change},{priceChanges[i + 2].Change},{priceChanges[i + 3].Change}";
                if(!sequencePrices.ContainsKey(sequence))
                {
                    sequencePrices[sequence] = priceChanges[i + 3].Price;
                }
            }

            return sequencePrices;
        }
        
        private long GetNthSecrentNumber(long number, int count)
        {
            for(var i = 0; i < count; i++)
            {
                number = CalculateNextSecretNumber(number);
            }

            return number;
        }

        private long CalculateNextSecretNumber(long number)
        {
            number = Prune(Mix(number, number * 64));
            number = Prune(Mix(number, number / 32));
            number = Prune(Mix(number, number * 2048));

            return number;

            long Mix(long a, long b) => a ^ b;
            long Prune(long a) => a % 16777216;
        }
    }
}
