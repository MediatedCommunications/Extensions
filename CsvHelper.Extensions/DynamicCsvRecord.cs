using System.Collections.Immutable;
using System.Diagnostics;

namespace CsvHelper {
    public record DynamicCsvRecord : DisplayRecord {
        public ImmutableDictionary<int, string> IndexToHeaders { get; init; } = ImmutableDictionary<int, string>.Empty;
        public ImmutableDictionary<string, ImmutableList<int>> HeadersToIndex { get; init; } = ImmutableDictionary<string, ImmutableList<int>>.Empty;
        public ImmutableDictionary<string, int> HeaderToIndex { get; init; } = ImmutableDictionary<string, int>.Empty;

        public ImmutableList<string> Values { get; init; } = ImmutableList<string>.Empty;
    }

}
