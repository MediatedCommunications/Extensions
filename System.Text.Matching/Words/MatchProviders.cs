using System.Collections.Immutable;

namespace System.Text.Matching {
    public static class MatchProviders {
        public static ImmutableList<MatchProvider> All { get; private set; } = new MatchProvider[]{
            ExactMatchProvider.Instance,
            HammingMatchProvider.Instance,
            CambridgeMatchProvider.Instance,
            SoundsLikeMatchProvider.Instance,
        }.ToImmutableList();

        public static ExactMatchProvider Exact => ExactMatchProvider.Instance;
        public static HammingMatchProvider Hamming => HammingMatchProvider.Instance;
        public static CambridgeMatchProvider Cambridge => CambridgeMatchProvider.Instance;
        public static SoundsLikeMatchProvider SoundsLike => SoundsLikeMatchProvider.Instance;

    }

}
