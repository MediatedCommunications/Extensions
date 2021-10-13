using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Message.Senders
{
    public abstract record HttpMessageSender : DisplayRecord, IHttpMessageSender {
        public abstract Task<HttpResponseMessage> SendAsync(IHttpRequestMessageBuilder Message, CancellationToken Token);
    }

}
