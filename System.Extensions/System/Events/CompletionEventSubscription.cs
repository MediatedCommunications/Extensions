using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Events.Async;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions.System.Events {

    internal class CompletionEventSubscription<TSender, TArgs> : DisplayClass, IDisposable {

        private AsyncDelegate<TSender, TArgs>? Handler { get; set; }
        private Action<CompletionEventSubscription<TSender, TArgs>>? RemoveHandler { get; set; }

        public CompletionEventSubscription(
            AsyncDelegate<TSender, TArgs> Handler,
            Action<CompletionEventSubscription<TSender, TArgs>> RemoveHandler
            ) {
            this.Handler = Handler;
            this.RemoveHandler = RemoveHandler;
        }

        public async Task InvokeAsync(TSender Sender, TArgs Args) {
            if(Handler is { } V1) {
                await V1(Sender, Args)
                    .DefaultAwait()
                    ;
            }

            Dispose();
        }

        public void Dispose() {
            this.Handler = null;
            
            this.RemoveHandler?.Invoke(this);
            this.RemoveHandler = null;
        }
    }

}
