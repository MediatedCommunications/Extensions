using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Senders
{
    public abstract record DelegatingHttpMessageSender : HttpRequestMessageSender {
        public IHttpRequestMessageSender Child { get; init; }

        public DelegatingHttpMessageSender(IHttpRequestMessageSender? Child = default) {
            this.Child = Child ?? HttpRequestMessageSenders.Throw;
        }

        public override async Task<HttpResponseMessage> SendAsync(IHttpRequestMessageBuilder Message, CancellationToken Token) {
            var ret = await Child.SendAsync(Message, Token)
                .DefaultAwait()
                ;

            return ret;
        }

    }

}
