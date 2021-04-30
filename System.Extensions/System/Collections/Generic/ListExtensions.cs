namespace System.Collections.Generic {
    public static class ListExtensions {

        public static void AddFirst<T>(this IList This, T Item) {
            This.Insert(0, Item);
        }

        public static void AddLast<T>(this IList This, T Item) {
            This.Add(Item);
        }

    }
}
