using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Events.Async {

    public class AsyncMethod<TSender, TData>
        : AsyncMulticastDelegate<TSender, MethodEventArgs<TData>>
        , IAsyncEventInvoker<TSender, TData> 
        {
        
        public IAsyncEventInvoker<TSender, TData> Invoker { get; }
       
        public AsyncMethod() {
            this.Invoker = new DelegatingAsyncEventInvoker<TSender, TData>(this);
        }

        public virtual async Task InvokeAsync(TSender sender, TData data) {
            var args = MethodEventArgs.Create(data);

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

                if (args.Handled) {
                    break;
                }

            }
        }

    }
}
