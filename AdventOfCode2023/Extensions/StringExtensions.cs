namespace AdventOfCode2023.Extensions;

public static class StringExtensions
{
    public static string RemoveWhitespace(this string s)
    {
        return new string(s.Where(c => !char.IsWhiteSpace(c)).ToArray());
    }
}