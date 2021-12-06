using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace AdventOfCode.Tests
{
    [TestClass()]
    public class Solver5Tests
    {
        [TestMethod, Ignore]
        public async Task Solve1() => await SolverTestHelper.Solve(5, 1, "5632");

        [TestMethod, Ignore]
        public async Task Solve2() => await SolverTestHelper.Solve(5, 2, "22213");
    }
}