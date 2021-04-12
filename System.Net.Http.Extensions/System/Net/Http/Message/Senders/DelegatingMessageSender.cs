using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Senders {
    public abstract record DelegatingMessageSender<TRequest, TResponse> : MessageSender<TRequest, TResponse> {
        public IMessageSender<TRequest, TResponse> Child { get; init; }

        public DelegatingMessageSender(IMessageSender<TRequest, TResponse>? Child = default) {
            this.Child = Child ?? ThrowMessageSender<TRequest, TResponse>.Instance;
        }

        public override async Task<TResponse> SendAsync(TRequest Message, CancellationToken Token) {
            var ret = await Child.SendAsync(Message, Token)
                .DefaultAwait()
                ;

            return ret;
        }

    }

}
