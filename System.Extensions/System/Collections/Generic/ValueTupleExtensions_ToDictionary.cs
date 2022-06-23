namespace System.Collections.Generic {
    public static class ValueTupleExtensions_ToDictionary {
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> This, IEqualityComparer<TKey>? KeyComparer = default) where TKey : notnull {
            var ret = new Dictionary<TKey, TValue>(This.ToKeyValuePairs(), KeyComparer);

            return ret;
        }
    }

}
