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

    public record TreeTable<TKey1, TValue>  : TreeTable<TValue>
        where TKey1 : notnull
        {

        public ImmutableDictionary<TKey1, ImmutableList<TValue>> Values { get; init; } = ImmutableDictionary<TKey1, ImmutableList<TValue>>.Empty;

        public override IEnumerator<TValue> GetEnumerator() {
            var items = (
                from a in Values.Values
                from z in a
                select z
                );

            foreach (var item in items) {
                yield return item;
            }
                
        }

        public ImmutableList<TValue>? TryGetValue(TKey1 Key1) {
            if(!(Values.TryGetValue(Key1, out var ret))) {
                ret = default;
            }

            return ret;
        }

        public ImmutableList<TValue> SafeGetValue(TKey1 Key1) {
            return TryGetValue(Key1) ?? ImmutableList<TValue>.Empty;
        }
      

    }

    public record TreeTable<TKey1, TKey2, TValue> : TreeTable<TValue>
        where TKey1 : notnull
        where TKey2 : notnull
        {

        public ImmutableDictionary<TKey1, ImmutableDictionary<TKey2, ImmutableList<TValue>>> Values { get; init; } = ImmutableDictionary<TKey1, ImmutableDictionary<TKey2, ImmutableList<TValue>>>.Empty;

        public override IEnumerator<TValue> GetEnumerator() {
            var items = (
                from x in Values.Values
                from y in x.Values
                from z in y
                select z
                );

            foreach (var item in items) {
                yield return item;
            }

        }


        public ImmutableList<TValue>? TryGetValue(TKey1 Key1, TKey2 Key2) {
            if (!(Values.TryGetValue(Key1, out var V2) && V2.TryGetValue(Key2, out var ret))) {
                ret = default;
            }

            return ret;
        }

        public ImmutableList<TValue> SafeGetValue(TKey1 Key1, TKey2 Key2) {
            return TryGetValue(Key1, Key2) ?? ImmutableList<TValue>.Empty;
        }

    }

    public record TreeTable<TKey1, TKey2, TKey3, TValue> : TreeTable<TValue>
        where TKey1 : notnull
        where TKey2 : notnull
        where TKey3 : notnull {

        public ImmutableDictionary<TKey1, ImmutableDictionary<TKey2, ImmutableDictionary<TKey3, ImmutableList<TValue>>>> Values { get; init; } = ImmutableDictionary<TKey1, ImmutableDictionary<TKey2, ImmutableDictionary<TKey3, ImmutableList<TValue>>>>.Empty;


        public override IEnumerator<TValue> GetEnumerator() {
            var items = (
                from w in Values.Values
                from x in w.Values
                from y in x.Values
                from z in y
                select z
                );

            foreach (var item in items) {
                yield return item;
            }

        }

        public ImmutableList<TValue>? TryGetValue(TKey1 Key1, TKey2 Key2, TKey3 Key3) {
            if (!(Values.TryGetValue(Key1, out var V2) && V2.TryGetValue(Key2, out var V3) && V3.TryGetValue(Key3, out var ret))) {
                ret = default;
            }

            return ret;
        }

        public ImmutableList<TValue> SafeGetValue(TKey1 Key1, TKey2 Key2, TKey3 Key3) {
            return TryGetValue(Key1, Key2, Key3) ?? ImmutableList<TValue>.Empty;
        }

    }

    public record TreeTable<TKey1, TKey2, TKey3, TKey4, TValue> : TreeTable<TValue>
        where TKey1 : notnull
        where TKey2 : notnull
        where TKey3 : notnull
        where TKey4 : notnull {

        public ImmutableDictionary<TKey1, ImmutableDictionary<TKey2, ImmutableDictionary<TKey3, ImmutableDictionary<TKey4, ImmutableList<TValue>>>>> Values { get; init; } = ImmutableDictionary<TKey1, ImmutableDictionary<TKey2, ImmutableDictionary<TKey3, ImmutableDictionary<TKey4, ImmutableList<TValue>>>>>.Empty;


        public override IEnumerator<TValue> GetEnumerator() {
            var items = (
                from v in Values.Values
                from w in v.Values
                from x in w.Values
                from y in x.Values
                from z in y
                select z
                );

            foreach (var item in items) {
                yield return item;
            }

        }

        public ImmutableList<TValue>? TryGetValue(TKey1 Key1, TKey2 Key2, TKey3 Key3, TKey4 Key4) {
            if (!(Values.TryGetValue(Key1, out var V2) && V2.TryGetValue(Key2, out var V3) && V3.TryGetValue(Key3, out var V4) && V4.TryGetValue(Key4, out var ret))) {
                ret = default;
            }

            return ret;
        }

        public ImmutableList<TValue> SafeGetValue(TKey1 Key1, TKey2 Key2, TKey3 Key3, TKey4 Key4) {
            return TryGetValue(Key1, Key2, Key3, Key4) ?? ImmutableList<TValue>.Empty;
        }

    }

    public record TreeTable<TKey1, TKey2, TKey3, TKey4, TKey5, TValue> : TreeTable<TValue>
            where TKey1 : notnull
            where TKey2 : notnull
            where TKey3 : notnull
            where TKey4 : notnull
            where TKey5 : notnull {

        public ImmutableDictionary<TKey1, ImmutableDictionary<TKey2, ImmutableDictionary<TKey3, ImmutableDictionary<TKey4, ImmutableDictionary<TKey5, ImmutableList<TValue>>>>>> Values { get; init; } = ImmutableDictionary<TKey1, ImmutableDictionary<TKey2, ImmutableDictionary<TKey3, ImmutableDictionary<TKey4, ImmutableDictionary<TKey5, ImmutableList<TValue>>>>>>.Empty;


        public override IEnumerator<TValue> GetEnumerator() {
            var items = (
                from u in Values.Values
                from v in u.Values
                from w in v.Values
                from x in w.Values
                from y in x.Values
                from z in y
                select z
                );

            foreach (var item in items) {
                yield return item;
            }

        }

        public ImmutableList<TValue>? TryGetValue(TKey1 Key1, TKey2 Key2, TKey3 Key3, TKey4 Key4, TKey5 Key5) {
            if (!(Values.TryGetValue(Key1, out var V2) && V2.TryGetValue(Key2, out var V3) && V3.TryGetValue(Key3, out var V4) && V4.TryGetValue(Key4, out var V5) && V5.TryGetValue(Key5, out var ret))) {
                ret = default;
            }

            return ret;
        }

        public ImmutableList<TValue> SafeGetValue(TKey1 Key1, TKey2 Key2, TKey3 Key3, TKey4 Key4, TKey5 Key5) {
            return TryGetValue(Key1, Key2, Key3, Key4, Key5) ?? ImmutableList<TValue>.Empty;
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
                Values = Values
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
            };

            return ret;
        }

    }
}