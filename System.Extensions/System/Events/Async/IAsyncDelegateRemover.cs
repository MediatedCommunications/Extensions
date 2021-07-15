namespace System.Events.Async {
    public interface IAsyncDelegateRemover<TSender, TArgs> {
        void Remove(AsyncDelegate<TSender, TArgs> Handler);
    }
}
