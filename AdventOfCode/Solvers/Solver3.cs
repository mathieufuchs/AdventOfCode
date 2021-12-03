using System.Collections;

namespace AdventOfCode
{
    public class Solver3 : ISolver
    {
        public Solver3()
        {
        }
        public async Task<string> Solve1(string input)
        {
            var rows = input.GetRows();

            var chars = rows.Select(r => r.ReplaceLineEndings("").ToArray());

            var rowLength = chars.First().Length;

            var gammaRateBits = "";
            var epsilonRateBits = "";

            for(var i = 0; i < rowLength; i++)
            {
                var ones = chars.Count(c => c.ElementAt(i) == '1');
                var zeros = chars.Count(c => c.ElementAt(i) == '0');
                var mostOnes = ones > zeros;
                gammaRateBits += mostOnes ? "1": "0";
                epsilonRateBits += mostOnes ? "0" : "1";
            }

            var gammaRate = Convert.ToInt32(gammaRateBits, 2);
            var epsilonRate = Convert.ToInt32(epsilonRateBits, 2);

            var result = epsilonRate * gammaRate;
            return result.ToString();
        }

        public async Task<string> Solve2(string input)
        {
            var rows = input.GetRows();

            var chars = rows.Select(r => r.ReplaceLineEndings("").ToArray());

            var rowLength = chars.First().Length;

            var seq = "";
            var oxygen = "";
            var scrubber = "";

            var filtered = chars;

            for (var i = 0; i < rowLength; i++)
            {
                var ones = filtered.Count(c => c.ElementAt(i) == '1');
                var zeros = filtered.Count(c => c.ElementAt(i) == '0');
                var mostOnes = ones >= zeros;
                seq += mostOnes ? "1": "0";

                filtered = chars.Where(c => new string(c).StartsWith(seq));
                if (filtered.Count() == 1)
                {
                    oxygen = new string(filtered.First());
                    break;
                }
            }

            filtered = chars;
            seq = "";

            for (var i = 0; i < rowLength; i++)
            {

                var ones = filtered.Count(c => c.ElementAt(i) == '1');
                var zeros = filtered.Count(c => c.ElementAt(i) == '0');
                var mostOnes = ones >= zeros;
                seq += mostOnes ? "0" : "1";

                filtered = chars.Where(c => new string(c).StartsWith(seq));
                if (filtered.Count() == 1)
                {
                    scrubber = new string(filtered.First());
                    break;
                }
            }

            var gammaRate = Convert.ToInt32(oxygen, 2);
            var epsilonRate = Convert.ToInt32(scrubber, 2);

            var result = epsilonRate * gammaRate;
            return result.ToString();
        }
    }
}
