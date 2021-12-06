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
    public class Solver1Tests
    {
        [TestMethod]
        public async Task Solve1() => await SolverTestHelper.Solve(1, 1, "1195");

        [TestMethod]
        public async Task Solve2() => await SolverTestHelper.Solve(1, 2, "1235");
    }
}