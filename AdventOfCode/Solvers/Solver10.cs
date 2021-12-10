namespace AdventOfCode
{
    public class Solver10 : ISolver
    {
        public Solver10()
        {
        }
        public async Task<string> Solve1(string input)
        {
            /*input = @"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]";*/

            var parsed = ParseInput(input);

            var openChars = new Stack<char>();

            var openClose = new Dictionary<char, char>() {
                { ')', '(' },
                { ']', '[' },
                { '>', '<' },
                { '}', '{' },
            };

            var illegalScore = new Dictionary<char, long>() {
                { ')', 3 },
                { ']', 57 },
                { '}', 1197 },
                { '>', 25137 },
            };

            var illegalChars = new List<char>();

            foreach(var line in parsed)
            {
                foreach(var c in line)
                {
                    if(c is '(' or '[' or '{' or '<')
                    {
                        openChars.Push(c);
                        continue;
                    }

                    var openChar = openChars.Pop();

                    if (openChar != openClose[c])
                    {
                        illegalChars.Add(c);
                    }
                }
            }

            var score = illegalChars.Select(c => illegalScore[c]).Sum();

            return score.ToString();
        }

        private char[][] ParseInput(string input)
        {
            return input.Split(Environment.NewLine).Select(c => c.ToCharArray()).ToArray();
        }

        public async Task<string> Solve2(string input)
        {
            /*input = @"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]";*/

            var parsed = ParseInput(input);

            var openChars = new Stack<char>();

            var openClose = new Dictionary<char, char>() {
                { ')', '(' },
                { ']', '[' },
                { '>', '<' },
                { '}', '{' },
            };

            var matchingClose = new Dictionary<char, char>() {
                { '(', ')' },
                { '[', ']' },
                { '<', '>' },
                { '{', '}' },
            };

            var closeScore = new Dictionary<char, long>() {
                { ')', 1 },
                { ']', 2 },
                { '}', 3 },
                { '>', 4 },
            };

            var closeChars = new List<List<char>>();

            foreach (var line in parsed)
            {
                openChars.Clear();
                var isIllegal = false;
                foreach (var c in line)
                {
                    if (isIllegal) continue;

                    if (c is '(' or '[' or '{' or '<')
                    {
                        openChars.Push(c);
                        continue;
                    }

                    var openChar = openChars.Pop();

                    if (openChar != openClose[c])
                    {
                        isIllegal = true;
                    }
                }

                if (isIllegal) continue;

                // find missing closing chars

                

                var toComplete = new List<char>();

                foreach (var c in openChars)
                {
                    toComplete.Add(matchingClose[c]);
                }

                closeChars.Add(toComplete);
            }

            var scores = closeChars.Select(line => line.Select(c => closeScore[c]).Aggregate(0m, (acc, val) => 5 * acc + val)).OrderBy(n => n);

            return scores.ElementAt(scores.Count() / 2).ToString();
        }
    }
}
