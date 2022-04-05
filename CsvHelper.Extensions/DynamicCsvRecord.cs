using System;
using System.Collections.Immutable;
using System.Diagnostics;

namespace CsvHelper {
    public record DynamicCsvRecord : DisplayRecord {
        public ImmutableDictionary<int, string> IndexToHeaders { get; init; } = ImmutableDictionary<int, string>.Empty;
        public ImmutableDictionary<string, ImmutableArray<int>> HeadersToIndex { get; init; } = ImmutableDictionary<string, ImmutableArray<int>>.Empty;
        public ImmutableDictionary<string, int> HeaderToIndex { get; init; } = ImmutableDictionary<string, int>.Empty;

        public ImmutableArray<string> Values { get; init; } = ImmutableArray<string>.Empty;

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            var ret = base.GetDebuggerDisplayBuilder(Builder);
            
            var ToAdd = Math.Min(7, Values.Length);
            for (var i = 0; i < ToAdd; i++) {
                if(this.TryGetValue(i, out var Header, out var Value)) {
                    var MyHeader = Header ?? $@"{i}";

                    ret.Data.AddNameValue(MyHeader, Value);
                }
            }


            return ret;
        }

    }


}
