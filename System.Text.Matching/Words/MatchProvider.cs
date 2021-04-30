namespace System.Text.Matching {
    public abstract class MatchProvider {
        public abstract MatchResult Match(string Left, string Right, StringComparer? OptionalComparer = default);


    }

}
