using System.Collections.Immutable;

namespace System.Text.Matching {
    public record DefaultPhraseMatchProviderOptions {
        public ImmutableList<MatchProvider> Providers { get; init; } = MatchProviders.All;
        public double MinimumWeight { get; init; } = 0.25;
    }


}
