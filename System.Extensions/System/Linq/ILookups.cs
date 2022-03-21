using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq {
    public static class ILookups {
        public static ILookup<TKey, TValue> Empty<TKey, TValue>() {

            return ILookups<TKey, TValue>.Empty;

        }
    }

    public static class ILookups<TKey, TValue> {
        public static ILookup<TKey, TValue> Empty { get; }

        static ILookups() {

            Empty = Array.Empty<object>().ToLookup(x => (TKey)x, x => (TValue)x);

        }
    }
}
