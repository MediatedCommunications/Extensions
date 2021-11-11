using System.Collections.Immutable;
using System.Linq;

namespace System.Collections.Generic {
    public record TreeTable<TKey1, TKey2, TValue> : TreeTable<TValue>
        where TKey1 : notnull
        where TKey2 : notnull
        {

        public ImmutableDictionary<TKey1, ImmutableDictionary<TKey2, ImmutableList<TValue>>> Values { get; init; } = ImmutableDictionary<TKey1, ImmutableDictionary<TKey2, ImmutableList<TValue>>>.Empty;

        public Func<TValue, TKey1>? Key1Extractor { get; init; }
        public IEqualityComparer<TKey1>? Key1Comparer { get; init; }

        public Func<TValue, TKey2>? Key2Extractor { get; init; }
        public IEqualityComparer<TKey2>? Key2Comparer { get; init; }

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

        public TreeTable<TKey1, TKey2, TValue> Add(TKey1 Key1, TKey2 Key2, TValue Value) {
            var Level0 = Values;
            
            if(!Level0.TryGetValue(Key1, out var Level1)) {
                Level1 = ImmutableDictionary<TKey2, ImmutableList<TValue>>.Empty.WithComparers(Key2Comparer);
            }

            if (Level1.TryGetValue(Key2, out var Level2)) {
                Level2 = Level2.Add(Value);
            } else {
                Level2 = new[] { Value }.ToImmutableList();
            }

            Level1 = Level1.SetItem(Key2, Level2);
            Level0 = Level0.SetItem(Key1, Level1);

            var ret = this with {
                Values = Level0
            };

            return ret;
        }

    }
}