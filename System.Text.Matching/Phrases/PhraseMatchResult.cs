using System.Collections.Immutable;
using System.Diagnostics;

namespace System.Text.Matching {
    public record PhraseMatchResult : DisplayRecord {
        public ImmutableList<MatchResult> Matches { get; init; } = ImmutableList<MatchResult>.Empty;
        public double Weight { get; init; }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add(Matches)
                .Postfix.AddPair(nameof(Weight), Weight)
                ;
        }


        public static PhraseMatchResult Create(ImmutableList<MatchResult> Matches, double Weight) {
            return new PhraseMatchResult() {
                Matches = Matches,
                Weight = Weight
            };
        }

        public static PhraseMatchResult<TLeft, TRight> Create<TLeft, TRight>(ImmutableList<MatchResult> Matches, double Weight, TokenizeResult<TLeft> Left, TokenizeResult<TRight> Right) {
            var ret = new PhraseMatchResult<TLeft, TRight>(Left, Right) {
                Matches = Matches,
                Weight = Weight,
            };
            return ret;
        }
    }

    public record PhraseMatchResult<TLeft, TRight> : PhraseMatchResult {
        public TokenizeResult<TLeft> Left { get; init; }
        public TokenizeResult<TRight> Right { get; init; }

        public PhraseMatchResult(TokenizeResult<TLeft> Left, TokenizeResult<TRight> Right) {
            this.Left = Left;
            this.Right = Right;
        }


    }


}
