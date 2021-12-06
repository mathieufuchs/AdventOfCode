namespace AdventOfCode
{
    public class Solver6 : ISolver
    {
        public Solver6()
        {
        }
        public async Task<string> Solve1(string input)
        {
            //input = UseDummyInput();

            var rows = input.Split(",").Select(r => int.Parse(r)).ToArray();

            var result = Solve(rows, 80);

            return result.ToString();
        }
        public async Task<string> Solve2(string input)
        {
            var rows = input.Split(",").Select(r => int.Parse(r)).ToArray();

            var result = Solve(rows, 256);

            return result.ToString();
        }

        public static long Solve(int[] numbers, int iterations)
        {
            var counters = Enumerable.Range(0, 9).Select(i => numbers.LongCount(n => n == i)).ToArray();

            for(var iter = 0; iter < iterations; iter++)
            {
                long zeros = counters[0];

                for (var i = 0; i < 9; i++)
                {
                    counters[i] = i switch
                    {
                        8 => zeros,
                        6 => zeros + counters[i + 1],
                        _ => counters[i + 1]
                    };
                }
            }

            return counters.Sum();
        }

        private string UseDummyInput()
        {
            return "3,4,3,1,2";
        }
    }
}
