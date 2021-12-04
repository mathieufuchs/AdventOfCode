namespace AdventOfCode
{
    public class Solver4 : ISolver
    {
        public Solver4()
        {
        }

        public async Task<string> Solve1(string input)
        {
            var rows = input.GetRows();

            var drawSeq = rows.First().Split(",").Select(c => int.Parse(c)).ToArray();

            var bingoGrids = CreateBingoGrid(rows);

            foreach (var drawn in drawSeq)
            {
                foreach (var bingoGrid in bingoGrids)
                {
                    foreach (var bingoRow in bingoGrid.Rows)
                    {
                        foreach (var bingoCell in bingoRow.Cells)
                        {
                            if (bingoCell.Number == drawn)
                            {
                                bingoCell.Bingo = true;

                                if (bingoGrid.IsBingo())
                                {
                                    var winnerNum = drawn;
                                    var winner = bingoGrid;
                                    var unmarked = winner.SumUnMarked();
                                    var result = winnerNum * unmarked;
                                    return result.ToString();
                                }
                            }
                        }
                    }
                }
            }

            return "No winner :(";
        }

        private List<BingoGrid> CreateBingoGrid(IEnumerable<string> rows)
        {
            var bingoGrids = new List<BingoGrid>();

            for (var i = 2; i < rows.Count(); i += 6)
            {
                var puzzleRows = rows
                    .Skip(i)
                    .Take(5)
                    .Select(r => r.ReplaceLineEndings("").Trim().Split(" ").Where(c => !string.IsNullOrEmpty(c)));

                var bingoRows = puzzleRows
                    .Select(r => new BingoRow(r.Select(c => GetCell(c)).ToList()))
                    .ToList();

                bingoGrids.Add(new BingoGrid(bingoRows));
            }

            return bingoGrids;
        }

        public async Task<string> Solve2(string input)
        {
            var rows = input.GetRows();

            var drawSeq = rows.First().Split(",").Select(c => int.Parse(c)).ToArray();

            var bingoGrids = CreateBingoGrid(rows);

            foreach (var drawn in drawSeq)
            {
                foreach (var bingoGrid in bingoGrids)
                {
                    foreach (var bingoRow in bingoGrid.Rows)
                    {
                        foreach (var bingoCell in bingoRow.Cells)
                        {
                            if (bingoCell.Number == drawn)
                            {
                                bingoCell.Bingo = true;

                                if (bingoGrid.IsBingo())
                                {
                                    bingoGrid.Bingo = true;
                                }

                                if (bingoGrids.All(bg => bg.Bingo))
                                {
                                    var winnerNum = drawn;
                                    var winner = bingoGrid;
                                    var unmarked = winner.SumUnMarked();
                                    var result = winnerNum * unmarked;
                                    return result.ToString();
                                }
                            }
                        }
                    }
                }
            }
            return "No winner :(";
        }


        private BingoNumber GetCell(string c)
        {
            return int.TryParse(c.Trim(), out var num) ? new BingoNumber(num) : null;
        }
    }

class BingoGrid
{
    public List<BingoRow> Rows { get; set; }
    public bool Bingo { get; set; }

    public BingoGrid(List<BingoRow> rows)
    {
        Rows = rows;
    }

    public bool IsBingo()
    {
        return IsRowBingo() || IsColumnBingo() || IsDiagonalBingo();
    }

    public bool IsRowBingo()
    {
        return Rows.Any(r => r.IsBingo());
    }

    public bool IsColumnBingo()
    {
        for (var i = 0; i < 5; i++)
        {
            if (Rows.All(r => r.Cells[i].Bingo)) return true;
        }

        return false;
    }

    public bool IsDiagonalBingo()
    {
        var LRDiag = Rows[0].Cells[0].Bingo && Rows[1].Cells[1].Bingo && Rows[2].Cells[2].Bingo && Rows[3].Cells[3].Bingo && Rows[4].Cells[4].Bingo;
        var RLDiag = Rows[0].Cells[4].Bingo && Rows[1].Cells[3].Bingo && Rows[2].Cells[2].Bingo && Rows[3].Cells[1].Bingo && Rows[4].Cells[0].Bingo;

        return LRDiag || RLDiag;
    }

    public int SumUnMarked()
    {
        return Rows.Sum(r => r.SumUnMarked());
    }
}

class BingoRow
{
    public List<BingoNumber> Cells { get; set; }

    public BingoRow(List<BingoNumber> cells)
    {
        Cells = cells;
    }

    public bool IsBingo()
    {
        return Cells.All(c => c.Bingo);
    }

    public int SumUnMarked()
    {
        return Cells.Where(c => !c.Bingo).Sum(c => c.Number);
    }
}

class BingoNumber
{
    public BingoNumber(int num)
    {
        Number = num;
    }

    public int Number { get; set; }
    public bool Bingo { get; set; }
}
}
