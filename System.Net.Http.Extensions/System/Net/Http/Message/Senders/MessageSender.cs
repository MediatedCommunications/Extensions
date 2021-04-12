using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Senders {
    public abstract record MessageSender<TRequest, TResponse> : DisplayRecord, IMessageSender<TRequest, TResponse> {
        public abstract Task<TResponse> SendAsync(TRequest Message, CancellationToken Token);
    }

}
