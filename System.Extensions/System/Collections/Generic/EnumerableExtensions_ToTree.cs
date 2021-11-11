using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace System.Collections.Generic {

    public abstract record TreeTable<TValue> : DisplayRecord, IEnumerable<TValue> {
        public abstract IEnumerator<TValue> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }

    public static class EnumerableExtensions_ToTreeTable {

        private static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? keyComparer, Func<TSource, TValue> elementSelector)
        where TKey : notnull {
            return source.ToImmutableDictionary(keySelector, elementSelector, keyComparer);
        }


        public static TreeTable<TKey1, TValue> ToTreeTable<TValue, TKey1>(this IEnumerable<TValue> This, Func<TValue, TKey1> Key1)
            where TKey1 : notnull {

            return ToTreeTable(This, Key1, default);
        }

        public static TreeTable<TKey1, TKey2, TValue> ToTreeTable<TValue, TKey1, TKey2>(this IEnumerable<TValue> This, Func<TValue, TKey1> Key1, Func<TValue, TKey2> Key2)
            where TKey1 : notnull
            where TKey2 : notnull {

            return ToTreeTable(This, Key1, default, Key2, default);
        }

        public static TreeTable<TKey1, TKey2, TKey3, TValue> ToTreeTable<TValue, TKey1, TKey2, TKey3>(this IEnumerable<TValue> This, Func<TValue, TKey1> Key1, Func<TValue, TKey2> Key2, Func<TValue, TKey3> Key3)
            where TKey1 : notnull
            where TKey2 : notnull
            where TKey3 : notnull {

            return ToTreeTable(This, Key1, default, Key2, default, Key3, default);
        }

        public static TreeTable<TKey1, TKey2, TKey3, TKey4, TValue> ToTreeTable<TValue, TKey1, TKey2, TKey3, TKey4>(this IEnumerable<TValue> This, Func<TValue, TKey1> Key1, Func<TValue, TKey2> Key2, Func<TValue, TKey3> Key3, Func<TValue, TKey4> Key4)
            where TKey1 : notnull
            where TKey2 : notnull
            where TKey3 : notnull
            where TKey4 : notnull {

            return ToTreeTable(This, Key1, default, Key2, default, Key3, default, Key4, default);
        }

        public static TreeTable<TKey1, TKey2, TKey3, TKey4, TKey5, TValue> ToTreeTable<TValue, TKey1, TKey2, TKey3, TKey4, TKey5>(this IEnumerable<TValue> This, Func<TValue, TKey1> Key1, Func<TValue, TKey2> Key2, Func<TValue, TKey3> Key3, Func<TValue, TKey4> Key4, Func<TValue, TKey5> Key5)
            where TKey1 : notnull
            where TKey2 : notnull
            where TKey3 : notnull
            where TKey4 : notnull
            where TKey5 : notnull {

            return ToTreeTable(This, Key1, default, Key2, default, Key3, default, Key4, default, Key5, default);
        }




        public static TreeTable<TKey1, TValue> ToTreeTable<TValue, TKey1>(this IEnumerable<TValue> This, Func<TValue, TKey1> Key1, IEqualityComparer<TKey1>? Key1Comparer)
            where TKey1 : notnull {
            var Values = This.ToLookup(Key1, Key1Comparer)
                .ToImmutableDictionary(x => x.Key, Key1Comparer, x => x.ToImmutableList())
                ;

            var ret = new TreeTable<TKey1, TValue>() {
                Values = Values,
                Key1Extractor = Key1,
                Key1Comparer = Key1Comparer,
               
            };

            return ret;
        }




        public static TreeTable<TKey1, TKey2, TValue> ToTreeTable<TValue, TKey1, TKey2>(this IEnumerable<TValue> This, Func<TValue, TKey1> Key1, IEqualityComparer<TKey1>? Key1Comparer, Func<TValue, TKey2> Key2, IEqualityComparer<TKey2>? Key2Comparer)
            where TKey1 : notnull
            where TKey2 : notnull {
            var Values = This
                .ToLookup(Key1, Key1Comparer)
                .ToImmutableDictionary(x => x.Key, Key1Comparer, x => x.ToLookup(Key2, Key2Comparer)
                .ToImmutableDictionary(x => x.Key, Key2Comparer, x => x.ToImmutableList())
                )
                ;

            var ret = new TreeTable<TKey1, TKey2, TValue>() {
                Values = Values,
                Key1Extractor = Key1,
                Key1Comparer = Key1Comparer,

                Key2Extractor = Key2,
                Key2Comparer = Key2Comparer,
                
            };

            return ret;
        }



        public static TreeTable<TKey1, TKey2, TKey3, TValue> ToTreeTable<TValue, TKey1, TKey2, TKey3>(this IEnumerable<TValue> This, Func<TValue, TKey1> Key1, IEqualityComparer<TKey1>? Key1Comparer, Func<TValue, TKey2> Key2, IEqualityComparer<TKey2>? Key2Comparer, Func<TValue, TKey3> Key3, IEqualityComparer<TKey3>? Key3Comparer)
            where TKey1 : notnull
            where TKey2 : notnull
            where TKey3 : notnull {
            var Values = This
                .ToLookup(Key1, Key1Comparer)
                .ToImmutableDictionary(x => x.Key, Key1Comparer, x => x.ToLookup(Key2, Key2Comparer)
                .ToImmutableDictionary(x => x.Key, Key2Comparer, x => x.ToLookup(Key3, Key3Comparer)
                .ToImmutableDictionary(x => x.Key, Key3Comparer, x => x.ToImmutableList())
                ))
                ;

            var ret = new TreeTable<TKey1, TKey2, TKey3, TValue>() {
                Values = Values,

                Key1Extractor = Key1,
                Key1Comparer = Key1Comparer,

                Key2Extractor = Key2,
                Key2Comparer = Key2Comparer,

                Key3Extractor = Key3,
                Key3Comparer = Key3Comparer,
            };

            return ret;
        }

        public static TreeTable<TKey1, TKey2, TKey3, TKey4, TValue> ToTreeTable<TValue, TKey1, TKey2, TKey3, TKey4>(this IEnumerable<TValue> This, Func<TValue, TKey1> Key1, IEqualityComparer<TKey1>? Key1Comparer, Func<TValue, TKey2> Key2, IEqualityComparer<TKey2>? Key2Comparer, Func<TValue, TKey3> Key3, IEqualityComparer<TKey3>? Key3Comparer, Func<TValue, TKey4> Key4, IEqualityComparer<TKey4>? Key4Comparer)
            where TKey1 : notnull
            where TKey2 : notnull
            where TKey3 : notnull
            where TKey4 : notnull {
            var Values = This
                .ToLookup(Key1, Key1Comparer)
                .ToImmutableDictionary(x => x.Key, Key1Comparer, x => x.ToLookup(Key2, Key2Comparer)
                .ToImmutableDictionary(x => x.Key, Key2Comparer, x => x.ToLookup(Key3, Key3Comparer)
                .ToImmutableDictionary(x => x.Key, Key3Comparer, x => x.ToLookup(Key4, Key4Comparer)
                .ToImmutableDictionary(x => x.Key, Key4Comparer, x => x.ToImmutableList())
                )))
                ;

            var ret = new TreeTable<TKey1, TKey2, TKey3, TKey4, TValue>() {
                Values = Values,

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

        public static TreeTable<TKey1, TKey2, TKey3, TKey4, TKey5, TValue> ToTreeTable<TValue, TKey1, TKey2, TKey3, TKey4, TKey5>(this IEnumerable<TValue> This, Func<TValue, TKey1> Key1, IEqualityComparer<TKey1>? Key1Comparer, Func<TValue, TKey2> Key2, IEqualityComparer<TKey2>? Key2Comparer, Func<TValue, TKey3> Key3, IEqualityComparer<TKey3>? Key3Comparer, Func<TValue, TKey4> Key4, IEqualityComparer<TKey4>? Key4Comparer, Func<TValue, TKey5> Key5, IEqualityComparer<TKey5>? Key5Comparer)
            where TKey1 : notnull
            where TKey2 : notnull
            where TKey3 : notnull
            where TKey4 : notnull
            where TKey5 : notnull {
            var Values = This
                .ToLookup(Key1, Key1Comparer)
                .ToImmutableDictionary(x => x.Key, Key1Comparer, x => x.ToLookup(Key2, Key2Comparer)
                .ToImmutableDictionary(x => x.Key, Key2Comparer, x => x.ToLookup(Key3, Key3Comparer)
                .ToImmutableDictionary(x => x.Key, Key3Comparer, x => x.ToLookup(Key4, Key4Comparer)
                .ToImmutableDictionary(x => x.Key, Key4Comparer, x => x.ToLookup(Key5, Key5Comparer)
                .ToImmutableDictionary(x => x.Key, Key5Comparer, x => x.ToImmutableList())
                ))))
                ;

            var ret = new TreeTable<TKey1, TKey2, TKey3, TKey4, TKey5, TValue>() {
                Values = Values,

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