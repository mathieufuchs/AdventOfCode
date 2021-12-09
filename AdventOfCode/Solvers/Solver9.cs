namespace AdventOfCode
{
    public class Solver9 : ISolver
    {
        public Solver9()
        {
        }
        public async Task<string> Solve1(string input)
        {
            int[][] grid = input.Split(Environment.NewLine).Select(s => s.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

            var iMax = grid.Length;
            var jMax = grid[0].Length;

            var counter = 0;

            for (var i = 0; i < iMax; i++)
            {
                for (var j = 0; j < jMax; j++)
                {
                    var level = grid[i][j];
                    var directions = new (int, int)?[4];

                    directions[0] = i > 0 ? (i - 1, j) : null;
                    directions[1] = i < iMax - 1 ? (i + 1, j) : null;
                    directions[2] = j > 0 ? (i, j - 1) : null;
                    directions[3] = j < jMax - 1 ? (i, j + 1) : null;

                    var validDirections = directions.Where(d => d.HasValue);
                    counter += validDirections.All(d => grid[d.Value.Item1][d.Value.Item2] > level) ? level + 1 : 0;
                }
            }

            return counter.ToString();
        }
        public async Task<string> Solve2(string input)
        {
            /*input = @"2199943210
3987894921
9856789892
8767896789
9899965678";*/

            Floor[][] grid = input.Split(Environment.NewLine).Select(s => s.Select(c => new Floor(int.Parse(c.ToString()))).ToArray()).ToArray();

            var iMax = grid.Length;
            var jMax = grid[0].Length;

            var basins = new Dictionary<int, int>();
            var newBasinId = 0;



            for (var i = 0; i < iMax; i++)
            {
                for (var j = 0; j < jMax; j++)
                {
                    var level = grid[i][j];

                    if (level.Traversed)
                    {
                        continue;
                    }

                    level.Traversed = true;

                    var directions = new (int, int)?[4];

                    directions[0] = i > 0 ? (i - 1, j) : null;
                    directions[1] = i < iMax - 1 ? (i + 1, j) : null;
                    directions[2] = j > 0 ? (i, j - 1) : null;
                    directions[3] = j < jMax - 1 ? (i, j + 1) : null;

                    var validDirections = directions.Where(d => d.HasValue);
                    var floors = validDirections.Select(d => grid[d.Value.Item1][d.Value.Item2]).Where(d => d.HasBasin);

                    if (floors.Count() > 1)
                    {
                        var smallestBassin = floors.OrderBy(f => f.BasinId).First().BasinId.Value;
                        foreach (var f in floors)
                        {
                            if (f.BasinId == smallestBassin) { continue; }

                            if (basins.ContainsKey(f.BasinId.Value))
                            {
                                basins[smallestBassin] += basins[f.BasinId.Value];
                                basins.Remove(f.BasinId.Value);
                            }

                            foreach(var fixBasin in grid.SelectMany(g => g).Where(g => g.BasinId == f.BasinId))
                            {
                                fixBasin.BasinId = smallestBassin;
                            }
                        }
                    }

                    var basinId = floors.FirstOrDefault()?.BasinId;

                    if (!basinId.HasValue)
                    {
                        newBasinId++;
                        basins.Add(newBasinId, 1);
                        level.BasinId = newBasinId;
                    }
                    else
                    {
                        basins[basinId.Value] += 1;
                        level.BasinId = basinId;
                    }
                }
            }

            var largest = basins.OrderByDescending(b => b.Value).Take(3).Select(b => b.Value).ToList();

            var result = largest.Aggregate(1, (acc, val) => acc * val);

            return result.ToString();
        }
    }

    public class Floor
    {
        public Floor(int level)
        {
            Level = level;
            if (level == 9) Traversed = true;
        }

        public int Level { get; }
        public bool Traversed { get; set; }

        public bool HasBasin => BasinId.HasValue;

        public int? BasinId { get; set; }
    }
}
