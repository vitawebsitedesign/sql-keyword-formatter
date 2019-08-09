using System.Collections.Generic;

namespace SqlFormatter.Classes
{
    public static class SqlKeywordsProvider
    {
        public static string GetKeywordsRegex(IEnumerable<string> keywords)
        {
            return string.Join("|", keywords);
        }
    }
}
