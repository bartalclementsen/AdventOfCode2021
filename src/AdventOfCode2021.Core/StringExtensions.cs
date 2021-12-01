namespace System
{
    public static class StringExtensions
    {
        public static List<int> SplitAndConvertToIntByNewLine(this string input)
        {
            return input.Split(Environment.NewLine).Select(i => int.Parse(i)).ToList();
        }
    }
}