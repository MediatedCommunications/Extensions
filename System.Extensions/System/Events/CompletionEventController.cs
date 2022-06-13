using System.Diagnostics;

namespace System.Extensions.System.Events {
    public class CompletionEventInvoker<TSender, TArgs> : DisplayClass {
        protected CompletionEvent<TSender, TArgs> Event { get; }
        public CompletionEventInvoker(CompletionEvent<TSender, TArgs> Event) {
            this.Event = Event;
        }

        public void Invoke(TSender Sender, TArgs Args) {
            Event.Invoke(Sender, Args);
        }

        public Task InvokeAsync(TSender Sender, TArgs Args) {
            return Event.InvokeAsync(Sender, Args);
        }
    }

}
