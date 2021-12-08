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

            foreach(var item in row.Outputs)
            {
                if (item == row.C0)
                {
                    integerTranslation += "0";
                    continue;
                }

                if (item == row.C1)
                {
                    integerTranslation += "1";
                    continue;
                }

                if (item == row.C2)
                {
                    integerTranslation += "2";
                    continue;
                }

                if (item == row.C3)
                {
                    integerTranslation += "3";
                    continue;
                }

                if (item == row.C4)
                {
                    integerTranslation += "4";
                    continue;
                }

                if (item == row.C5)
                {
                    integerTranslation += "5";
                    continue;
                }

                if (item == row.C6)
                {
                    integerTranslation += "6";
                    continue;
                }

                if (item == row.C7)
                {
                    integerTranslation += "7";
                    continue;
                }

                if (item == row.C8)
                {
                    integerTranslation += "8";
                    continue;
                }

                if (item == row.C9)
                {
                    integerTranslation += "9";
                    continue;
                }
            }

            return int.Parse(integerTranslation);
        }

    }


    public class Day8Row
    {
        public string C0;
        public string C1;
        public string C2;
        public string C3;
        public string C4;
        public string C5;
        public string C6;
        public string C7;
        public string C8;
        public string C9;

        public Day8Row()
        {

        }

        public Day8Row(IEnumerable<string> signals, IEnumerable<string> outputs)
        {
            Signals = signals.Select(s => Sort(s));
            Outputs = outputs.Select(s => Sort(s));

            InitWires();
        }

        private string Sort(string input)
        {
            var chars = input.ToCharArray();
            Array.Sort(chars);
            return new string(chars);
        }

        private void InitWires()
        {
            C1 = Signals.Single(s => s.Length == 2);
            C7 = Signals.Single(s => s.Length == 3);
            C4 = Signals.Single(s => s.Length == 4);
            C8 = Signals.Single(s => s.Length == 7);


            C3 = Signals.Single(s => s.Length == 5 && C1.All(c => s.Contains(c)));
            C9 = Signals.Single(s => s.Length == 6 && C4.All(c => s.Contains(c)));

            var bottomLeft = C8.Except(C9).Single();

            C2 = Signals.Single(s => s.Length == 5 && s.Contains(bottomLeft));
            C5 = Signals.Single(s => s.Length == 5 && s != C2 && s != C3);

            C6 = Signals.Single(s => s.Length == 6 && s != C9 && C5.All(c => s.Contains(c)));
            C0 = Signals.Single(s => s.Length == 6 && s != C9 && s != C6);
        }

        public IEnumerable<string> Signals { get; set; }
        public IEnumerable<string> Outputs { get; set; }
    }
}
