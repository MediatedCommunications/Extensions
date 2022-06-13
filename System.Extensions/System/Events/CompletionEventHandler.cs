using System.Diagnostics;
using System.Events.Async;

namespace System.Extensions.System.Events {
    public class CompletionEventHandler<TSender, TArgs> : DisplayClass {

        public CancellationToken IsComplete { get; }

        protected CompletionEvent<TSender, TArgs> Event { get; }
        public CompletionEventHandler(CompletionEvent<TSender, TArgs> Event) {
            this.Event = Event;
            this.IsComplete = Event.IsComplete;
        }

        public IDisposable Subscribe(AsyncDelegate<TSender, TArgs> Handler) {
            return Event.Subscribe(Handler);
        }
        public Task<IDisposable> SubscribeAsync(AsyncDelegate<TSender, TArgs> Handler, bool? Await = default) {
            return Event.SubscribeAsync(Handler, Await);
        }
    }

}
