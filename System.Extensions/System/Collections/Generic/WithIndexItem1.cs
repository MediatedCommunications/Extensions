using System.Diagnostics;

namespace System.Collections.Generic {
    public record WithIndexItem<T> : DisplayRecord {
        public int Index { get; init; }
        public T Item { get; init; }

        public WithIndexItem(int Index, T Item) {
            this.Index = Index;
            this.Item = Item;
        }

        public void Deconstruct(out int Index, out T Item) {
            Index = this.Index;
            Item = this.Item;
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Id.Add(Index)
                .Data.Add(Item)
                ;
        }


    }

}
