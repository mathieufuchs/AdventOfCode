namespace AdventOfCode
{
    public class Solver11 : ISolver
    {
        public Solver11()
        {
        }
        public async Task<string> Solve1(string input)
        {
            var octopuses = Init(input);

            foreach (var i in Enumerable.Range(0, 100))
            {
                foreach (var octopus in octopuses)
                {
                    octopus.Flash();
                }

                foreach (var octopus in octopuses)
                {
                    octopus.Reset();
                }
            }


            var flashes = 0;

            foreach (var octopus in octopuses)
            {
                flashes += octopus.Flashes;
            }

            return flashes.ToString();
        }

        public Octopus[,] Init(string input)
        {
            /*input = @"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526";*/

            Octopus[][] grid = input.Split(Environment.NewLine).Select(s => s.Select(c => new Octopus(int.Parse(c.ToString()))).ToArray()).ToArray();

            var iMax = grid.Length;
            var jMax = grid[0].Length;

            var octopuses = new Octopus[iMax, jMax];

            for (var i = 0; i < iMax; i++)
            {
                for (var j = 0; j < jMax; j++)
                {
                    var octopus = grid[i][j];
                    octopus.Left = i > 0 ? grid[i - 1][j] : null;
                    octopus.Right = i < iMax - 1 ? grid[i + 1][j] : null;
                    octopus.Top = j > 0 ? grid[i][j - 1] : null;
                    octopus.Bottom = j < jMax - 1 ? grid[i][j + 1] : null;

                    octopus.TopLeft = octopus.Top != null && octopus.Left != null ? grid[i - 1][j - 1] : null;
                    octopus.TopRight = octopus.Top != null && octopus.Right != null ? grid[i + 1][j - 1] : null;
                    octopus.BottomLeft = octopus.Bottom != null && octopus.Left != null ? grid[i - 1][j + 1] : null;
                    octopus.BottomRight = octopus.Bottom != null && octopus.Right != null ? grid[i + 1][j + 1] : null;

                    octopuses[i, j] = octopus;
                }
            }

            return octopuses;
        }

        public async Task<string> Solve2(string input)
        {
            var octopuses = Init(input);

            var flashPoint = 0;

            foreach (var i in Enumerable.Range(1, int.MaxValue))
            {
                bool allFlashed = true;
                foreach (var octopus in octopuses)
                {
                    octopus.Flash();
                }

                foreach (var octopus in octopuses)
                {
                    allFlashed = allFlashed && octopus.Flashed;
                    octopus.Reset();
                }

                if (allFlashed)
                {
                    flashPoint = i;

                    break;
                }
            }

            return flashPoint.ToString();
        }
        
        public class Octopus
        {
            public Octopus Left { get; set; }
            public Octopus Right { get; set; }
            public Octopus Top { get; set; }
            public Octopus Bottom { get; set; }
            public Octopus TopRight { get; set; }
            public Octopus TopLeft { get; set; }
            public Octopus BottomLeft { get; set; }
            public Octopus BottomRight { get; set; }

            public bool Flashed { get; set; }

            public Octopus(int level)
            {
                Level = level;
            }

            public void Reset()
            {
                Flashed = false;
            }

            public void Flash()
            {
                if (Flashed) { return; }
                
                Level++;

                if (Level < 10)
                {
                    return;
                }

                Level = 0;
                Flashed = true;
                Flashes++;

                Left?.Flash();
                Right?.Flash();
                Top?.Flash();
                Bottom?.Flash();
                TopRight?.Flash();
                TopLeft?.Flash();
                BottomLeft?.Flash();
                BottomRight?.Flash();
            }

            public int Level { get; set; }

            public int Flashes { get; set; }
        }
    }
}
