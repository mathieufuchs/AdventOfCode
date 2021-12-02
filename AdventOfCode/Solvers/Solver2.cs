namespace AdventOfCode
{
    public class Solver2 : ISolver
    {
        public Solver2()
        {
        }

        public async Task<string> Solve1(string input)
        {
            var horizontal = 0;
            var vertical = 0;

            var rows = input.GetRows();

            var commands = rows
                .GetCommandsSeparatedBySpaces()
                .Select(x => new { direction = x[0], value = int.Parse(x[1]) });

            foreach (var command in commands)
            {
                switch (command.direction)
                {
                    case "forward":
                        horizontal += command.value;
                        break;
                    case "down":
                        vertical += command.value;
                        break;
                    case "up":
                        vertical -= command.value;
                        break;
                    default:
                        throw new ArgumentException($"Direction {command.direction} not mapped");
                }
            }

            var result = (horizontal * vertical);

            return result.ToString();
        }

        public async Task<string> Solve2(string input)
        {
            var aim = 0;
            var horizontal = 0;
            var vertical = 0;

            var rows = input.GetRows();

            var commands = rows
                .GetCommandsSeparatedBySpaces()
                .Select(x => new { direction = x[0], value = int.Parse(x[1]) });

            foreach (var command in commands)
            {
                switch (command.direction)
                {
                    case "forward":
                        horizontal += command.value;
                        vertical += (aim * command.value);
                        break;
                    case "down":
                        aim += command.value;
                        break;
                    case "up":
                        aim -= command.value;
                        break;
                    default:
                        throw new ArgumentException($"Direction {command.direction} not mapped");
                }
            }

            var result = (horizontal * vertical);

            return result.ToString();
        }
    }
}
