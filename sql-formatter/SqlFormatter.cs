using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage;

namespace sql_formatter
{
    internal static class SqlFormatter
    {
        public static async Task<string> GetCasedSql(string sql)
        {
            var lines = sql.Split(new[] { "\r", Environment.NewLine }, StringSplitOptions.None);
            var cased = new List<string>();

            var keywordsFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///sql-keywords.txt"));
            var keywordLines = await FileIO.ReadLinesAsync(keywordsFile);
            var keywords = keywordLines.Select(w => $@"{w}");
            var keywordsJoined = string.Join("|", keywords);

            var pattern = $@"\b({keywordsJoined})\b";
            foreach (var line in lines)
            {
                var lineCased = line;

                var isComment = lineCased.TrimStart().StartsWith("--");
                if (!isComment)
                {
                    var matches = Regex.Matches(line, pattern, RegexOptions.IgnoreCase);
                    foreach (Match match in matches)
                    {
                        lineCased = lineCased.Remove(match.Index, match.Length).Insert(match.Index, match.Value.ToUpper());
                    }
                }

                cased.Add(lineCased);
            }

            return string.Join(Environment.NewLine, cased);
        }
    }
}
