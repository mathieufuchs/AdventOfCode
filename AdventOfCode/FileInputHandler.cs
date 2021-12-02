using System.Reflection;

namespace AdventOfCode
{
    public class FileInputHandler
    {
        public static async Task<string> ParseString(AdventDay day, Puzzle puzzle)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"AdventOfCode.Puzzles.Input_{(int)day}_{(int)puzzle}.txt";

            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            using StreamReader reader = new(stream);
            return await reader.ReadToEndAsync();
        }
    }

    public enum AdventDay 
    {
        _1 = 1,
        _2,
        _3,
        _4,
        _5,
        _6,
        _7,
        _8,
        _9,
        _10,
        _11,
        _12,
        _13,
        _14,
        _15,
        _16,
        _17,
        _18,
        _19,
        _20,
        _21,
        _22,
        _23,
        _24,
        _25,
    }

    public enum Puzzle
    {
        _1 = 1,
        _2
    }
}
