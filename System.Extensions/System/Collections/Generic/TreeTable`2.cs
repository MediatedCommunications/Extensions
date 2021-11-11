using System.Collections.Immutable;
using System.Linq;

namespace System.Collections.Generic {
    public record TreeTable<TKey1, TValue> : TreeTable<TValue>
        where TKey1 : notnull
        {

        public ImmutableDictionary<TKey1, ImmutableList<TValue>> Values { get; init; } = ImmutableDictionary<TKey1, ImmutableList<TValue>>.Empty;

        public Func<TValue, TKey1>? Key1Extractor { get; init; }
        public IEqualityComparer<TKey1>? Key1Comparer { get; init; }

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
      
        public TreeTable<TKey1, TValue> Add(TKey1 Key1, TValue Value) {
            var Level0 = Values;

            if (Level0.TryGetValue(Key1, out var Level1)) {
                Level1 = Level1.Add(Value);
            } else {
                Level1 = new[] { Value }.ToImmutableList();
            }
            Level0 = Level0.SetItem(Key1, Level1);


            var ret = this with {
                Values = Level0
            };

            return ret;
        }

    }
}