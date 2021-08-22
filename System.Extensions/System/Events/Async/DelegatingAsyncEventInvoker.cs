using System.Threading.Tasks;

namespace System.Events.Async
{
    internal class DelegatingAsyncEventInvoker<TSender, TArgs>
    : IAsyncEventInvoker<TSender, TArgs> {

        protected readonly IAsyncEventInvoker<TSender, TArgs> Delegate;
        public DelegatingAsyncEventInvoker(IAsyncEventInvoker<TSender, TArgs> Delegate) {
            this.Delegate = Delegate;
        }

        public Task InvokeAsync(TSender sender, TArgs args) {
            return this.Delegate.InvokeAsync(sender, args);
        }
    }
}
