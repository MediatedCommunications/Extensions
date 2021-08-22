using System.Threading.Tasks;

namespace System.Events.Async
{
    internal class DelegatingAsyncFunctionInvoker<TSender, TData, TResult>
    : IAsyncFunctionInvoker<TSender, TData, TResult> {

        protected readonly IAsyncFunctionInvoker<TSender, TData, TResult> Delegate;
        public DelegatingAsyncFunctionInvoker(IAsyncFunctionInvoker<TSender, TData, TResult> Delegate) {
            this.Delegate = Delegate;
        }

        public Task<TResult?> InvokeAsync(TSender sender, TData args) {
            return this.Delegate.InvokeAsync(sender, args);
        }
    }
}
