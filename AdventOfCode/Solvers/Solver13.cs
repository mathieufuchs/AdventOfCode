namespace AdventOfCode
{
    public class Solver13 : ISolver
    {
        public Solver13()
        {
        }
        public async Task<string> Solve1(string input)
        {
            var (dots, instructions) = Init(input);

            var (x, index) = instructions.First();

            var maxIndex = index * 2 + 1;

            var xMax = x ? maxIndex : dots.Max(d => d.x) + 1;
            var yMax = x ? dots.Max(d => d.y) + 1 : maxIndex;

            bool[,] grid = new bool[xMax, yMax];

            foreach(var dot in dots)
            {
                grid[dot.x, dot.y] = true;
            }

            

            bool[,] folded;

            if (x)
            {
                folded = new bool[index, yMax];
                for (var i = 0; i < index; i++)
                {
                    for(var j = 0; j < yMax; j++)
                    {
                        folded[i, j] = grid[i, j] || grid[xMax - 1 - i, j];
                    }
                }
            }
            else
            {
                folded = new bool[xMax, index];
                for (var i = 0; i < xMax; i++)
                {
                    for (var j = 0; j < index; j++)
                    {
                        folded[i, j] = grid[i, j] || grid[i, yMax - 1 - j];
                    }
                }
            }

            var markedCount = 0;

            foreach(var f in folded)
            {
                markedCount += f ? 1 : 0;
            }

            return markedCount.ToString();
        }

        public ((int x, int y)[] dots, (bool x, int index)[] instructions) Init(string input)
        {
            /*input = @"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5";*/

            var rows = input.GetRows();


            var dots = rows.TakeWhile(x => x != "").Select(s => s.Split(",")).Select(s => (int.Parse(s[0]), int.Parse(s[1]))).ToArray();

            var instructions = rows.Skip(dots.Length + 1).Select(i => i.Split("=")).Select(s => (s[0].Last() == 'x', int.Parse(s[1]))).ToArray();

            return (dots, instructions);

        }

        public async Task<string> Solve2(string input)
        {
            var (dots, instructions) = Init(input);

            var (x, index) = instructions.First();

            var maxIndex = index * 2 + 1;

            var xMax = x ? maxIndex : dots.Max(d => d.x) + 1;
            var yMax = x ? dots.Max(d => d.y) + 1 : maxIndex;

            bool[,] grid = new bool[xMax, yMax];

            foreach (var dot in dots)
            {
                grid[dot.x, dot.y] = true;
            }

            bool[,] folded = default;

            if (x)
            {
                folded = new bool[index, yMax];
                for (var i = 0; i < index; i++)
                {
                    for (var j = 0; j < yMax; j++)
                    {
                        folded[i, j] = grid[i, j] || grid[xMax - 1 - i, j];
                    }
                }
            }
            else
            {
                folded = new bool[xMax, index];
                for (var i = 0; i < xMax; i++)
                {
                    for (var j = 0; j < index; j++)
                    {
                        folded[i, j] = grid[i, j] || grid[i, yMax - 1 - j];
                    }
                }
            }

            foreach (var instruction in instructions.Skip(1))
            {
                (x, index) = instruction;

                maxIndex = index * 2 + 1;

                xMax = x ? maxIndex : folded.GetLength(0);
                yMax = x ? folded.GetLength(1) : maxIndex;

                grid = folded;

                if (x)
                {
                    folded = new bool[index, yMax];
                    for (var i = 0; i < index; i++)
                    {
                        for (var j = 0; j < yMax; j++)
                        {
                            folded[i, j] = grid[i, j] || grid[xMax - 1 - i, j];
                        }
                    }
                }
                else
                {
                    folded = new bool[xMax, index];
                    for (var i = 0; i < xMax; i++)
                    {
                        for (var j = 0; j < index; j++)
                        {
                            folded[i, j] = grid[i, j] || grid[i, yMax - 1 - j];
                        }
                    }
                }

            }

            var output = "";

            for(var j = 0; j < folded.GetLength(1); j++)
            {
                for (var i = 0; i < folded.GetLength(0); i++)
                {
                    output += folded[i, j] ? "#" : " ";
                }
                
                output += Environment.NewLine;
            }

            return output;
        }
    }
}
