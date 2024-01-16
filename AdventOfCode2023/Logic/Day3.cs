using AdventOfCode2023.Constants;

namespace AdventOfCode2023.Logic;

public class Day3
{
    private const string SchematicFullFilename = "Day3.txt";
    private const string SchematicSample1Filename = "Day3Part1Sample.txt";
    private const string SchematicSample2Filename = "Day3Part2Sample.txt";
    private readonly FileReader _reader = new();
    
    public IEnumerable<string> GetPuzzleInput(InputFiles input)
    {
        return input switch
        {
            (InputFiles.Sample1) => _reader.ReadLines(SchematicSample1Filename),
            (InputFiles.Sample2) => _reader.ReadLines(SchematicSample2Filename),
            (InputFiles.FullInput) => _reader.ReadLines(SchematicFullFilename),
            _ => throw new ArgumentOutOfRangeException(nameof(input), input, null)
        };
    }

    public IEnumerable<IEnumerable<char>> TranslateSchematicToGrid(IEnumerable<string> schematicInput)
    {
        return schematicInput.Select(line =>
        {
            return line.Select(c => c);
        });
    }

    public IEnumerable<char> GetAllAdjacentCells(IEnumerable<IEnumerable<char>> grid, int row, int lastColumn, int partNumberLength)
    {
        List<char> adjacentCells = [];
        
    }

    public IEnumerable<int> GetAllPartNumbers(IEnumerable<IEnumerable<char>> grid)
    {
        var buffer = string.Empty;
        List<int> partNumbers = [];
        foreach (var (line, lineIndex) in grid.Select((value, i) => ( value, i )))
        {
            foreach (var (c, index) in line.Select((value, i) => ( value, i )))
            {
                if (char.IsDigit(c))
                {
                    buffer += c;
                }
                else
                {
                    if (buffer != string.Empty)
                    {
                        var length = buffer.Length;
                        var adjacentCells = GetAllAdjacentCells(grid, lineIndex, index, length);
                    }
                }
            }
        }
    }
}