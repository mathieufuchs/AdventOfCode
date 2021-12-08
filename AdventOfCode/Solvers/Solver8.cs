namespace AdventOfCode
{
    public class Solver8 : ISolver
    {
        public Solver8()
        {
        }
        public async Task<string> Solve1(string input)
        {
            /*input = @"be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe
edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc
fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg
fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb
aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea
fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb
dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe
bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef
egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb
gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce";*/

            var rows = ParseInput(input);
            var counter = 0;

            foreach (var row in rows)
            {
                counter += row.Outputs.Count(o => o.Length is 2 or 3 or 4 or 7);
            }

            return counter.ToString();
        }

        private static IEnumerable<Day8Row> ParseInput(string input)
        {
            var result = new List<Day8Row>();

            var signalsAndOutput = input.Split(Environment.NewLine).Select(s => s.Split(" | ")).ToArray();

            foreach(var item in signalsAndOutput)
            {
                yield return new Day8Row(item[0].Split(" "), item[1].Split(" "));
            }
        }

        public async Task<string> Solve2(string input)
        {
            /*input = @"be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe
edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc
fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg
fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb
aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea
fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb
dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe
bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef
egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb
gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce";*/

            var rows = ParseInput(input);

            var counter = rows.Sum(row => CalculateOutputValue(row));

            return counter.ToString();
        }

        public int CalculateOutputValue(Day8Row row)
        {
            var integerTranslation = "";

            foreach(var output in row.Outputs)
            {
                integerTranslation += row.Numbers[output];
            }

            return int.Parse(integerTranslation);
        }

    }


    public class Day8Row
    {
        public Dictionary<string, string> Numbers = new();

        public Day8Row()
        {

        }

        public Day8Row(IEnumerable<string> signals, IEnumerable<string> outputs)
        {
            Signals = signals.Select(s => Sort(s));
            Outputs = outputs.Select(s => Sort(s));

            InitNumbers();
        }

        private static string Sort(string input)
        {
            var chars = input.ToCharArray();
            Array.Sort(chars);
            return new string(chars);
        }

        private void InitNumbers()
        {
            var c1 = Signals.Single(s => s.Length == 2);
            var c7 = Signals.Single(s => s.Length == 3);
            var c4 = Signals.Single(s => s.Length == 4);
            var c8 = Signals.Single(s => s.Length == 7);

            var c3 = Signals.Single(s => s.Length == 5 && c1.All(c => s.Contains(c)));
            var c9 = Signals.Single(s => s.Length == 6 && c4.All(c => s.Contains(c)));

            var bottomLeft = c8.Except(c9).Single();
            var c2 = Signals.Single(s => s.Length == 5 && s.Contains(bottomLeft));

            var c5 = Signals.Single(s => s.Length == 5 && s != c2 && s != c3);
            var c6 = Signals.Single(s => s.Length == 6 && s != c9 && c5.All(c => s.Contains(c)));
            var c0 = Signals.Single(s => s.Length == 6 && s != c9 && s != c6);

            Numbers.Add(c0, "0");
            Numbers.Add(c1, "1");
            Numbers.Add(c2, "2");
            Numbers.Add(c3, "3");
            Numbers.Add(c4, "4");
            Numbers.Add(c5, "5");
            Numbers.Add(c6, "6");
            Numbers.Add(c7, "7");
            Numbers.Add(c8, "8");
            Numbers.Add(c9, "9");
        }

        public IEnumerable<string> Signals { get; set; }
        public IEnumerable<string> Outputs { get; set; }
    }
}
