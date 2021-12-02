namespace AdventOfCode
{
    public interface ISolver
    {
        Task<string> Solve1(string input);
        Task<string> Solve2(string input);
    }
}
