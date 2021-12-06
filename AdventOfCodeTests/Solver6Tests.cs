using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Solver6Tests
    {
        private readonly int[] fishes = new int[] { 3, 4, 3, 1, 2 };

        [DataTestMethod()]
        [DataRow(18, 26)]
        [DataRow(80, 5934)]
        public void Solve(int iterations, long expected)
        {
            var result = Solver6.Solve(fishes, iterations);
            Assert.AreEqual(expected, result);
        }
    }
}