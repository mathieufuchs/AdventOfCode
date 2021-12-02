using AdventOfCode;

Console.WriteLine("Welcome to the advent of code solver :)");
Console.WriteLine("Please enter the day and the puzzle: (day-puzzle)");

var solved = false;
var tryAgain = true;

while (!solved || tryAgain)
{
    try
    {
        var commands = Console.ReadLine()?.Split("-");
        var adventDay = Enum.Parse<AdventDay>(commands[0]);
        var puzzleNr = Enum.Parse<Puzzle>(commands[1]);

        var input = await FileInputHandler.ParseString(adventDay, puzzleNr);

        var solution = await Solvers.Solve(adventDay, puzzleNr, input);

        Console.WriteLine(solution);

        solved = true;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Could not solve: {ex.Message}");
    }

    if (solved)
    {
        Console.WriteLine("Try againg? (day-puzzle)");
    }
}




