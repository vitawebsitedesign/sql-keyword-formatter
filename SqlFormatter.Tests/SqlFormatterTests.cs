using NUnit.Framework;
using SqlFormatter.Classes;
using System;

namespace SqlFormatter.Tests
{
    [Parallelizable(ParallelScope.Children)]
    internal class SqlFormatterTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        public void SqlFormatterTests_ThrowsExceptionForInvalidArgument_KeywordsRegex(string keywordsRegex)
        {
            Assert.Throws<ArgumentException>(() => Classes.SqlFormatter.Format(keywordsRegex, ""));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("  ")]
        [TestCase("selec")]
        [TestCase("selec ")]
        public void SqlFormatterTests_ReturnsCorrectResult_ForUncasableInput(string input)
        {
            var keywords = TSqlKeywordProvider.Get();
            var keywordsRegex = SqlKeywordsProvider.GetKeywordsRegex(keywords);
            SqlFormatterResult output = null;
            Assert.DoesNotThrow(() => output = Classes.SqlFormatter.Format(keywordsRegex, input));

            Assert.NotNull(output);
            Assert.False(output.Changed);
            Assert.Zero(output.NumReplacements);
            Assert.AreEqual(input, output.Result);
            Assert.True(output.Verified);
        }
    }
}
