namespace AdventOfCode
{
    public class Solver14 : ISolver
    {
        public Solver14()
        {
        }
        public async Task<string> Solve1(string input)
        {
            var (template, steps) = ParseInput(input);

            // var result = Compute(template, steps, 10);
            var result2 = ComputeCount(template, steps, 10);

            return result2.ToString();
        }

        public (string template, Dictionary<string, string> steps) ParseInput(string input)
        {
            /*input = @"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C";*/

            var rows = input.GetRows();

            var template = rows.First();

            var steps = rows.Skip(2).Select(r => r.Split(" -> ")).ToDictionary(e => e[0], e => e[1]);

            return (template, steps);
        }

        public int Compute(string template, Dictionary<string, string> steps, int iterations)
        {
            foreach (var _ in Enumerable.Range(1, iterations))
            {
                var stopIndex = template.Length - 1;
                var newTemplate = template[..1];
                for (var i = 0; i < stopIndex; i++)
                {
                    var pair = template[i..(i + 2)];

                    var element = steps[pair];

                    newTemplate += element + pair[1];
                }

                template = newTemplate;
            }

            var elements = template.GroupBy(c => c).Select(c => c.Count()).ToList();
            var mostCommon = elements.Max();
            var leastCommon = elements.Min();

            return (mostCommon - leastCommon);
        }

        public long ComputeCount(string template, Dictionary<string, string> steps, int iterations)
        {
            var pairs = new Dictionary<string, long>();
            for (var i = 0; i < template.Length - 1; i++)
            {
                pairs[template[i..(i + 2)]] = pairs.GetValueOrDefault(template[i..(i + 2)]) + 1;
            }

            foreach (var _ in Enumerable.Range(1, iterations))
            {
                var newPairs = new Dictionary<string, long>();

                foreach (var pair in pairs)
                {
                    var element = steps[pair.Key];
                    var pair1 = pair.Key[0] + element;
                    var pair2 = element + pair.Key[1];
                    newPairs[pair1] = newPairs.GetValueOrDefault(pair1) + pair.Value;
                    newPairs[pair2] = newPairs.GetValueOrDefault(pair2) + pair.Value;
                }

                pairs = newPairs;
            }

            var letterCount = new Dictionary<char, long>
            {
                [template[0]] = 1
            };

            foreach (var pair in pairs)
            {
                letterCount[pair.Key[1]] = letterCount.GetValueOrDefault(pair.Key[1]) + pair.Value;
            }

            var count = letterCount.Select(x => x.Value).OrderBy(x => x).ToList();

            var result = count.Last() - count.First();

            return result;
        }

        public async Task<string> Solve2(string input)
        {
            var (template, steps) = ParseInput(input);

            // var bruteForce = Compute(template, steps, 40); => this takes too long to run!
            var result = ComputeCount(template, steps, 40);

            return result.ToString();
        }
    }
}
