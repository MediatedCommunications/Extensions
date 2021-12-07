using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Senders
{
    public record ThrowHttpMessageSender : HttpRequestMessageSender {

        public override Task<HttpResponseMessage> SendAsync(IHttpRequestMessageBuilder Message, CancellationToken Token) {
            throw new NotImplementedException();
        }
    }

}
