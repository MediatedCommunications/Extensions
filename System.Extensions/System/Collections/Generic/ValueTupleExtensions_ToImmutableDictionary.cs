using System.Collections.Immutable;

namespace System.Collections.Generic {
    public static class ValueTupleExtensions_ToImmutableDictionary {
        public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> This, IEqualityComparer<TKey>? KeyComparer = default) where TKey : notnull {

            var ret = This.ToKeyValuePairs().ToImmutableDictionary(KeyComparer);
            return ret;
        }
    }

}
