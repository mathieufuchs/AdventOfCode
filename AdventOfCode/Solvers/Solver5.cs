using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Solver5 : ISolver
    {
        public Solver5()
        {
        }

        

        public async Task<string> Solve1(string input)
        {
            //input = UseDummyInput();

            var rows = input.GetRows();

            var coordinates = rows.Select(r => new Coordinates(r)).Where(c => c.IsLine());

            var xMax = coordinates.Max(c => c.Xmax());
            var yMax = coordinates.Max(c => c.Ymax());

            var grid = new Coordinate[xMax+1, yMax+1];

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    grid[x, y] = new Coordinate(x, y);
                }
            }

            foreach (var coordinate in coordinates)
            {
                if (coordinate.IsVertical())
                {
                    var line = Enumerable.Range(coordinate.Xmin(), 1 + coordinate.Xmax() - coordinate.Xmin()).Select(c => new Coordinate(c, coordinate.C1.Y));

                    foreach (var c in line)
                    {
                        grid[c.X, c.Y].Count += 1;
                    }
                }

                if (coordinate.IsHorizontal())
                {
                    var line = Enumerable.Range(coordinate.Ymin(), 1 + coordinate.Ymax() - coordinate.Ymin()).Select(c => new Coordinate(coordinate.C1.X, c));

                    foreach (var c in line)
                    {
                        grid[c.X, c.Y].Count += 1;
                    }
                }
            }

            var overlaps = 0;

            foreach (var c in grid)
            {
                if (c.Count >= 2) overlaps++;
            }

            //PrintGrid(grid);

            return overlaps.ToString();
        }

       


        public async Task<string> Solve2(string input)
        {
            //input = UseDummyInput();

            var rows = input.GetRows();

            var coordinates = rows.Select(r => new Coordinates(r)).Where(c => c.IsLine());

            var xMax = coordinates.Max(c => c.Xmax());
            var yMax = coordinates.Max(c => c.Ymax());

            var grid = new Coordinate[xMax + 1, yMax + 1];

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    grid[x, y] = new Coordinate(x, y);
                }
            }

            foreach (var coordinate in coordinates)
            {
                if (coordinate.IsVertical())
                {
                    var line = Enumerable.Range(coordinate.Xmin(), 1 + coordinate.Xmax() - coordinate.Xmin()).Select(c => new Coordinate(c, coordinate.C1.Y));

                    foreach (var c in line)
                    {
                        grid[c.X, c.Y].Count += 1;
                    }
                }

                if (coordinate.IsHorizontal())
                {
                    var line = Enumerable.Range(coordinate.Ymin(), 1 + coordinate.Ymax() - coordinate.Ymin()).Select(c => new Coordinate(coordinate.C1.X, c));

                    foreach (var c in line)
                    {
                        grid[c.X, c.Y].Count += 1;
                    }
                }

                if (coordinate.IsDiagonal())
                {
                    var length = coordinate.Xmax() - coordinate.Xmin();
                    var x = coordinate.C1.X;   
                    var y = coordinate.C1.Y;

                    for(var i = 0; i < length; i++)
                    {

                        grid[x, y].Count += 1;
                        x += coordinate.C1.X < coordinate.C2.X ? 1 : -1;
                        y += coordinate.C1.Y < coordinate.C2.Y ? 1 : -1;
                    }

                    grid[x, y].Count += 1;
                }
            }

            var overlaps = 0;

            foreach(var c in grid)
            {
                if (c.Count >= 2) overlaps++;
            }

            //PrintGrid(grid);

            return overlaps.ToString();
        }

        private void PrintGrid(Coordinate[,] grid)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                var gridToString = "";
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    var count = grid[y, x].Count;
                    gridToString += count == 0 ? "." : count.ToString();
                }

                Console.WriteLine(gridToString);
            }
        }

        private string UseDummyInput()
        {
            return @"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2";
        }
    }

    class Coordinates
    {
        private readonly Regex rx = new(@"(\d+),(\d+) -> (\d+),(\d+)", RegexOptions.Compiled);

        public Coordinates(string value)
        {
            var matches = rx.Match(value).Groups.Values.Skip(1);
                
            var coordinates = matches.Select(c => int.Parse(c.Value)).ToArray();

            C1 = new Coordinate(coordinates[0], coordinates[1]);
            C2 = new Coordinate(coordinates[2], coordinates[3]);
        }

        public Coordinate C1 { get; set; }
        public Coordinate C2 { get; set; }

        public bool IsHorizontal() => C1.X == C2.X;
        public bool IsVertical() => C1.Y == C2.Y;
        public bool IsDiagonal() => Math.Abs(C1.X - C2.X) == Math.Abs(C1.Y - C2.Y);
        public bool IsLine() => IsHorizontal() || IsVertical() || IsDiagonal();

        public int Xmax() => Math.Max(C1.X, C2.X);
        public int Xmin() => Math.Min(C1.X, C2.X);
        public int Ymax() => Math.Max(C1.Y, C2.Y);
        public int Ymin() => Math.Min(C1.Y, C2.Y);
    }

    class Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Count { get; set; }
    }
}
