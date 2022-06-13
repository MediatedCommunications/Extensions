using System.Events.Async;
using System.Collections.Immutable;
using System.Diagnostics;

namespace System.Extensions.System.Events {

    public class CompletionEvent<TSender, TArgs> : DisplayClass {
        public CompletionEventHandler<TSender, TArgs> Subscriber { get; }
        public CompletionEventInvoker<TSender, TArgs> Controller { get; }


        public CancellationToken IsComplete { get; }
        protected CancellationTokenSource Source { get; }

        private ImmutableList<CompletionEventSubscription<TSender, TArgs>> Queue { get; set; }
        protected bool Await { get;}

        public CompletionEvent(bool Await = false) {
            this.Await = Await;
            this.Queue = ImmutableList<CompletionEventSubscription<TSender, TArgs>>.Empty;

            this.Source = new CancellationTokenSource();
            this.IsComplete = Source.Token;


            this.Subscriber = new(this);
            this.Controller = new(this);

        }

        private void RemoveHandler(CompletionEventSubscription<TSender, TArgs> Handler) {
            Queue = Queue.Remove(Handler);
        }

        private void AddHandler(CompletionEventSubscription<TSender, TArgs> Handler) {
            Queue = Queue.Add(Handler);
        }

        public IDisposable Subscribe(AsyncDelegate<TSender, TArgs> Handler) {
            var ret = new CompletionEventSubscription<TSender, TArgs>(Handler, RemoveHandler);
            if(Original is { } V1) {
                var T = InvokeAsync(V1.Item1, V1.Item2, ret, false);
            } else {
                AddHandler(ret);
            }
            return ret;
        }

        public async Task<IDisposable> SubscribeAsync(AsyncDelegate<TSender, TArgs> Handler, bool? Await = default) {
            var MyAwait = Await ?? this.Await;

            var ret = new CompletionEventSubscription<TSender, TArgs>(Handler, RemoveHandler);
            
            if (Original is { } V1) {
                await InvokeAsync(V1.Item1, V1.Item2, ret, MyAwait)
                    .DefaultAwait()
                    ;
            } else {
                AddHandler(ret);
            }

            return ret;
        }



        protected volatile Tuple<TSender, TArgs>? Original;

        public void Invoke(TSender Sender, TArgs Args) {
            if (Original is not null) {
                throw new InvalidOperationException();
            }

            Original = Tuple.Create(Sender, Args);
            Source.Cancel();
            foreach (var item in Queue) {
                var T = InvokeAsync(Sender, Args, item, false)
                    .DefaultAwait()
                    ;
            }

            Queue = ImmutableList<CompletionEventSubscription<TSender, TArgs>>.Empty;


        }


        public async Task InvokeAsync(TSender Sender, TArgs Args) {
            if(Original is not null) {
                throw new InvalidOperationException();
            }

            Original = Tuple.Create(Sender, Args);
            Source.Cancel();
            foreach (var item in Queue) {
                await InvokeAsync(Sender, Args, item, this.Await)
                    .DefaultAwait()
                    ;
            }

            Queue = ImmutableList<CompletionEventSubscription<TSender, TArgs>>.Empty;


        }

        private async Task InvokeAsync(TSender Sender, TArgs Args, CompletionEventSubscription<TSender, TArgs> Method, bool Await) {

            var T = Task.Run(async () => { 
                await Method.InvokeAsync(Sender, Args)
                    .DefaultAwait()
                    ;
            });
            if (Await) {
                await T
                    .DefaultAwait()
                    ;
            }
            
           

        }

    }

}
