namespace System.Collections.Generic {
    public static class EnumerableExtensions_ToDictionaries {
        public static SortedDictionary<TKey, TValue> ToSortedDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, IComparer<TKey>? Comparer = null) where TKey : notnull {

            var ret = new SortedDictionary<TKey, TValue>(Comparer);

            foreach (var item in source.Coalesce()) {
                var key = keySelector(item);
                var value = elementSelector(item);

                ret[key] = value;
            }

            return ret;

        }
    }

}
