using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Senders
{
    public abstract record DelegatingHttpMessageSender : HttpMessageSender {
        public IHttpMessageSender Child { get; init; }

        public DelegatingHttpMessageSender(IHttpMessageSender? Child = default) {
            this.Child = Child ?? HttpMessageSenders.Throw;
        }

        public override async Task<HttpResponseMessage> SendAsync(IHttpRequestMessageBuilder Message, CancellationToken Token) {
            var ret = await Child.SendAsync(Message, Token)
                .DefaultAwait()
                ;

            return ret;
        }

    }

}
