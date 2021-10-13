namespace System.Collections.Generic {
    public abstract class AdaptiveChunkerBase {

        protected abstract IAsyncEnumerable<ChunkResult<T>> ChunkInternal<T>(IAsyncEnumerable<T> This);

        public async IAsyncEnumerable<LinkedList<T>> Chunk<T>(IAsyncEnumerable<T> This) {
            var ret = new LinkedList<T>();

            await foreach (var item in ChunkInternal(This).DefaultAwait()) {
                if(item is ChunkResultEnd<T> V1) {
                    yield return ret;
                    ret = new();

                } else if (item is ChunkResultItem<T> V2) {
                    ret.Add(V2.Item);
                }
            }

            if (ret.Count > 0) {
                yield return ret;
            }
        }
    }

}
