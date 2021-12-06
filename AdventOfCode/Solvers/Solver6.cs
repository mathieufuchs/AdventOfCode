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
            var rows = input.Split(",").Select(r => int.Parse(r)).ToList();

            for (var i = 0; i < 256; i++)
            {
                var length = rows.Count;
                for (var j = 0; j < length; j++)
                {
                    if (rows[j] == 0)
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

        private string UseDummyInput()
        {
            return "3,4,3,1,2";
        }
    }
}
