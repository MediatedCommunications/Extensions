using System.Diagnostics;

namespace System.Text.Matching {
    public record SoundsLikeMatchResult : MatchResult {
        public string LeftCode { get; init; } = Strings.Empty;
        public string RightCode { get; init; } = Strings.Empty;

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Postfix.AddExpression(LeftCode)
                .Postfix.AddExpression(RightCode)
                ;
        }

        public bool Matches => LeftCode == RightCode;

    }
}
