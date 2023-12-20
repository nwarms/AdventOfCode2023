using AdventOfCode2023.Constants;

namespace AdventOfCode2023.Logic
{
    public class Day1
    {
        private const string CalibrationDocumentFilename = "Day1.txt";
        private const string Sample1CalibrationDocumentFilename = "Day1Part1Sample.txt";
        private const string Sample2CalibrationDocumentFilename = "Day1Part2Sample.txt";
        private readonly FileReader _reader = new();
        private List<string> _calibrationInput = [];
        private readonly Dictionary<string, int> _mapWordToNumberValue = new() {
            { "one", 1},
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 }
        };
        
        private void GetPuzzleInput(InputFiles input)
        {
            _calibrationInput = input switch
            {
                InputFiles.Sample1 => _reader.ReadLines(Sample1CalibrationDocumentFilename),
                InputFiles.Sample2 => _reader.ReadLines(Sample2CalibrationDocumentFilename),
                InputFiles.FullInput => _reader.ReadLines(CalibrationDocumentFilename),
                _ => _calibrationInput
            };
        }

        private List<int> GetAllDigitsFromString(string input)
        {
            return input.Where(char.IsDigit).Select(c => int.Parse(c.ToString())).ToList();
        }

        private IEnumerable<int> GetCalibrationValueForEachLine(IEnumerable<int> firsts, IEnumerable<int> lasts)
        {
            return firsts.Select((c, index) => int.Parse($"{c}{lasts.ElementAt(index)}")).ToList();
        }

        public int FindCalibrationTotalByDigitsOnly(InputFiles inputFile = InputFiles.FullInput)
        {
            GetPuzzleInput(inputFile);
            var allDigitsInEachLine = _calibrationInput.Select(GetAllDigitsFromString).ToList();
            var firstDigitOfEachLine = allDigitsInEachLine.Select(m => m.First()).ToList();
            var lastDigitOfEachLine = allDigitsInEachLine.Select(m => m.Last()).ToList();
            var numberFromEachLine = GetCalibrationValueForEachLine(firstDigitOfEachLine, lastDigitOfEachLine);
            return numberFromEachLine.Sum();
        }

        public int FindCalibrationTotalByDigitsAndWords(InputFiles inputFile = InputFiles.FullInput)
        {
            GetPuzzleInput(inputFile);
            var allDigitsInEachLine = _calibrationInput.Select(line =>
            {
                var buffer = "";
                return line.Select(character =>
                {
                    if (char.IsDigit(character))
                    {
                        buffer = "";
                        return int.Parse(character.ToString());
                    }
                    buffer += character;
                    var matchBufferToDigitValue = _mapWordToNumberValue.FirstOrDefault(n => buffer.Contains(n.Key));
                    if (matchBufferToDigitValue.Equals(default(KeyValuePair<string, int>))) return 0;
                    buffer = buffer.Last().ToString();
                    return matchBufferToDigitValue.Value;

                }).Where(value => value is not 0).ToList();
            }).ToList();
            var firstDigitInEachLine = allDigitsInEachLine.Select(f => f.First()).ToList();
            var lastDigitInEachLine = allDigitsInEachLine.Select(f => f.Last()).ToList();
            return GetCalibrationValueForEachLine(firstDigitInEachLine, lastDigitInEachLine).Sum();
        }
    }
}
