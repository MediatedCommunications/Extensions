namespace System.Events.Async {
    public interface IAsyncDelegateAdder<TSender, TArgs> {
        void Add(AsyncDelegate<TSender, TArgs> Handler);
    }
}
