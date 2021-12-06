using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace AdventOfCode.Tests
{
    [TestClass()]
    public class Solver4Tests
    {
        [TestMethod]
        public async Task Solve1() => await SolverTestHelper.Solve(4, 1, "67716");

        [TestMethod]
        public async Task Solve2() => await SolverTestHelper.Solve(4, 2, "1830");
    }
}