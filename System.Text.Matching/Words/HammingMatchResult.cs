using System.Diagnostics;

namespace System.Text.Matching {
    public record HammingMatchResult : MatchResult {
        public long Distance { get; init; }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Postfix.Add($@"Distance={Distance}")
                ;
        }


    }
}
