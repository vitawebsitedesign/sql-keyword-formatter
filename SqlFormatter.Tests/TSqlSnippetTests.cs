using NUnit.Framework;
using SqlFormatter.Classes;
using System.IO;

namespace SqlFormatter.Tests
{
    [Parallelizable(ParallelScope.Children)]
    internal class TSqlSnippetTests
    {
        private string _keywordsRegex = null;

        [OneTimeSetUp]
        public void SetUp()
        {
            var keywords = TSqlKeywordProvider.Get();
            _keywordsRegex = SqlKeywordsProvider.GetKeywordsRegex(keywords);
        }

        [TestCase("all-keywords.sql")]
        [TestCase("empty.sql")]
        [TestCase("keyword-in-string.sql")]
        [TestCase("keyword-with-newline.sql")]
        [TestCase("keyword-without-newline.sql")]
        [TestCase("leading-tab.sql")]
        [TestCase("leading-whitespace.sql")]
        [TestCase("multiline-with-comment.sql")]
        [TestCase("multiline-with-comments.sql")]
        [TestCase("multiline-without-comment.sql")]
        [TestCase("multiple-queries.sql")]
        [TestCase("scalar-function.sql")]
        [TestCase("single-empty-line.sql")]
        [TestCase("single-line-with-comment.sql")]
        [TestCase("single-line-with-comments.sql")]
        [TestCase("single-line-without-comment.sql")]
        [TestCase("spaces-around-comment.sql")]
        [TestCase("stored-procedure-multiple-resultsets.sql")]
        [TestCase("stored-procedure-single-resultset.sql")]
        [TestCase("subqueries.sql")]
        [TestCase("table-declaration.sql")]
        [TestCase("table-function.sql")]
        [TestCase("tabs-around-comment.sql")]
        [TestCase("trailing-tab.sql")]
        [TestCase("trailing-whitespace.sql")]
        public void TSqlSnippetTests_SnippetsTransformedCorrectly(string snippetFile)
        {
            var pathSnippets = Path.Combine(TestContext.CurrentContext.TestDirectory, "resources", "snippets");
            var pathUncased = Path.Combine(pathSnippets, "uncased", snippetFile);
            var pathCased = Path.Combine(pathSnippets, "cased", snippetFile);
            if (!File.Exists(pathUncased))
            {
                throw new FileNotFoundException(nameof(pathUncased));
            }
            if (!File.Exists(pathCased))
            {
                throw new FileNotFoundException(nameof(pathCased));
            }

            var uncased = File.ReadAllText(pathUncased);
            var cased = File.ReadAllText(pathCased);
            var output = Classes.SqlFormatter.Format(_keywordsRegex, uncased);

            Assert.NotNull(output);
            Assert.AreEqual(cased, output.Result);
            Assert.True(output.Verified);
        }
    }
}
