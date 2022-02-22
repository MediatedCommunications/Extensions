namespace System.Collections.Generic {
    public static class TreeTables {
        public static TreeTable<TItem, TKey1, TValue> Empty<TItem, TKey1, TValue>()
            where TKey1 : notnull {
            return TreeTable<TItem, TKey1, TValue>.Empty;
        }

        public static TreeTable<TItem, TKey1, TKey2, TValue> Empty<TItem, TKey1, TKey2, TValue>()
            where TKey1 : notnull 
            where TKey2 : notnull {
            return TreeTable<TItem, TKey1, TKey2, TValue>.Empty;
        }

        public static TreeTable<TItem, TKey1, TKey2, TKey3, TValue> Empty<TItem, TKey1, TKey2, TKey3, TValue>()
            where TKey1 : notnull 
            where TKey2 : notnull
            where TKey3 : notnull{
            return TreeTable<TItem, TKey1, TKey2, TKey3, TValue>.Empty;
        }

        public static TreeTable<TItem, TKey1, TKey2, TKey3, TKey4, TValue> Empty<TItem, TKey1, TKey2, TKey3, TKey4, TValue>()
                    where TKey1 : notnull
                    where TKey2 : notnull
                    where TKey3 : notnull 
                    where TKey4 : notnull {
            return TreeTable<TItem, TKey1, TKey2, TKey3, TKey4, TValue>.Empty;
        }

        public static TreeTable<TItem, TKey1, TKey2, TKey3, TKey4, TKey5, TValue> Empty<TItem, TKey1, TKey2, TKey3, TKey4, TKey5, TValue>()
                    where TKey1 : notnull
                    where TKey2 : notnull
                    where TKey3 : notnull
                    where TKey4 : notnull 
                    where TKey5 : notnull {
            return TreeTable<TItem, TKey1, TKey2, TKey3, TKey4, TKey5, TValue>.Empty;
        }
    }
}