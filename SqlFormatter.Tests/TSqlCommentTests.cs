using NUnit.Framework;
using SqlFormatter.Classes;

namespace SqlFormatter.Tests
{
    [Parallelizable(ParallelScope.Children)]
    internal class TSqlCommentTests
    {
        private string _keywordsRegex = null;

        [OneTimeSetUp]
        public void SetUp()
        {
            var keywords = TSqlKeywordProvider.Get();
            _keywordsRegex = SqlKeywordsProvider.GetKeywordsRegex(keywords);
        }

        [TestCase("--select")]
        [TestCase("-- select")]
        [TestCase(" -- select")]
        public void SqlCommentTests_SingleLineComment_CommentUnchanged(string input)
        {
            var output = Classes.SqlFormatter.Format(_keywordsRegex, input);
            Assert.NotNull(output);
            Assert.False(output.Changed);
            Assert.AreEqual(0, output.NumReplacements);
            Assert.AreEqual(input, output.Result);
            Assert.True(output.Verified);
        }

        [TestCase("select--select", "SELECT--select")]
        [TestCase("select-- select", "SELECT-- select")]
        [TestCase("select--  select", "SELECT--  select")]
        [TestCase("select --select", "SELECT --select")]
        [TestCase("select -- select", "SELECT -- select")]
        [TestCase("select --  select", "SELECT --  select")]
        public void SqlCommentTests_SingleLineComment_TransformedCorrectly(string input, string expected)
        {
            var output = Classes.SqlFormatter.Format(_keywordsRegex, input);
            AssertThatSqlCasedCorrectly(expected, output);
        }

        [TestCase("select -- select\rselect", "SELECT -- select\rSELECT")]
        [TestCase("select -- select\rselect -- select", "SELECT -- select\rSELECT -- select")]
        public void SqlCommentTests_SingleLineComment_RangeDetectedCorrectly(string input, string expected)
        {
            var output = Classes.SqlFormatter.Format(_keywordsRegex, input);
            AssertThatSqlCasedCorrectly(expected, output);
        }

        [TestCase("select /* select */", "SELECT /* select */")]
        [TestCase("select /* select */ select", "SELECT /* select */ SELECT")]
        [TestCase("select/*select*/", "SELECT/*select*/")]
        [TestCase("select/*\rselect\r*/", "SELECT/*\rselect\r*/")]
        [TestCase("select/*\r\rselect\r*/", "SELECT/*\r\rselect\r*/")]
        [TestCase("select/*\rselect\r*/select", "SELECT/*\rselect\r*/SELECT")]
        [TestCase("select/*\rselect\r*/select -- select", "SELECT/*\rselect\r*/SELECT -- select")]
        [TestCase("select/*\rselect\r*/select-- select", "SELECT/*\rselect\r*/SELECT-- select")]
        [TestCase("select/*\rselect\r*/select --select", "SELECT/*\rselect\r*/SELECT --select")]
        [TestCase("select/*\rselect*/select --select", "SELECT/*\rselect*/SELECT --select")]
        public void SqlCommentTests_MultiLineComment_TransformedCorrectly(string input, string expected)
        {
            var output = Classes.SqlFormatter.Format(_keywordsRegex, input);
            AssertThatSqlCasedCorrectly(expected, output);
        }

        [TestCase("", false)]
        [TestCase("-- select select select", false)]
        [TestCase("--select select select", false)]
        [TestCase("/*select select select*/", false)]
        [TestCase("/* select select select */", false)]
        [TestCase("select", true)]
        [TestCase("selectt", false)]
        [TestCase("selecxt", false)]
        [TestCase("selec t", false)]
        [TestCase("selectselect", false)]
        [TestCase("select select", true)]
        [TestCase("select selectselect", true)]
        [TestCase("select select select", true)]
        [TestCase("select select select ", true)]
        [TestCase("select\r--select", true)]
        [TestCase("select\r/* select */", true)]
        [TestCase("--select\r/* select */", false)]
        [TestCase("/*select*/\r--select", false)]
        [TestCase("select\r/* select */select select", true)]
        public void SqlCommentTests_OutputFlag_Changed_IsCorrect(string input, bool changeExpected)
        {
            var output = Classes.SqlFormatter.Format(_keywordsRegex, input);
            Assert.NotNull(output);
            Assert.AreEqual(changeExpected, output.Changed);
        }

        [TestCase("", 0)]
        [TestCase("-- select select select", 0)]
        [TestCase("--select select select", 0)]
        [TestCase("/*select select select*/", 0)]
        [TestCase("/* select select select */", 0)]
        [TestCase("select", 1)]
        [TestCase("selectt", 0)]
        [TestCase("selecxt", 0)]
        [TestCase("selec t", 0)]
        [TestCase("selectselect", 0)]
        [TestCase("select select", 2)]
        [TestCase("select selectselect", 1)]
        [TestCase("select select select", 3)]
        [TestCase("select select select ", 3)]
        [TestCase("select\r--select", 1)]
        [TestCase("select\r/* select */", 1)]
        [TestCase("--select\r/* select */", 0)]
        [TestCase("/*select*/\r--select", 0)]
        [TestCase("select\r/* select */select select", 3)]
        public void SqlCommentTests_OutputFlag_NumReplacements_IsCorrect(string input, int expectedNumReplacements)
        {
            var output = Classes.SqlFormatter.Format(_keywordsRegex, input);
            Assert.NotNull(output);
            Assert.AreEqual(expectedNumReplacements, output.NumReplacements);
        }

        [TestCase("")]
        [TestCase("selec")]
        [TestCase("select")]
        [TestCase("select ")]
        [TestCase("select s")]
        public void SqlCommentTests_OutputFlag_Verified_IsCorrect(string input)
        {
            var output = Classes.SqlFormatter.Format(_keywordsRegex, input);
            Assert.NotNull(output);
            Assert.True(output.Verified);
        }

        private void AssertThatSqlCasedCorrectly(string expected, SqlFormatterResult output)
        {
            Assert.NotNull(output);
            Assert.True(output.Changed);
            Assert.True(output.Verified);
            Assert.AreEqual(expected, output.Result);
        }
    }
}
