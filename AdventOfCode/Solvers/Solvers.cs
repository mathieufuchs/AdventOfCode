namespace AdventOfCode
{
    public static class Solvers
    {
        public static async Task<string> Solve(AdventDay adventDay, Puzzle puzzle, string input)
        {
            var solver = adventDay.GetSolver();
            return await solver.Solve(puzzle, input);
        }

        private static ISolver GetSolver(this AdventDay adventDay)
        {
            return adventDay switch
            {
                AdventDay._1 => new Solver1(),
                AdventDay._2 => new Solver2(),
                AdventDay._3 => new Solver3(),
                AdventDay._4 => new Solver4(),
                AdventDay._5 => new Solver5(),
                AdventDay._6 => new Solver6(),
                AdventDay._7 => new Solver7(),
                AdventDay._8 => new Solver8(),
                AdventDay._9 => new Solver9(),
                AdventDay._10 => new Solver10(),
                AdventDay._11 => new Solver11(),
                AdventDay._12 => new Solver12(),
                AdventDay._13 => new Solver13(),
                AdventDay._14 => new Solver14(),
                AdventDay._15 => new Solver15(),
                AdventDay._16 => new Solver16(),
                AdventDay._17 => new Solver17(),
                AdventDay._18 => new Solver18(),
                AdventDay._19 => new Solver19(),
                AdventDay._20 => new Solver20(),
                AdventDay._21 => new Solver21(),
                AdventDay._22 => new Solver22(),
                AdventDay._23 => new Solver23(),
                AdventDay._24 => new Solver24(),
                AdventDay._25 => new Solver25(),
                _ => throw new NotImplementedException(),
            };
        }

        public static async Task<string> Solve(this ISolver solver, Puzzle puzzle, string input)
        {
            return puzzle switch
            {
                Puzzle._1 => await solver.Solve1(input),
                Puzzle._2 => await solver.Solve2(input),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
