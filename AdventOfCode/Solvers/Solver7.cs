namespace AdventOfCode
{
    public class Solver7 : ISolver
    {
        public Solver7()
        {
        }
        public async Task<string> Solve1(string input)
        {
            var result = Internal_Solve_1(input);
            return result.ToString();
        }

        public static int Internal_Solve_1(string input)
        {
            var crabPositions = input.Split(',').Select(m => int.Parse(m)).ToArray();

            var fuel = int.MaxValue;

            var posMin = crabPositions.Min();
            var posMax = crabPositions.Max();

            for (var targetPos = posMin; targetPos <= posMax; targetPos++)
            {
                var tempFuel = 0; 

                foreach(var crabPosition in crabPositions)
                {
                    tempFuel += Math.Abs(crabPosition - targetPos);
                }

                if(tempFuel < fuel)
                {
                    fuel = tempFuel;
                }
            }

            return fuel;
        }

        public static int Internal_Solve_2(string input)
        {
            var crabPositions = input.Split(',').Select(m => int.Parse(m)).ToArray();

            var fuel = int.MaxValue;

            var posMin = crabPositions.Min();
            var posMax = crabPositions.Max();

            for (var targetPos = posMin; targetPos <= posMax; targetPos++)
            {
                int tempFuel = 0;

                foreach (var crabPosition in crabPositions)
                {
                    int steps = Math.Abs(crabPosition - targetPos);
                    // tempFuel += Enumerable.Range(1, steps).Sum();        => this works but is much slower than the mathematical form below :) 
                    tempFuel += steps * (steps + 1) / 2;
                }

                if (tempFuel < fuel)
                {
                    fuel = tempFuel;
                }
            }

            return fuel;
        }

        public async Task<string> Solve2(string input)
        {
            var result = Internal_Solve_2(input);
            return result.ToString();
        }
    }
}
