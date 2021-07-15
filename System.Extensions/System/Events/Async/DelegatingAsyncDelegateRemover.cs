namespace System.Events.Async {
    internal class DelegatingAsyncDelegateRemover<TSender, TArgs>
    : IAsyncDelegateRemover<TSender, TArgs> {

        protected readonly IAsyncDelegateRemover<TSender, TArgs> Delegate;
        public DelegatingAsyncDelegateRemover(IAsyncDelegateRemover<TSender, TArgs> Delegate) {
            this.Delegate = Delegate;
        }

        public void Remove(AsyncDelegate<TSender, TArgs> Handler) {
            this.Delegate.Remove(Handler);
        }
    }
}
