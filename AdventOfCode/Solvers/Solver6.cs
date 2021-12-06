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

            var rows = input.Split(",").Select(r => int.Parse(r)).ToList();

            for(var i = 0; i < 80; i++)
            {
                var length = rows.Count;
                for(var j = 0; j < length; j++)
                {
                    if(rows[j] == 0)
                    {
                        rows[j] = 6;
                        rows.Add(8);
                    }
                    else
                    {
                        rows[j]--;
                    }
                }
            }

            return rows.Count.ToString();
        }
        public async Task<string> Solve2(string input)
        {
            //input = UseDummyInput();

            var rows = input.Split(",").Select(r => int.Parse(r)).ToList();

            var fishes = new Dictionary<int, long>() {
                {0, 0},
                {1, rows.Count(r => r == 1)},
                {2, rows.Count(r => r == 2)},
                {3, rows.Count(r => r == 3)},
                {4, rows.Count(r => r == 4)},
                {5, rows.Count(r => r == 5)},
                {6, rows.Count(r => r == 6)},
                {7, 0},
                {8, 0},
            };

            for (var i = 0; i < 256; i++)
            {
                var zeros = fishes[0];
                fishes[0] = fishes[1];
                fishes[1] = fishes[2]; 
                fishes[2] = fishes[3]; 
                fishes[3] = fishes[4]; 
                fishes[4] = fishes[5]; 
                fishes[5] = fishes[6]; 
                fishes[6] = zeros + fishes[7]; 
                fishes[7] = fishes[8];
                fishes[8] = zeros;
            }

            return fishes.Sum(f => f.Value).ToString();
        }

        private string UseDummyInput()
        {
            return "3,4,3,1,2";
        }
    }
}
