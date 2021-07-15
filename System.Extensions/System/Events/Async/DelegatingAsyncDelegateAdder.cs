namespace System.Events.Async {
    internal class DelegatingAsyncDelegateAdder<TSender, TArgs>
        : IAsyncDelegateAdder<TSender, TArgs> {

        protected readonly IAsyncDelegateAdder<TSender, TArgs> Delegate;
        public DelegatingAsyncDelegateAdder(IAsyncDelegateAdder<TSender, TArgs> Delegate) {
            this.Delegate = Delegate;
        }

        public void Add(AsyncDelegate<TSender, TArgs> Handler) {
            this.Delegate.Add(Handler);
        }
    }
}
