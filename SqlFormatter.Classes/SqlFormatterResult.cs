namespace SqlFormatter.Classes
{
    public class SqlFormatterResult
    {
        public int NumReplacements { get; }
        public string Result { get; }
        public bool Changed { get; }
        public bool Verified { get; }

        public SqlFormatterResult(int numReplacements, string result, bool changed, bool verified)
        {
            NumReplacements = numReplacements;
            Result = result;
            Changed = changed;
            Verified = verified;
        }
    }
}
