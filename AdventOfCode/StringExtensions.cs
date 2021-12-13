namespace AdventOfCode
{
    public static class StringExtensions
    {
        public static IEnumerable<string> GetRows(this string input)
        {
            return input.Split(Environment.NewLine);
        }

        public static IEnumerable<string[]> GetCommandsSeparatedBySpaces(this IEnumerable<string> rows)
        {
            return rows.Select(x => x.Trim().Split(' '));
        }
    }
}
