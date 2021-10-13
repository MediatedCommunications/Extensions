namespace System.Collections.Generic {
    public record ChunkResultItem<T> : ChunkResult<T>{

        public T Item { get; init; }

        public ChunkResultItem(T item) {
            this.Item = item;
        }
        
    }

}
