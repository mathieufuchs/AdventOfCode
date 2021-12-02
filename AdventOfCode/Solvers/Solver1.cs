namespace AdventOfCode
{
    public class Solver1 : ISolver
    {
        public Solver1()
        {
        }

        public async Task<string> Solve1(string input)
        {
            var increased = 0;
            var windowSize = 1;

            var rows = input.GetRows().Select(r => int.Parse(r));

            foreach (var (prev, next) in GetSlidingWindow(rows, windowSize))
            {
                if (next.Sum() > prev.Sum()) increased++;
            }

            return increased.ToString();
        }

        public async Task<string> Solve2(string input)
        {
            var increased = 0;
            var windowSize = 3;

            var rows = input.GetRows().Select(r => int.Parse(r));

            foreach (var (prev, next) in GetSlidingWindow(rows, windowSize))
            {
                if (next.Sum() > prev.Sum()) increased++;
            }

            return increased.ToString();
        }

        private IEnumerable<(IEnumerable<int> prev, IEnumerable<int> next)> GetSlidingWindow(IEnumerable<int> rows, int windowSize)
        {
            for (var i = 1; i < rows.Count(); i++)
            {
                var prev = rows.Skip(i - 1).Take(windowSize);
                var next = rows.Skip(i).Take(windowSize);

                yield return (prev, next);
            }
        }
    }
}
