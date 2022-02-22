using System.Collections.Immutable;
using System.Linq;

namespace System.Collections.Generic {

    public static class EnumerableExtensions_ToTreeTable {

        private static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? keyComparer, Func<TSource, TValue> elementSelector)
        where TKey : notnull {
            return source.ToImmutableDictionary(keySelector, elementSelector, keyComparer);
        }


        public static TreeTable<TItem, TKey1, TValue> ToTreeTable<TItem, TKey1, TValue>(this IEnumerable<TItem> This, Func<TItem, TKey1> Key1, Func<TItem, TValue> ValueSelector)
            where TKey1 : notnull {

            return ToTreeTable(This, Key1, default, ValueSelector);
        }

        public static TreeTable<TItem, TKey1, TKey2, TValue> ToTreeTable<TItem, TKey1, TKey2, TValue>(this IEnumerable<TItem> This, Func<TItem, TKey1> Key1, Func<TItem, TKey2> Key2, Func<TItem, TValue> ValueSelector)
            where TKey1 : notnull
            where TKey2 : notnull {

            return ToTreeTable(This, Key1, default, Key2, default, ValueSelector);
        }

        public static TreeTable<TItem, TKey1, TKey2, TKey3, TValue> ToTreeTable<TItem, TKey1, TKey2, TKey3, TValue>(this IEnumerable<TItem> This, Func<TItem, TKey1> Key1, Func<TItem, TKey2> Key2, Func<TItem, TKey3> Key3, Func<TItem, TValue> ValueSelector)
            where TKey1 : notnull
            where TKey2 : notnull
            where TKey3 : notnull {

            return ToTreeTable(This, Key1, default, Key2, default, Key3, default, ValueSelector);
        }

        public static TreeTable<TItem, TKey1, TKey2, TKey3, TKey4, TValue> ToTreeTable<TItem, TKey1, TKey2, TKey3, TKey4, TValue>(this IEnumerable<TItem> This, Func<TItem, TKey1> Key1, Func<TItem, TKey2> Key2, Func<TItem, TKey3> Key3, Func<TItem, TKey4> Key4, Func<TItem, TValue> ValueSelector)
            where TKey1 : notnull
            where TKey2 : notnull
            where TKey3 : notnull
            where TKey4 : notnull {

            return ToTreeTable(This, Key1, default, Key2, default, Key3, default, Key4, default, ValueSelector);
        }

        public static TreeTable<TItem, TKey1, TKey2, TKey3, TKey4, TKey5, TValue> ToTreeTable<TItem, TKey1, TKey2, TKey3, TKey4, TKey5, TValue>(this IEnumerable<TItem> This, Func<TItem, TKey1> Key1, Func<TItem, TKey2> Key2, Func<TItem, TKey3> Key3, Func<TItem, TKey4> Key4, Func<TItem, TKey5> Key5, Func<TItem, TValue> ValueSelector)
            where TKey1 : notnull
            where TKey2 : notnull
            where TKey3 : notnull
            where TKey4 : notnull
            where TKey5 : notnull {

            return ToTreeTable(This, Key1, default, Key2, default, Key3, default, Key4, default, Key5, default, ValueSelector);
        }




        public static TreeTable<TItem, TKey1, TValue> ToTreeTable<TItem, TKey1, TValue>(this IEnumerable<TItem> This, Func<TItem, TKey1> Key1, IEqualityComparer<TKey1>? Key1Comparer, Func<TItem, TValue> ValueSelector)
            where TKey1 : notnull {
            
            var Items = This.ToImmutableList();

            var Values = Items.ToLookup(Key1, Key1Comparer)
                .ToImmutableDictionary(x => x.Key, Key1Comparer, x => x.Select(ValueSelector).ToImmutableList())
                ;

            var ret = new TreeTable<TItem, TKey1, TValue>() {
                Items = Items,
                Values = Values,
                ValueSelector = ValueSelector,

                Key1Extractor = Key1,
                Key1Comparer = Key1Comparer,
               
            };

            return ret;
        }




        public static TreeTable<TItem, TKey1, TKey2, TValue> ToTreeTable<TItem, TKey1, TKey2, TValue>(this IEnumerable<TItem> This, Func<TItem, TKey1> Key1, IEqualityComparer<TKey1>? Key1Comparer, Func<TItem, TKey2> Key2, IEqualityComparer<TKey2>? Key2Comparer, Func<TItem, TValue> ValueSelector)
            where TKey1 : notnull
            where TKey2 : notnull {
            
            var Items = This.ToImmutableList();

            var Values = Items
                .ToLookup(Key1, Key1Comparer)
                .ToImmutableDictionary(x => x.Key, Key1Comparer, x => x.ToLookup(Key2, Key2Comparer)
                .ToImmutableDictionary(x => x.Key, Key2Comparer, x => x.Select(ValueSelector).ToImmutableList())
                )
                ;

            var ret = new TreeTable<TItem, TKey1, TKey2, TValue>() {
                Items = Items,
                Values = Values,
                ValueSelector = ValueSelector,

                Key1Extractor = Key1,
                Key1Comparer = Key1Comparer,

                Key2Extractor = Key2,
                Key2Comparer = Key2Comparer,
                
            };

            return ret;
        }



        public static TreeTable<TItem, TKey1, TKey2, TKey3, TValue> ToTreeTable<TItem, TKey1, TKey2, TKey3, TValue>(this IEnumerable<TItem> This, Func<TItem, TKey1> Key1, IEqualityComparer<TKey1>? Key1Comparer, Func<TItem, TKey2> Key2, IEqualityComparer<TKey2>? Key2Comparer, Func<TItem, TKey3> Key3, IEqualityComparer<TKey3>? Key3Comparer, Func<TItem, TValue> ValueSelector)
            where TKey1 : notnull
            where TKey2 : notnull
            where TKey3 : notnull {

            var Items = This.ToImmutableList();

            var Values = Items
                .ToLookup(Key1, Key1Comparer)
                .ToImmutableDictionary(x => x.Key, Key1Comparer, x => x.ToLookup(Key2, Key2Comparer)
                .ToImmutableDictionary(x => x.Key, Key2Comparer, x => x.ToLookup(Key3, Key3Comparer)
                .ToImmutableDictionary(x => x.Key, Key3Comparer, x => x.Select(ValueSelector).ToImmutableList())
                ))
                ;

            var ret = new TreeTable<TItem, TKey1, TKey2, TKey3, TValue>() {
                Items = Items,
                Values = Values,
                ValueSelector = ValueSelector,

                Key1Extractor = Key1,
                Key1Comparer = Key1Comparer,

                Key2Extractor = Key2,
                Key2Comparer = Key2Comparer,

                Key3Extractor = Key3,
                Key3Comparer = Key3Comparer,
            };

            return ret;
        }

        public static TreeTable<TItem, TKey1, TKey2, TKey3, TKey4, TValue> ToTreeTable<TItem, TKey1, TKey2, TKey3, TKey4, TValue>(this IEnumerable<TItem> This, Func<TItem, TKey1> Key1, IEqualityComparer<TKey1>? Key1Comparer, Func<TItem, TKey2> Key2, IEqualityComparer<TKey2>? Key2Comparer, Func<TItem, TKey3> Key3, IEqualityComparer<TKey3>? Key3Comparer, Func<TItem, TKey4> Key4, IEqualityComparer<TKey4>? Key4Comparer, Func<TItem, TValue> ValueSelector)
            where TKey1 : notnull
            where TKey2 : notnull
            where TKey3 : notnull
            where TKey4 : notnull {

            var Items = This.ToImmutableList();

            var Values = Items
                .ToLookup(Key1, Key1Comparer)
                .ToImmutableDictionary(x => x.Key, Key1Comparer, x => x.ToLookup(Key2, Key2Comparer)
                .ToImmutableDictionary(x => x.Key, Key2Comparer, x => x.ToLookup(Key3, Key3Comparer)
                .ToImmutableDictionary(x => x.Key, Key3Comparer, x => x.ToLookup(Key4, Key4Comparer)
                .ToImmutableDictionary(x => x.Key, Key4Comparer, x => x.Select(ValueSelector).ToImmutableList())
                )))
                ;

            var ret = new TreeTable<TItem, TKey1, TKey2, TKey3, TKey4, TValue>() {
                Items = Items,
                Values = Values,
                ValueSelector = ValueSelector,

                Key1Extractor = Key1,
                Key1Comparer = Key1Comparer,

                Key2Extractor = Key2,
                Key2Comparer = Key2Comparer,

                Key3Extractor = Key3,
                Key3Comparer = Key3Comparer,

                Key4Extractor = Key4,
                Key4Comparer = Key4Comparer,
            };

            return ret;
        }

        public static TreeTable<TItem, TKey1, TKey2, TKey3, TKey4, TKey5, TValue> ToTreeTable<TItem, TKey1, TKey2, TKey3, TKey4, TKey5, TValue>(this IEnumerable<TItem> This, Func<TItem, TKey1> Key1, IEqualityComparer<TKey1>? Key1Comparer, Func<TItem, TKey2> Key2, IEqualityComparer<TKey2>? Key2Comparer, Func<TItem, TKey3> Key3, IEqualityComparer<TKey3>? Key3Comparer, Func<TItem, TKey4> Key4, IEqualityComparer<TKey4>? Key4Comparer, Func<TItem, TKey5> Key5, IEqualityComparer<TKey5>? Key5Comparer, Func<TItem, TValue> ValueSelector)
            where TKey1 : notnull
            where TKey2 : notnull
            where TKey3 : notnull
            where TKey4 : notnull
            where TKey5 : notnull {

            var Items = This.ToImmutableList();

            var Values = Items
                .ToLookup(Key1, Key1Comparer)
                .ToImmutableDictionary(x => x.Key, Key1Comparer, x => x.ToLookup(Key2, Key2Comparer)
                .ToImmutableDictionary(x => x.Key, Key2Comparer, x => x.ToLookup(Key3, Key3Comparer)
                .ToImmutableDictionary(x => x.Key, Key3Comparer, x => x.ToLookup(Key4, Key4Comparer)
                .ToImmutableDictionary(x => x.Key, Key4Comparer, x => x.ToLookup(Key5, Key5Comparer)
                .ToImmutableDictionary(x => x.Key, Key5Comparer, x => x.Select(ValueSelector).ToImmutableList())
                ))))
                ;

            var ret = new TreeTable<TItem, TKey1, TKey2, TKey3, TKey4, TKey5, TValue>() {
                Items = Items,
                Values = Values,
                ValueSelector = ValueSelector,

                Key1Extractor = Key1,
                Key1Comparer = Key1Comparer,

                Key2Extractor = Key2,
                Key2Comparer = Key2Comparer,

                Key3Extractor = Key3,
                Key3Comparer = Key3Comparer,

                Key4Extractor = Key4,
                Key4Comparer = Key4Comparer,

                Key5Extractor = Key5,
                Key5Comparer = Key5Comparer,
            };

            return ret;
        }

    }
}