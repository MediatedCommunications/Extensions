using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Senders {
    public record ThrowMessageSender<TRequest, TResponse> : MessageSender<TRequest, TResponse> {

        public static ThrowMessageSender<TRequest, TResponse> Instance { get; private set; } = new ThrowMessageSender<TRequest, TResponse>();

        public override Task<TResponse> SendAsync(TRequest Message, CancellationToken Token) {
            throw new NotImplementedException();
        }
    }

}
