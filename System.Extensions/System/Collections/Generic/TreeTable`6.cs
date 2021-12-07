using System.Collections.Immutable;
using System.Linq;

namespace System.Collections.Generic {
    public record TreeTable<TItem, TKey1, TKey2, TKey3, TKey4, TKey5, TValue> : TreeTable<TItem, TValue>
            where TKey1 : notnull
            where TKey2 : notnull
            where TKey3 : notnull
            where TKey4 : notnull
            where TKey5 : notnull {

        
        public ImmutableDictionary<TKey1, ImmutableDictionary<TKey2, ImmutableDictionary<TKey3, ImmutableDictionary<TKey4, ImmutableDictionary<TKey5, ImmutableList<TValue>>>>>> Values { get; init; } = ImmutableDictionary<TKey1, ImmutableDictionary<TKey2, ImmutableDictionary<TKey3, ImmutableDictionary<TKey4, ImmutableDictionary<TKey5, ImmutableList<TValue>>>>>>.Empty;

        
        public Func<TItem, TKey1>? Key1Extractor { get; init; }
        public IEqualityComparer<TKey1>? Key1Comparer { get; init; }

        public Func<TItem, TKey2>? Key2Extractor { get; init; }
        public IEqualityComparer<TKey2>? Key2Comparer { get; init; }

        public Func<TItem, TKey3>? Key3Extractor { get; init; }
        public IEqualityComparer<TKey3>? Key3Comparer { get; init; }

        public Func<TItem, TKey4>? Key4Extractor { get; init; }
        public IEqualityComparer<TKey4>? Key4Comparer { get; init; }

        public Func<TItem, TKey5>? Key5Extractor { get; init; }
        public IEqualityComparer<TKey5>? Key5Comparer { get; init; }

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

        public TreeTable<TItem, TKey1, TKey2, TKey3, TKey4, TKey5, TValue> Add(TKey1 Key1, TKey2 Key2, TKey3 Key3, TKey4 Key4, TKey5 Key5, TItem Item) {
            var Value = ValueSelector(Item);
            
            var Level0 = Values;

            if (!Level0.TryGetValue(Key1, out var Level1)) {
                Level1 = ImmutableDictionary<TKey2, ImmutableDictionary<TKey3, ImmutableDictionary<TKey4, ImmutableDictionary<TKey5, ImmutableList<TValue>>>>>.Empty.WithComparers(Key2Comparer);
            }

            if (!Level1.TryGetValue(Key2, out var Level2)) {
                Level2 = ImmutableDictionary<TKey3, ImmutableDictionary<TKey4, ImmutableDictionary<TKey5, ImmutableList<TValue>>>>.Empty.WithComparers(Key3Comparer);
            }

            if (!Level2.TryGetValue(Key3, out var Level3)) {
                Level3 = ImmutableDictionary<TKey4, ImmutableDictionary<TKey5, ImmutableList<TValue>>>.Empty.WithComparers(Key4Comparer);
            }

            if (!Level3.TryGetValue(Key4, out var Level4)) {
                Level4 = ImmutableDictionary<TKey5, ImmutableList<TValue>>.Empty.WithComparers(Key5Comparer);
            }

            if (Level4.TryGetValue(Key5, out var Level5)) {
                Level5 = Level5.Add(Value);
            } else {
                Level5 = new[] { Value }.ToImmutableList();
            }

            Level4 = Level4.SetItem(Key5, Level5);
            Level3 = Level3.SetItem(Key4, Level4);
            Level2 = Level2.SetItem(Key3, Level3);
            Level1 = Level1.SetItem(Key2, Level2);
            Level0 = Level0.SetItem(Key1, Level1);

            var ret = this with {
                Items = this.Items.Add(Item),
                Values = Level0,
                
            };

            return ret;
        }

    }
}