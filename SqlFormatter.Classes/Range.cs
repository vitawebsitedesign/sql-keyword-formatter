using System.Text.RegularExpressions;

namespace SqlFormatter.Classes
{
    internal class Range
    {
        public int Start { get; }
        public int End { get; }

        public Range(int start, int end)
        {
            Start = start;
            End = end;
        }

        public static Range Map(Match match)
        {
            return new Range(match.Index, match.Index + match.Length);
        }
    }
}
