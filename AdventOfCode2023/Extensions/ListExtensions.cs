namespace AdventOfCode2023.Extensions
{
    public static class ListExtensions
    {
        public static string ListToString<T>(this IEnumerable<T> list, bool? spacing = false)
        {
            var spacingCharacter = spacing is true ? " " : "";
            return list.Aggregate("", (acc, x) => $"{acc}{spacingCharacter}{x}");
        }
        
        public static int TotalList(IEnumerable<int> numbers)
        {
            return numbers.Sum();
        }
    }
}
