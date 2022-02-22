using System.Collections.Immutable;
using System.Diagnostics;

namespace System.Collections.Generic {
    public abstract record TreeTable<TItem, TValue> : DisplayRecord, IEnumerable<TValue> {
        public ImmutableList<TItem> Items { get; init; } = ImmutableList<TItem>.Empty;
        public Func<TItem, TValue> ValueSelector { get; init; } = x => throw new NotImplementedException();

        public abstract IEnumerator<TValue> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}