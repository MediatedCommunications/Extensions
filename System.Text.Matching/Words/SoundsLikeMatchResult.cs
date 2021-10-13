using System.Diagnostics;

namespace System.Text.Matching {
    public record SoundsLikeMatchResult : MatchResult {
        public string LeftCode { get; init; } = string.Empty;
        public string RightCode { get; init; } = string.Empty;

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Postfix.AddExpression(RightCode, LeftCode)
                ;
        }

        public bool Matches => LeftCode == RightCode;

    }
}
