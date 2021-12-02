namespace System
{
    public static class StringExtensions
    {
        public static IEnumerable<string> SplitByNewLine(this string input)
        {
            return input.Split(Environment.NewLine);
        }

        public static List<T> SplitAndConvertByNewLine<T>(this string input, Func<string, T> map)
        {
            return SplitByNewLine(input).Select(i => map(i)).ToList();
        }

        public static List<int> SplitAndConvertToIntByNewLine(this string input)
        {
            return SplitAndConvertByNewLine(input, (s) => int.Parse(s));
        }
    }
}