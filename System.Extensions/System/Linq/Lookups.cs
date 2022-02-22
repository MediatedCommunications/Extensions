using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq {
    public static class Lookups {
        public static ILookup<TKey, TValue> Empty<TKey, TValue>()
            where TKey : notnull {

            return Lookups<TKey, TValue>.Empty;
        }
    }

    public static class Lookups<TKey, TValue>
        where TKey : notnull {
        public static ILookup<TKey, TValue> Empty { get; }

        static Lookups() {
            Empty = ImmutableDictionary<TKey, TValue>.Empty.ToLookup(x => x.Key, x => x.Value);
        }
    }
}
