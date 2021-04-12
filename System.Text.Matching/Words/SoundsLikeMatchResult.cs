using System.Diagnostics;

namespace System.Text.Matching {
    public record SoundsLikeMatchResult : MatchResult {
        public string LeftCode { get; init; } = string.Empty;
        public string RightCode { get; init; } = string.Empty;

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Postfix.Add($@"{LeftCode}={RightCode}")
                ;
        }

        public bool Matches => LeftCode == RightCode;

    }
}
