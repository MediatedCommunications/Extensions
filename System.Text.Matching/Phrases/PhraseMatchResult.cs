using System.Collections.Immutable;

namespace System.Text.Matching {
    public record PhraseMatchResult {
        public ImmutableList<MatchResult> Matches { get; init; } = ImmutableList<MatchResult>.Empty;
        public double Weight { get; init; }

    }


}
