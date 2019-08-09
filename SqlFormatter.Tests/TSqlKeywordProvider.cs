using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace SqlFormatter.Tests
{
    internal static class TSqlKeywordProvider
    {
        public static IEnumerable<string> Get()
        {
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "resources", @"tsql-keywords.txt");
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(nameof(path));
            }
            return File.ReadLines(path);
        }
    }
}
