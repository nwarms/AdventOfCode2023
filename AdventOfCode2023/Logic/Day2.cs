using AdventOfCode2023.Constants;
using AdventOfCode2023.Extensions;

namespace AdventOfCode2023.Logic
{
    public class Day2
    {
        private const string GamesFullFilename = "Day2.txt";
        private const string GamesSample1Filename = "Day2Part1Sample.txt";
        private readonly FileReader _reader = new();

        private static readonly Dictionary<string, int> ColourLimits = new()
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };
        
        public IEnumerable<string> GetPuzzleInput(InputFiles input)
        {
            return input switch
            {
                (InputFiles.Sample1) => _reader.ReadLines(GamesSample1Filename),
                (InputFiles.Sample2) => _reader.ReadLines(GamesSample1Filename),
                (InputFiles.FullInput) => _reader.ReadLines(GamesFullFilename),
                _ => throw new ArgumentOutOfRangeException(nameof(input), input, null)
            };
        }

        public static int GetGameId(string gameInput)
        {
            return int.Parse(gameInput.Split(":").First().Replace("Game ", ""));
        }

        public static string GetCurrentColour(string handInput)
        {
            return ColourLimits.Keys.First(handInput.Contains);
        }

        public static IEnumerable<Dictionary<string, int>> GetGameCubes(string gameInput)
        {
            return gameInput.Split(":").Last().Split(";").Select(hand =>
            {
                var handful = new Dictionary<string, int>();
                Array.ForEach(hand.Split(","), cubes =>
                {
                    var colour = GetCurrentColour(cubes);
                    var itemToParse = cubes.Replace($"{colour}", "").RemoveWhitespace();
                    var number = int.Parse(itemToParse);
                    handful.Add(colour, number);
                });
                return handful;
            });
        }

        public static Dictionary<string, int> GetHighestColourTotal(IEnumerable<Dictionary<string, int>> gameHandfuls)
        {
            return gameHandfuls.Aggregate((source, accumulator) =>
            {
                Array.ForEach(source.Keys.ToArray(), key =>
                {
                    if (accumulator.ContainsKey(key) is false || accumulator[key] < source[key])
                    {
                        accumulator[key] = source[key];
                    }
                });
                return accumulator;
            });
        }

        public static (int, IEnumerable<Dictionary<string, int>>) GetFullGame(string gameInput)
        {
            return (GetGameId(gameInput), GetGameCubes(gameInput));
        }

        public static (bool, int) IsGameValid(string gameInput)
        {
            var id = GetGameId(gameInput);
            var handfuls = GetGameCubes(gameInput);
            var areAllHandfulsValid = handfuls.Select(handful => handful.All(IsHandfulValid)).All(valid => valid);
            return (areAllHandfulsValid, id);
        }

        public static bool IsHandfulValid(KeyValuePair<string, int> handful)
        {
            return ColourLimits[handful.Key] >= handful.Value;
        }

        public IEnumerable<int> GetTotalOfAllValidGameIds(InputFiles inputFiles = InputFiles.FullInput)
        {
            var gameInputs = GetPuzzleInput(inputFiles);
            return gameInputs.Select(IsGameValid).Where(item => item.Item1).Select(i => i.Item2);
        }

        public IEnumerable<int> GetPowersOfAllGames(InputFiles inputFiles = InputFiles.FullInput)
        {
            var gameInputs = GetPuzzleInput(inputFiles);
            return gameInputs.Select(GetGameCubes).Select(GetHighestColourTotal).Select(item =>
                item.Aggregate<KeyValuePair<string, int>, int>(1, (total, current) =>
                {
                    return total * current.Value;
                }));
        }
    }
}
