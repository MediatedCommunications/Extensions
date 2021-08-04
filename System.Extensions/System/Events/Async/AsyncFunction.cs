using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Events.Async {

    public class AsyncFunction<TSender, TData, TResult> 
        : AsyncMulticastDelegate<TSender, FunctionEventArgs<TData, TResult>>
        , IAsyncFunctionInvoker<TSender, TData, TResult> 
        {
        
        public TResult? DefaultResult { get; init; }

        public IAsyncFunctionInvoker<TSender, TData, TResult> Invoker { get; }

        public AsyncFunction() {

            this.Invoker = new DelegatingAsyncFunctionInvoker<TSender, TData, TResult>(this);

        }

        public virtual async Task<TResult?> InvokeAsync(TSender sender, TData data) {
            var ret = DefaultResult;

            var args = FunctionEventArgs.Create(data, DefaultResult);

            var ToInvoke = this.HandlerInvocationList;

            foreach (var item in ToInvoke) {
                try {
                    await item(sender, args)
                        .DefaultAwait()
                        ;
                } catch (Exception ex) {
                    ex.Ignore();

                    if (this.ThrowOnError) {
                        throw;
                    }

                }

                ret = args.Result;
                if (args.Handled) {
                    break;
                }

            }

            return ret;
        }

    }
}
