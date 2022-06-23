namespace System.Collections.Generic {
    public static class ValueTupleExtensions {
        public static KeyValuePair<TKey, TValue> ToKeyValuePair<TKey, TValue>(this ValueTuple<TKey, TValue> This) {
            return KeyValuePair.Create(This.Item1, This.Item2);
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> ToKeyValuePairs<TKey, TValue>(this IEnumerable<ValueTuple<TKey, TValue>> This) {
            return This.Select(x => x.ToKeyValuePair());
        }
    }

}
