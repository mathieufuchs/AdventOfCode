using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace AdventOfCode.Tests
{
    [TestClass()]
    public class Solver3Tests
    {
        [TestMethod]
        public async Task Solve1() => await SolverTestHelper.Solve(3, 1, "3847100");

        [TestMethod]
        public async Task Solve2() => await SolverTestHelper.Solve(3, 2, "4105235");
    }
}