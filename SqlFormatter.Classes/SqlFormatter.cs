using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SqlFormatter.Classes
{
    public static class SqlFormatter
    {
        private static readonly string _patternSingleLineComment = @"(--(.|\w)+?(\r|$))";
        private static readonly string _patternMultiLineComment = @"(\/\*(.|\s)+?\*\/)";

        public static SqlFormatterResult Format(string keywordsRegex, string input)
        {
            if (string.IsNullOrWhiteSpace(keywordsRegex))
            {
                throw new ArgumentException(nameof(keywordsRegex));
            }
            if (string.IsNullOrWhiteSpace(input))
            {
                return new SqlFormatterResult(0, input, false, true);
            }

            var pattern = $@"\b({keywordsRegex})\b";
            var commentRanges = GetCommentRanges(input);
            var matches = Regex.Matches(input, pattern, RegexOptions.IgnoreCase).Cast<Match>();
            var matchesToReplace = matches.Where(m => !IsMatchInComment(m.Index, commentRanges));

            var sb = new StringBuilder(input);
            foreach (var match in matchesToReplace)
            {
                sb.Remove(match.Index, match.Length).Insert(match.Index, match.Value.ToUpper());
            }

            var output = sb.ToString();
            var changed = input != output;
            var verified = input.Equals(output, StringComparison.OrdinalIgnoreCase);
            return new SqlFormatterResult(matchesToReplace.Count(), output, changed, verified);
        }

        private static IReadOnlyCollection<Range> GetCommentRanges(string sql)
        {
            var pattern = $@"{_patternSingleLineComment}|{_patternMultiLineComment}";
            var matches = Regex.Matches(sql, pattern, RegexOptions.IgnoreCase).Cast<Match>().ToList();
            return matches
                .Select(Range.Map)
                .ToList()
                .AsReadOnly();
        }

        private static bool IsMatchInComment(int matchIndex, IReadOnlyCollection<Range> ranges)
        {
            foreach (var range in ranges)
            {
                if (matchIndex >= range.Start && matchIndex < range.End)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
