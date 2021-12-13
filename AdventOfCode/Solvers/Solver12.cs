namespace AdventOfCode
{
    public class Solver12 : ISolver
    {
        public Solver12()
        {
        }
        public async Task<string> Solve1(string input)
        {
            var links = Init(input);

            var starts = links.Where(s => s.from == "start").ToList();

            var paths = starts.Select(s => new List<string> { s.to }.AsEnumerable());


            while (!paths.All(p => p.Last() == "end"))
            {
                var newPaths = new List<IEnumerable<string>>();

                foreach (var path in paths)
                {
                    starts = links.Where(s => s.from == path.Last()).ToList();

                    var newPath = starts
                        .Where(s => s.to.All(char.IsUpper) || !path.Contains(s.to))
                        .Select(s => path.Concat(new List<string> { s.to }));

                    newPaths.AddRange(newPath);
                }

                paths = newPaths.Concat(paths.Where(p => p.Last() == "end"));
            }

            return paths.Count().ToString();
        }

        public List<(string from, string to)> Init(string input)
        {
            /*input = @"start-A
start-b
A-c
A-b
b-d
A-end
b-end";*/

            /*input = @"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc";*/

            /*input = @"fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW";*/

            var links = new List<(string from, string to)>();

            var rows = input.GetRows().Select(s => s.Split("-")).Select(s => (s[0], s[1])).ToList();

            foreach (var (from, to) in rows)
            {
                if (to == "start" || from == "end")
                {
                    links.Add((to, from));
                }
                else if (from == "start" || to == "end")
                {
                    links.Add((from, to));
                }
                else
                {
                    links.Add((from, to));
                    links.Add((to, from));
                }
            }

            return links;
        }

        public async Task<string> Solve2(string input)
        {
            var links = Init(input);

            var paths = links.Where(s => s.from == "start").Select(s => new List<string> { s.to }.AsEnumerable());

            while (!paths.All(p => p.Last() == "end"))
            {
                var newPaths = new List<IEnumerable<string>>();

                foreach (var path in paths)
                {
                    var tos = links.Where(s => s.from == path.Last()).Select(s => s.to).ToList();

                    var newPath = tos
                        .Where(to => IsValid(to, path))
                        .Select(to => path.Concat(new List<string> { to }));

                    newPaths.AddRange(newPath);
                }

                paths = newPaths.Concat(paths.Where(p => p.Last() == "end"));
            }

            return paths.Count().ToString();
        }

        private bool IsValid(string to, IEnumerable<string> path)
        {
            var isBig = to.All(char.IsUpper);
            var isSmallTwice = path.Where(p => p.All(char.IsLower)).GroupBy(p => p).Count(g => g.Count() == 2) == 0;

            var isSmall = !path.Contains(to);
            return isBig || isSmallTwice || isSmall;
        }

    }
}
