using AdventOfCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Tests
{
    public static class SolverTestHelper
    {
        public async static Task Solve(int day, int part, string expected)
        {
            var aDay = (AdventDay)day;
            var puzzle = (Puzzle)part;

            var input = await FileInputHandler.ParseString(aDay, puzzle);

            var solution = await Solvers.Solve(aDay, puzzle, input);

            Assert.AreEqual(expected, solution);
        }
    }
}
