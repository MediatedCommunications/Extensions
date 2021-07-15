using System.Collections.Immutable;

namespace System.Events.Async {
    public abstract class AsyncMulticastDelegate<TSender, TArgs> 
        : IAsyncDelegateAdder<TSender, TArgs>
        , IAsyncDelegateRemover<TSender, TArgs>
        where TArgs : IHandled {

        protected AsyncDelegate<TSender, TArgs>? HandlerDelegate { get; private set; }
        protected ImmutableList<AsyncDelegate<TSender, TArgs>> HandlerInvocationList { get; private set; }

        public event AsyncDelegate<TSender, TArgs>? Handler {
            add {
                HandlerDelegate += value;
                RegenerateInvocationList();
            }
            remove {
                HandlerDelegate -= value;
                RegenerateInvocationList();
            }
        }

        protected void RegenerateInvocationList() {
            HandlerInvocationList = this.HandlerDelegate.GetInvocations().ToImmutableList();
        }


        public IAsyncDelegateAdder<TSender, TArgs> Adder { get; }
        public IAsyncDelegateRemover<TSender, TArgs> Remover { get; }

        public bool ThrowOnError { get; init; }
        public AsyncMulticastDelegate() {
            HandlerInvocationList = ImmutableList<AsyncDelegate<TSender, TArgs>>.Empty;

            this.Adder = new DelegatingAsyncDelegateAdder<TSender, TArgs>(this);
            this.Remover = new DelegatingAsyncDelegateRemover<TSender, TArgs>(this);
        }

        public virtual void Add(AsyncDelegate<TSender, TArgs> Handler) {
            this.Handler += Handler;
        }

        public virtual void Remove(AsyncDelegate<TSender, TArgs> Handler) {
            this.Handler -= Handler;
        }

    }
}
