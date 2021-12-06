using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Tests
{
    [TestClass()]
    public class Solver2Tests
    {
        [TestMethod]
        public async Task Solve1() => await SolverTestHelper.Solve(2, 1, "1962940");

        [TestMethod]
        public async Task Solve2() => await SolverTestHelper.Solve(2, 2, "1813664422");
    }
}