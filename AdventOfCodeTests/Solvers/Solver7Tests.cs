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
    public class Solver7Tests
    {
        [TestMethod()]
        public void Solve()
        {
            var input = "16,1,2,0,4,2,7,1,2,14";
            var expected = 37;

            var result = Solver7.Internal_Solve_1(input);

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void Solve_2()
        {
            var input = "16,1,2,0,4,2,7,1,2,14";
            var expected = 168;

            var result = Solver7.Internal_Solve_2(input);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public async Task Solve1() => await SolverTestHelper.Solve(7, 1, "357353");

        [TestMethod]
        public async Task Solve2() => await SolverTestHelper.Solve(7, 2, "104822130");
    }
}