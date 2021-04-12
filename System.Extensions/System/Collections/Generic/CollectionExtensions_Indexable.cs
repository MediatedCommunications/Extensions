namespace System.Collections.Generic {
    public static class CollectionExtensions_Indexable {

        public static IEnumerable<T> GetIndexableEnumerableFirst<T>(this IList<T> This) {
            for (int i = 0; i < This.Count; i++) {
                yield return This[i];
            }
        }

        public static IEnumerable<T> GetIndexableEnumerableLast<T>(this IList<T> This) {
            for (int i = This.Count - 1; i >= 0; i--) {
                yield return This[i];
            }
        }

    }

}
